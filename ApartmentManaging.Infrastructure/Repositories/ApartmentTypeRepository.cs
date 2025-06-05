using Microsoft.Data.SqlClient;
using System.Data;
using ApartmentManaging.Domain.Interfaces.BaseInterfaces;
using ApartmentManaging.Infrastructure.Data;
using ApartmentManaging.Shared.DTOs.Common;
using ApartmentManaging.Domain.Entities;

namespace ApartmentManaging.Infrastructure.Repositories
{
    public class ApartmentTypeRepository :
        IGetPagedRepository<PagingRequest, PagingResponse<ApartmentType>>,
        IBaseRepository<ApartmentType>
    {
        private readonly DbWorker _dbWorker;

        public ApartmentTypeRepository(DbWorker dbWorker)
        {
            _dbWorker = dbWorker ?? throw new ArgumentNullException(nameof(dbWorker));
        }

        public async Task<ApartmentType?> AddAsync(ApartmentType request)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", request.Name ?? (object)DBNull.Value),
                new SqlParameter("@Description", request.Description ?? (object)DBNull.Value)
            };

            var dt = await _dbWorker.ExecuteStoredProcedureAsync(StoredProcedures.ApartmentType.Add_Apartment_Type, parameters);

            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];

            return new ApartmentType
            {
                Id = row.Field<int>("Id"),
                Name = row.Field<string>("Name"),
                Description = row.Field<string?>("Description")
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            return await _dbWorker.ExecuteNonQueryAsync(StoredProcedures.ApartmentType.Delete_Apartment_Type, parameters);
        }

        public async Task<ApartmentType?> GetByIdAsync(int? id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            // Gọi SP lấy dữ liệu 1 bản ghi theo Id
            var dt = await _dbWorker.ExecuteStoredProcedureAsync(StoredProcedures.ApartmentType.Get_ApartmentType_By_Id, parameters);

            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];

            return new ApartmentType
            {
                Id = row.Field<int>("Id"),
                Name = row.Field<string>("Name"),
                Description = row.Field<string?>("Description")
            };
        }


        public async Task<PagingResponse<ApartmentType>> GetPagedAsync(PagingRequest filter)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@PageIndex", filter.PageIndex),
                new SqlParameter("@PageSize", filter.PageSize)
            };

            var ds = await _dbWorker.ExecuteStoredProcedureWithMultipleResultsAsync(
                StoredProcedures.ApartmentType.Get_Paged_ApartmentTypes,
                parameters);

            var result = new PagingResponse<ApartmentType>();

            if (ds.Tables.Count >= 2)
            {
                var dtData = ds.Tables[0];
                var dtTotal = ds.Tables[1];

                var list = dtData.AsEnumerable()
                .Select(row => new ApartmentType
                {
                    Id = row.Field<int>("Id"),
                    Name = row.Field<string>("Name"),
                    Description = row.Field<string>("Description")
                }).ToList();

                result.ListData = list;

                if (dtTotal.Rows.Count > 0 && int.TryParse(dtTotal.Rows[0][0]?.ToString(), out int total))
                {
                    result.TotalRecord = total;
                }
            }

            return result;
        }

        public async Task<ApartmentType?> UpdateAsync(ApartmentType request)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", request.Id),
                new SqlParameter("@Name", request.Name ?? (object)DBNull.Value),
                new SqlParameter("@Description", request.Description ?? (object)DBNull.Value)
            };

            var dt = await _dbWorker.ExecuteStoredProcedureAsync(StoredProcedures.ApartmentType.Update_Apartment_Type, parameters);

            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];

            return new ApartmentType
            {
                Id = row.Field<int>("Id"),
                Name = row.Field<string>("Name"),
                Description = row.Field<string?>("Description")
            };
        }
    }
}
