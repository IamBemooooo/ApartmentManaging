using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ApartmentManaging.Infrastructure.Data
{
    /// <summary>
    /// Lớp DbWorker dùng để thực thi các stored procedure thông qua SqlConnection.
    /// Cung cấp các phương thức: trả về bảng, trả về giá trị đơn, hoặc không trả về gì.
    /// </summary>
    public class DbWorker
    {
        private readonly string _connectionString;

        /// <summary>
        /// Khởi tạo DbWorker với chuỗi kết nối đến database.
        /// </summary>
        /// <param name="connectionString">Chuỗi kết nối đến SQL Server</param>
        /// <exception cref="ArgumentNullException">Ném nếu connectionString null</exception>
        public DbWorker(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        /// <summary>
        /// Thực thi stored procedure và trả về kết quả dưới dạng <see cref="DataTable"/>.
        /// </summary>
        /// <param name="spName">Tên của stored procedure cần thực thi.</param>
        /// <param name="parameters">Danh sách tham số truyền vào stored procedure (có thể null nếu không có).</param>
        /// <returns>
        /// Một <see cref="DataTable"/> chứa dữ liệu kết quả từ stored procedure.
        /// </returns>
        /// <exception cref="SqlException">Ném ra nếu có lỗi trong quá trình thực thi stored procedure.</exception>
        public async Task<DataTable> ExecuteStoredProcedureAsync(string spName, List<SqlParameter>? parameters = null)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
                cmd.Parameters.AddRange(parameters.ToArray());

            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();

            var dt = new DataTable();
            dt.Load(reader);  // Load dữ liệu từ DataReader vào DataTable

            return dt;
        }


        /// <summary>
        /// Thực thi stored procedure và trả về nhiều bảng dữ liệu (DataSet).
        /// </summary>
        /// <param name="spName">Tên stored procedure</param>
        /// <param name="parameters">Danh sách tham số (có thể null)</param>
        /// <returns>DataSet chứa nhiều bảng dữ liệu</returns>
        public async Task<DataSet> ExecuteStoredProcedureWithMultipleResultsAsync(string spName, List<SqlParameter>? parameters = null)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
                cmd.Parameters.AddRange(parameters.ToArray());

            await conn.OpenAsync();

            using var adapter = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            adapter.Fill(ds); // SqlDataAdapter chưa có FillAsync

            return ds;
        }

        /// <summary>
        /// Thực thi stored procedure không trả về dữ liệu (INSERT, UPDATE, DELETE).
        /// </summary>
        /// <param name="spName">Tên stored procedure</param>
        /// <param name="parameters">Danh sách tham số (có thể null)</param>
        /// <returns>True nếu có ít nhất một dòng bị ảnh hưởng</returns>
        public async Task<bool> ExecuteNonQueryAsync(string spName, List<SqlParameter>? parameters = null)
        {
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
                cmd.Parameters.AddRange(parameters.ToArray());

            await conn.OpenAsync();
            var affectedRows = await cmd.ExecuteNonQueryAsync();

            return affectedRows > 0;
        }

        /// <summary>
        /// Thực thi stored procedure và trả về giá trị đơn (ví dụ: ID vừa insert hoặc count()).
        /// </summary>
        /// <param name="spName">Tên stored procedure</param>
        /// <param name="parameters">Danh sách tham số (có thể null)</param>
        /// <returns>Giá trị trả về đầu tiên, có thể null</returns>
        public async Task<object?> ExecuteScalarAsync(string spName, List<SqlParameter>? parameters = null)
        {
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
                cmd.Parameters.AddRange(parameters.ToArray());

            await conn.OpenAsync();
            return await cmd.ExecuteScalarAsync();
        }
    }
}
