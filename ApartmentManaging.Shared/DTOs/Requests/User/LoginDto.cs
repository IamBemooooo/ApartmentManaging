using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Shared.DTOs.Requests.Authentication
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        public string? PasswordHash { get; set; }
    }
}
