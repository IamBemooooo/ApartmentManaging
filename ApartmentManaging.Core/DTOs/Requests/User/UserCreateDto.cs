using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Shared.DTOs.Requests.User
{
    /// <summary>
    /// DTO dùng để tạo mới một người dùng.
    /// </summary>
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên đăng nhập phải có từ 3 đến 100 ký tự.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [StringLength(256, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        // Lưu ý: Trong thực tế, bạn sẽ hash mật khẩu trước khi lưu vào PasswordHash
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ và tên đầy đủ là bắt buộc.")]
        [StringLength(255, ErrorMessage = "Họ và tên không được vượt quá 255 ký tự.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "ID vai trò là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID vai trò phải là số nguyên dương.")]
        public int RoleId { get; set; }
    }
}
