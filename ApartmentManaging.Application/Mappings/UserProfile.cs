using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Shared.DTOs.Requests.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
        }
    }
}
