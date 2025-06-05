using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Shared.DTOs.Common;
using ApartmentManaging.Shared.DTOs.Requests.ApartmentType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Application.Interfaces
{
    /// <summary>
    /// Interface định nghĩa các phương thức xử lý nghiệp vụ liên quan đến loại căn hộ.
    /// </summary>
    public interface IApartmentTypeService
    {
        /// <summary>
        /// Thêm mới một loại căn hộ bất đồng bộ.
        /// </summary>
        /// <param name="request">Đối tượng chứa thông tin loại căn hộ cần tạo.</param>
        /// <returns>Trả về <c>true</c> nếu thêm thành công, ngược lại <c>false</c>.</returns>
        Task<ApartmentType?> AddApartmentTypeAsync(ApartmentTypeCreateDto request);

        /// <summary>
        /// Cập nhật thông tin loại căn hộ bất đồng bộ.
        /// </summary>
        /// <param name="request">Đối tượng loại căn hộ cần cập nhật (thường là entity hoặc DTO cập nhật).</param>
        /// <returns>Trả về <c>true</c> nếu cập nhật thành công, ngược lại <c>false</c>.</returns>
        Task<ApartmentType?> UpdateApartmentTypeAsync(ApartmentType request);

        /// <summary>
        /// Xóa loại căn hộ theo ID bất đồng bộ.
        /// </summary>
        /// <param name="id">ID của loại căn hộ cần xóa.</param>
        /// <returns>Trả về <c>true</c> nếu xóa thành công, ngược lại <c>false</c>.</returns>
        Task<bool> DeleteApartmentTypeAsync(int id);

        /// <summary>
        /// Lấy danh sách loại căn hộ phân trang bất đồng bộ.
        /// </summary>
        /// <param name="filter">Đối tượng chứa thông tin phân trang và điều kiện lọc.</param>
        /// <returns>Trả về đối tượng <see cref="PagingResponse{ApartmentType}"/> chứa dữ liệu phân trang.</returns>
        Task<PagingResponse<ApartmentType>> GetPagedApartmentTypesAsync(PagingRequest filter);
    }
}
