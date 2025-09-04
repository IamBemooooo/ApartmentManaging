using ApartmentManaging.API.Middlewares;
using ApartmentManaging.Application.Extensions;
using ApartmentManaging.Infrastructure.Data;
using ApartmentManaging.Infrastructure.Extensions;
using ApartmentManaging.Shared.DTOs.Response.API;
using ApartmentManaging.Shared.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ApartmentManaging.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Tạo Configuration để đọc appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = WebApplication.CreateBuilder(args);

            var secretKey = configuration["Jwt:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            // Đăng ký dịch vụ Authentication với JWT Bearer
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,            // Kiểm tra issuer
                        ValidateAudience = false,          // Kiểm tra audience

                        ValidateLifetime = true,          // Kiểm tra thời gian hết hạn token

                        ClockSkew = TimeSpan.Zero,        // Không cho phép trễ thời gian hết hạn

                        ValidateIssuerSigningKey = true,  // Kiểm tra key sign token
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes) // key bí mật
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            // Ngăn middleware trả response mặc định (empty 401)
                            context.HandleResponse();

                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";

                            var responseObj = new APIResponse<string>(
                                StatusCodes.Status401Unauthorized,
                                Messages.Unauthorized,
                                null
                            );

                            var json = JsonSerializer.Serialize(responseObj);

                            await context.Response.WriteAsync(json);
                        }
                    };

                });

            // Đăng ký dịch vụ Authorization
            builder.Services.AddAuthorization();


            // Đăng ký Swagger và cấu hình để nhập Bearer token trong header
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Nhập 'Bearer' rồi thêm token JWT vào phía sau (ví dụ: 'Bearer abc123')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            // Đăng ký các dịch vụ controller kèm xử lý lỗi ModelState
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

                options.InvalidModelStateResponseFactory = context =>
                {
                    var modelState = context.ModelState;
                    var errors = modelState
                        .Where(ms => ms.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var apiResponse = new APIResponse<object>
                    {
                        Status = 400,
                        Message = Messages.InvalidInput,
                        Data = null,
                        Errors = errors
                    };

                    return new BadRequestObjectResult(apiResponse);
                };
            });

            // Lấy connection string và đăng ký DbWorker (đã có sẵn)
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddSingleton(new DbWorker(connectionString));

            // Đăng ký các service khác (Application, Infrastructure)
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices();

            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Luôn kích hoạt Swagger UI ở mọi môi trường
            app.UseSwagger();
            app.UseSwaggerUI();

            // Middleware xử lý ngoại lệ chung
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            // ĐÚNG THỨ TỰ: Authentication trước, Authorization sau
            app.UseAuthentication();
            app.UseAuthorization();

            // Thêm middleware xử lý 403 ngay dưới đây
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == StatusCodes.Status403Forbidden && !context.Response.HasStarted)
                {
                    context.Response.ContentType = "application/json";

                    var response = new APIResponse<string>(
                        StatusCodes.Status403Forbidden,
                        Messages.Forbidden,
                        null
                    );

                    var json = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsync(json);
                }
            });


            app.UseMiddleware<PermissionMiddleware>();

            // Map controller và các endpoint khác
            app.MapControllers();

            app.Run();
        }
    }
}
