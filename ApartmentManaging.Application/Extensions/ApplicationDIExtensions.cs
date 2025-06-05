using ApartmentManaging.Application.Interfaces;
using ApartmentManaging.Application.Mappings;
using ApartmentManaging.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApartmentManaging.Application.Extensions
{
    /// <summary>
    /// Lớp mở rộng để đăng ký các dịch vụ (services) thuộc tầng Application vào DI container.
    /// </summary>
    public static class ApplicationDIExtensions
    {
        /// <summary>
        /// Đăng ký các service và cấu hình liên quan đến tầng Application.
        /// </summary>
        /// <param name="services">Đối tượng <see cref="IServiceCollection"/> để đăng ký dịch vụ.</param>
        /// <returns>Trả về <see cref="IServiceCollection"/> sau khi đăng ký xong.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Đăng ký các service nghiệp vụ (Use Cases)
            services.AddScoped<IApartmentTypeService, ApartmentTypeService>();
            services.AddScoped<IApartmentService, ApartmentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();


            // Đăng ký AutoMapper và các Profile mapping trong Application layer
            services.AddAutoMapper(typeof(ApartmentTypeProfile));
            services.AddAutoMapper(typeof(ApartmentProfile));

            return services;
        }
    }
}
