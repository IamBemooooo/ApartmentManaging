using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Shared.DTOs.Requests.Apartment;
using AutoMapper;

namespace ApartmentManaging.Application.Mappings
{
    /// <summary>
    /// Cấu hình ánh xạ (mapping) giữa các DTO và entity <see cref="Apartment"/> sử dụng AutoMapper.
    /// </summary>
    public class ApartmentProfile : Profile
    {
        public ApartmentProfile()
        {
            // Ánh xạ hai chiều giữa ApartmentCreateDto và Apartment
            CreateMap<ApartmentCreateDto, Apartment>().ReverseMap();

            // Ánh xạ hai chiều giữa ApartmentViewDto và Apartment
            CreateMap<ApartmentUpdateDto, Apartment>().ReverseMap();
        }
    }
}
