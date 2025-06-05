using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Domain.Entities
{
    /// <summary>
    /// Đại diện cho người dùng trong hệ thống
    /// </summary>
    public class User
    {
        // Trường private lưu Id người dùng
        private int _id;

        // Trường private lưu tên đăng nhập
        private string _username;

        // Trường private lưu mật khẩu đã mã hóa
        private string _passwordHash;

        // Trường private lưu họ và tên đầy đủ
        private string _fullName;

        // Trường private lưu trạng thái hoạt động (true: hoạt động, false: bị khóa)
        private bool _isActive;

        private int _roleId;


        public int RoleId 
        {
            get => _roleId;
            set => _roleId = value;
        }

        /// <summary>
        /// Id người dùng
        /// </summary>
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// Tên đăng nhập của người dùng
        /// </summary>
        public string Username
        {
            get => _username;
            set => _username = value;
        }

        /// <summary>
        /// Mật khẩu đã được mã hóa (hash)
        /// </summary>
        public string PasswordHash
        {
            get => _passwordHash;
            set => _passwordHash = value;
        }

        /// <summary>
        /// Họ và tên đầy đủ của người dùng
        /// </summary>
        public string FullName
        {
            get => _fullName;
            set => _fullName = value;
        }

        /// <summary>
        /// Trạng thái hoạt động của người dùng
        /// </summary>
        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }
    }

}
