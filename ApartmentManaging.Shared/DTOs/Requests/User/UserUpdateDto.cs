using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Shared.DTOs.Requests.User
{
    /// <summary>
    /// DTO dùng để cập nhật thông tin của một người dùng hiện có.
    /// </summary>
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "ID người dùng là bắt buộc cho việc cập nhật.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID người dùng phải là số nguyên dương.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên đăng nhập phải có từ 3 đến 100 ký tự.")]
        public string Username { get; set; } = string.Empty;

        // Mật khẩu có thể được cập nhật qua DTO này, hoặc qua một DTO riêng biệt như ChangePasswordDto
        // Nếu bạn muốn cho phép cập nhật mật khẩu qua đây:
        [StringLength(256, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ và tên đầy đủ là bắt buộc.")]
        [StringLength(255, ErrorMessage = "Họ và tên không được vượt quá 255 ký tự.")]
        public string FullName { get; set; } = string.Empty;
    }
}
