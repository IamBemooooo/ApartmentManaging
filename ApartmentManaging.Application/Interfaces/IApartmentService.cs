using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Shared.DTOs.Common;
using ApartmentManaging.Shared.DTOs.Requests.Apartment;
using ApartmentManaging.Shared.DTOs.Response.Apartment;

namespace ApartmentManaging.Application.Interfaces
{
    /// <summary>
    /// Interface định nghĩa các phương thức xử lý nghiệp vụ liên quan đến căn hộ.
    /// </summary>
    public interface IApartmentService
    {
        /// <summary>
        /// Tạo mới một căn hộ bất đồng bộ.
        /// </summary>
        /// <param name="apartment">Đối tượng chứa thông tin căn hộ cần tạo.</param>
        /// <returns>Trả về <c>true</c> nếu tạo thành công, ngược lại <c>false</c>.</returns>
        Task<Apartment?> CreateApartmentAsync(ApartmentCreateDto apartment);

        /// <summary>
        /// Cập nhật thông tin căn hộ bất đồng bộ.
        /// </summary>
        /// <param name="apartment">Đối tượng chứa thông tin căn hộ cần cập nhật.</param>
        /// <returns>Trả về <c>true</c> nếu cập nhật thành công, ngược lại <c>false</c>.</returns>
        Task<Apartment?> UpdateApartmentAsync(ApartmentUpdateDto apartment);

        /// <summary>
        /// Xóa căn hộ theo ID bất đồng bộ.
        /// </summary>
        /// <param name="apartmentId">ID căn hộ cần xóa.</param>
        /// <returns>Trả về <c>true</c> nếu xóa thành công, ngược lại <c>false</c>.</returns>
        Task<bool> DeleteApartmentByIdAsync(int apartmentId);

        /// <summary>
        /// Lấy thông tin căn hộ theo ID bất đồng bộ.
        /// </summary>
        /// <param name="apartmentId">ID căn hộ cần lấy thông tin.</param>
        /// <returns>Trả về đối tượng <see cref="Apartment"/> nếu tìm thấy, ngược lại trả về <c>null</c>.</returns>
        Task<Apartment?> GetApartmentByIdAsync(int apartmentId);

        /// <summary>
        /// Lấy danh sách căn hộ phân trang bất đồng bộ.
        /// </summary>
        /// <param name="pagingRequest">Đối tượng chứa thông tin phân trang và các điều kiện lọc.</param>
        /// <returns>Trả về đối tượng <see cref="PagingResponse{ApartmentViewDto}"/> chứa dữ liệu phân trang.</returns>
        Task<PagingResponse<ApartmentViewDto>> GetPagedApartmentsAsync(PagingRequest pagingRequest);
    }
}
