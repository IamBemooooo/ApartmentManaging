using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Shared.DTOs.Requests.ApartmentType;
using AutoMapper;

namespace ApartmentManaging.Application.Mappings
{
    /// <summary>
    /// Cấu hình ánh xạ (mapping) giữa giữa các DTO và entity <see cref="ApartmentType"/> sử dụng AutoMapper.
    /// </summary>
    public class ApartmentTypeProfile : Profile
    {
        public ApartmentTypeProfile()
        {
            // Ánh xạ hai chiều giữa ApartmentTypeCreateDto và ApartmentType
            CreateMap<ApartmentTypeCreateDto, ApartmentType>().ReverseMap();
        }
    }
}
