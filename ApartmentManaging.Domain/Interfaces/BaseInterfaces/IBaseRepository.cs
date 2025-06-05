using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Domain.Interfaces.BaseInterfaces
{
    /// <summary>
    /// Interface định nghĩa các thao tác CRUD cơ bản cho entity.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu đối tượng entity thao tác.</typeparam>
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Thêm mới một thực thể bất đồng bộ.
        /// </summary>
        /// <param name="request">Đối tượng thực thể cần thêm.</param>
        /// <returns>Trả về <c>true</c> nếu thêm thành công, ngược lại <c>false</c>.</returns>
        Task<TEntity?> AddAsync(TEntity request);

        /// <summary>
        /// Cập nhật thông tin một thực thể bất đồng bộ.
        /// </summary>
        /// <param name="request">Đối tượng thực thể chứa dữ liệu cập nhật.</param>
        /// <returns>Trả về <c>true</c> nếu cập nhật thành công, ngược lại <c>false</c>.</returns>
        Task<TEntity?> UpdateAsync(TEntity request);

        /// <summary>
        /// Xóa một thực thể theo ID bất đồng bộ.
        /// </summary>
        /// <param name="id">ID của thực thể cần xóa.</param>
        /// <returns>Trả về <c>true</c> nếu xóa thành công, ngược lại <c>false</c>.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Lấy thông tin một thực thể theo ID bất đồng bộ.
        /// </summary>
        /// <param name="id">ID của thực thể cần lấy.</param>
        /// <returns>Trả về thực thể nếu tìm thấy, ngược lại trả về <c>null</c>.</returns>
        Task<TEntity?> GetByIdAsync(int? id);
    }
}
