using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Domain.Interfaces;
using ApartmentManaging.Domain.Interfaces.BaseInterfaces;
using ApartmentManaging.Infrastructure.Repositories;
using ApartmentManaging.Shared.DTOs.Common;
using ApartmentManaging.Shared.DTOs.Response.Apartment;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Infrastructure.Extensions
{
    /// <summary>
    /// Extension method để đăng ký các dịch vụ thuộc tầng Infrastructure vào Dependency Injection container.
    /// </summary>
    public static class InfrastructureDIExtensions
    {
        /// <summary>
        /// Đăng ký các repository và dịch vụ liên quan đến tầng Infrastructure.
        /// </summary>
        /// <param name="services">Đối tượng <see cref="IServiceCollection"/> để cấu hình DI.</param>
        /// <returns>Trả về <see cref="IServiceCollection"/> sau khi đã thêm các service.</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Đăng ký các repository và service cho entity ApartmentType
            services.AddScoped<IBaseRepository<ApartmentType>, ApartmentTypeRepository>();
            services.AddScoped<IGetPagedRepository<PagingRequest, PagingResponse<ApartmentType>>, ApartmentTypeRepository>();

            // Đăng ký các repository và service cho entity Apartment
            services.AddScoped<IBaseRepository<Apartment>, ApartmentRepository>();
            services.AddScoped<IGetPagedRepository<PagingRequest, PagingResponse<ApartmentViewDto>>, ApartmentRepository>();

            // Đăng ký các repository và service cho entity User
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IPermissionRepository, PermissionRepository>();

            return services;
        }
    }

}
