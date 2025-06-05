using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Domain.Interfaces.BaseInterfaces;
using ApartmentManaging.Infrastructure.Data;
using ApartmentManaging.Shared.DTOs.Common;
using ApartmentManaging.Shared.DTOs.Response.Apartment;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Infrastructure.Repositories
{
    public class ApartmentRepository :
         IGetPagedRepository<PagingRequest, PagingResponse<ApartmentViewDto>>,
         IBaseRepository<Apartment>
    {
        private readonly DbWorker _dbWorker;

        public ApartmentRepository(DbWorker dbWorker)
        {
            _dbWorker = dbWorker ?? throw new ArgumentNullException(nameof(dbWorker));
        }

        public async Task<Apartment?> AddAsync(Apartment request)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ApartmentTypeId", request.ApartmentTypeId),
                new SqlParameter("@Name", request.Name),
                new SqlParameter("@Address", request.Address),
                new SqlParameter("@FloorCount", (object?)request.FloorCount ?? DBNull.Value),
                new SqlParameter("@Price", request.Price)
            };

            var dataTable = await _dbWorker.ExecuteStoredProcedureAsync(StoredProcedures.Apartment.Add_Apartment, parameters);

            if (dataTable.Rows.Count == 0)
                return null;

            var row = dataTable.Rows[0];
            return new Apartment
            {
                Id = Convert.ToInt32(row["Id"]),
                ApartmentTypeId = Convert.ToInt32(row["ApartmentTypeId"]),
                Name = row["Name"].ToString() ?? string.Empty,
                Address = row["Address"].ToString() ?? string.Empty,
                FloorCount = row["FloorCount"] == DBNull.Value ? null : Convert.ToInt32(row["FloorCount"]),
                Price = Convert.ToDecimal(row["Price"]),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"])
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            return await _dbWorker.ExecuteNonQueryAsync(StoredProcedures.Apartment.Delete_Apartment, parameters);
        }

        public async Task<Apartment?> GetByIdAsync(int? id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id ?? (object)DBNull.Value)
            };

            var dataTable = await _dbWorker.ExecuteStoredProcedureAsync(StoredProcedures.Apartment.Get_Apartment_By_Id, parameters);
            if (dataTable.Rows.Count == 0)
                return null;

            var row = dataTable.Rows[0];
            return new Apartment
            {
                Id = Convert.ToInt32(row["Id"]),
                ApartmentTypeId = Convert.ToInt32(row["ApartmentTypeId"]),
                Name = row["Name"].ToString() ?? string.Empty,
                Address = row["Address"].ToString() ?? string.Empty,
                FloorCount = row["FloorCount"] == DBNull.Value ? null : Convert.ToInt32(row["FloorCount"]),
                Price = Convert.ToDecimal(row["Price"]),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"])
            };
        }

        public async Task<PagingResponse<ApartmentViewDto>> GetPagedAsync(PagingRequest filter)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@PageIndex", filter.PageIndex),
                new SqlParameter("@PageSize", filter.PageSize)
            };

            var dataSet = await _dbWorker.ExecuteStoredProcedureWithMultipleResultsAsync(StoredProcedures.Apartment.Get_Paged_Apartments, parameters);

            var apartmentTable = dataSet.Tables[0];
            var totalCount = Convert.ToInt32(dataSet.Tables[1].Rows[0]["TotalCount"]);

            var apartments = new List<ApartmentViewDto>();
            foreach (DataRow row in apartmentTable.Rows)
            {
                apartments.Add(new ApartmentViewDto
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString() ?? string.Empty,
                    NameApartmentType = row["ApartmentTypeName"].ToString() ?? string.Empty,
                    Address = row["Address"].ToString() ?? string.Empty,
                    FloorCount = row["FloorCount"] == DBNull.Value ? null : Convert.ToInt32(row["FloorCount"]),
                    Price = Convert.ToDecimal(row["Price"]),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                });
            }

            return new PagingResponse<ApartmentViewDto>
            {
                ListData = apartments,
                TotalRecord = totalCount,
                PageSize = filter.PageSize,
                CurrentPage = filter.PageIndex
            };
        }

        public async Task<Apartment?> UpdateAsync(Apartment request)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", request.Id),
                new SqlParameter("@ApartmentTypeId", request.ApartmentTypeId),
                new SqlParameter("@Name", request.Name),
                new SqlParameter("@Address", request.Address),
                new SqlParameter("@FloorCount", (object?)request.FloorCount ?? DBNull.Value),
                new SqlParameter("@Price", request.Price),
            };

            var dataTable = await _dbWorker.ExecuteStoredProcedureAsync(StoredProcedures.Apartment.Update_Apartment, parameters);

            if (dataTable.Rows.Count == 0)
                return null;

            var row = dataTable.Rows[0];
            return new Apartment
            {
                Id = Convert.ToInt32(row["Id"]),
                ApartmentTypeId = Convert.ToInt32(row["ApartmentTypeId"]),
                Name = row["Name"].ToString() ?? string.Empty,
                Address = row["Address"].ToString() ?? string.Empty,
                FloorCount = row["FloorCount"] == DBNull.Value ? null : Convert.ToInt32(row["FloorCount"]),
                Price = Convert.ToDecimal(row["Price"]),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"])
            };
        }
    }
}
