using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Domain.Interfaces;
using ApartmentManaging.Infrastructure.Data;
using ApartmentManaging.Shared.DTOs.Common;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.Http;

namespace ApartmentManaging.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly DbWorker _db;

        public PermissionRepository(DbWorker db)
        {
            _db = db;
        }

        public Task<Permission?> AddAsync(Permission request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Permission?> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<PagingResponse<Permission>> GetPagedAsync(PagingRequest filter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Permission>> GetPermissionsByRoleId(int roleId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@RoleId", SqlDbType.Int) { Value = roleId }
            };

            var table = await _db.ExecuteStoredProcedureAsync(StoredProcedures.Permission.Get_Permissions_By_RoleId, parameters);

            var permissions = new List<Permission>();

            foreach (DataRow row in table.Rows)
            {
                permissions.Add(new Permission
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Code = row["Code"].ToString() ?? string.Empty,
                    ApiEndpoint = row["ApiEndpoint"].ToString() ?? "",
                    Description = row["Description"].ToString() ?? "",
                    HttpMethod = row["HttpMethod"].ToString() ?? ""
                });
            }

            return permissions;
        }

        public Task<Permission?> UpdateAsync(Permission request)
        {
            throw new NotImplementedException();
        }
    }
}
