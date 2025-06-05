using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Domain.Interfaces;
using ApartmentManaging.Infrastructure.Data;
using ApartmentManaging.Shared.DTOs.Requests.Authentication;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbWorker _dbWorker;

        public UserRepository(DbWorker dbWorker)
        {
            _dbWorker = dbWorker ?? throw new ArgumentNullException(nameof(dbWorker));
        }

        public async Task<User?> AddAsync(User request)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Username", request.Username),
                new SqlParameter("@PasswordHash", request.PasswordHash),
                new SqlParameter("@FullName", request.FullName),
                new SqlParameter("@IsActive", request.IsActive),
                new SqlParameter("@RoleId", request.RoleId)
            };

            var dataTable = await _dbWorker.ExecuteStoredProcedureAsync(StoredProcedures.User.Insert_User, parameters);

            if (dataTable.Rows.Count == 0)
                return null;

            var row = dataTable.Rows[0];
            return MapUser(row);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            return await _dbWorker.ExecuteNonQueryAsync(StoredProcedures.User.Delete_User, parameters);
        }

        public async Task<User?> GetByIdAsync(int? id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id ?? (object)DBNull.Value)
            };

            var dataTable = await _dbWorker.ExecuteStoredProcedureAsync(StoredProcedures.User.Get_User_By_Id, parameters);
            if (dataTable.Rows.Count == 0)
                return null;

            return MapUser(dataTable.Rows[0]);
        }

        public async Task<User?> Login(LoginDto request)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Username", request.Username),
                new SqlParameter("@PasswordHash", request.PasswordHash)
            };

            var dataTable = await _dbWorker.ExecuteStoredProcedureAsync(StoredProcedures.User.Login_User, parameters);

            if (dataTable.Rows.Count == 0)
                return null;

            return MapUser(dataTable.Rows[0]);
        }

        public async Task<User?> UpdateAsync(User request)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", request.Id),
                new SqlParameter("@Username", request.Username),
                new SqlParameter("@PasswordHash", request.PasswordHash),
                new SqlParameter("@FullName", request.FullName)
            };

            var dataTable = await _dbWorker.ExecuteStoredProcedureAsync(StoredProcedures.User.Update_User, parameters);

            if (dataTable.Rows.Count == 0)
                return null;

            return MapUser(dataTable.Rows[0]);
        }

        private User MapUser(DataRow row)
        {
            return new User
            {
                Id = Convert.ToInt32(row["Id"]),
                Username = row["Username"].ToString() ?? string.Empty,
                FullName = row["FullName"].ToString() ?? string.Empty,
                IsActive = Convert.ToBoolean(row["IsActive"]),
                RoleId = Convert.ToInt32(row["RoleId"])
            };
        }
    }
}
