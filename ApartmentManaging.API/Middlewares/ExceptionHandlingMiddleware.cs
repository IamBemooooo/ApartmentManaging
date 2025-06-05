using ApartmentManaging.Shared.DTOs.Response.API;
using ApartmentManaging.Shared.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace ApartmentManaging.API.Middlewares
{
    /// <summary>
    /// Middleware xử lý ngoại lệ toàn cục cho ứng dụng ASP.NET Core.
    /// Chặn và bắt các exception phát sinh trong pipeline request, 
    /// ghi log lỗi và trả về response chuẩn dạng JSON cho client.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Khởi tạo middleware với các dependency cần thiết.
        /// </summary>
        /// <param name="next">Delegate tiếp theo trong pipeline.</param>
        /// <param name="logger">Logger để ghi lại thông tin lỗi.</param>
        /// <param name="env">Thông tin môi trường (Development, Production,...).</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }


        /// <summary>
        /// Phương thức xử lý request.
        /// Thực thi pipeline tiếp theo, bắt và xử lý các exception nếu có.
        /// </summary>
        /// <param name="context">HttpContext của request hiện tại.</param>
        /// <returns>Task bất đồng bộ thể hiện quá trình xử lý.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi không mong muốn");
                context.Response.ContentType = "application/json";

                int statusCode;
                string message;
                IDictionary<string, string[]> errors = null;

                switch (ex)
                {
                    case ValidationException validationEx:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        message = validationEx.Message;
                        break;

                    case BusinessException businessEx:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        message = businessEx.Message;
                        break;

                    case ArgumentNullException nullArgEx:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        message = $"Thiếu tham số: {nullArgEx.ParamName}";
                        break;

                    case SecurityTokenException:
                        statusCode = (int)HttpStatusCode.Unauthorized;
                        message = "Bạn chưa đăng nhập.";
                        break;

                    case UnauthorizedAccessException:
                        statusCode = (int)HttpStatusCode.Forbidden;
                        message = "Bạn không có quyền truy cập.";
                        break;

                    case SqlException sqlEx when sqlEx.Message.Contains("FK_"):
                        statusCode = (int)HttpStatusCode.BadRequest;
                        message = "Không thể thực hiện thao tác do ràng buộc dữ liệu.";
                        break;

                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        message = _env.IsDevelopment() ? ex.Message : "Lỗi hệ thống, vui lòng thử lại.";
                        break;
                }

                context.Response.StatusCode = statusCode;

                var errorResponse = new APIResponse<string>
                {
                    Status = statusCode,
                    Message = message,
                    Data = null
                };

                var result = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(result);
            }
        }
    }
}
