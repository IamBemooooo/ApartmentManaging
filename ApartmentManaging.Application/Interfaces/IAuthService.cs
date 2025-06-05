using ApartmentManaging.Shared.DTOs.Requests.Authentication;

namespace ApartmentManaging.Application.Interfaces
{
    /// <summary>
    /// Giao diện định nghĩa các chức năng xác thực người dùng (authentication).
    /// Tầng Application chỉ định nghĩa hành vi, không chứa logic cụ thể.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Đăng nhập người dùng.
        /// </summary>
        /// <param name="dto">Thông tin đăng nhập (username, password)</param>
        /// <returns>
        /// Trả về chuỗi JWT token nếu đăng nhập thành công,
        /// hoặc null nếu thất bại.
        /// </returns>
        Task<string?> LoginAsync(LoginDto dto);
    }

}
