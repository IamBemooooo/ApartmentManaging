using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Domain.Interfaces.BaseInterfaces;
using ApartmentManaging.Shared.DTOs.Requests.Authentication;

namespace ApartmentManaging.Domain.Interfaces
{
    public interface IUserRepository :
        IBaseRepository<User>
    {
        Task<User?> Login(LoginDto request);
    }
}
