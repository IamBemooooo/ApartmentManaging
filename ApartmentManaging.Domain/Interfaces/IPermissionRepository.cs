using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Domain.Interfaces.BaseInterfaces;
using ApartmentManaging.Shared.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Domain.Interfaces
{
    public interface IPermissionRepository : IBaseRepository<Permission>,
        IGetPagedRepository<PagingRequest, PagingResponse<Permission>>
    {
        Task<List<Permission>> GetPermissionsByRoleId(int roleId);
    }

}
