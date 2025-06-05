using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Shared.DTOs.Requests.Authentication;
using ApartmentManaging.Shared.DTOs.Requests.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Application.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Thêm một người dùng mới vào hệ thống.
        /// </summary>
        /// <param name="user">Đối tượng User chứa thông tin người dùng cần thêm.</param>
        /// <returns>Đối tượng User đã được thêm với ID và các thông tin khác từ DB, hoặc null nếu thêm thất bại.</returns>
        Task<User?> RegisterUserAsync(UserCreateDto user);

        /// <summary>
        /// Cập nhật thông tin của một người dùng hiện có.
        /// </summary>
        /// <param name="user">Đối tượng User chứa thông tin cập nhật (phải có Id).</param>
        /// <returns>Đối tượng User đã được cập nhật, hoặc null nếu không tìm thấy người dùng để cập nhật.</returns>
        Task<User?> UpdateUserAsync(UserUpdateDto user);

        /// <summary>
        /// Xóa một người dùng khỏi hệ thống dựa trên ID.
        /// </summary>
        /// <param name="userId">ID của người dùng cần xóa.</param>
        /// <returns>True nếu xóa thành công, ngược lại là false.</returns>
        Task<bool> DeleteUserAsync(int userId);

        /// <summary>
        /// Lấy thông tin người dùng theo ID.
        /// </summary>
        /// <param name="userId">ID của người dùng cần lấy.</param>
        /// <returns>Đối tượng User nếu tìm thấy, ngược lại là null.</returns>
        Task<User?> GetUserByIdAsync(int userId);
    }
}
