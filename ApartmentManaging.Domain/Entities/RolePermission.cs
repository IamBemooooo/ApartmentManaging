using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Domain.Entities
{
    /// <summary>
    /// Bảng liên kết nhiều-nhiều giữa Vai trò và Quyền
    /// </summary>
    public class RolePermission
    {
        // Trường private lưu Id vai trò
        private int _roleId;

        // Trường private lưu Id quyền
        private int _permissionId;

        /// <summary>
        /// Id vai trò
        /// </summary>
        public int RoleId
        {
            get => _roleId;
            set => _roleId = value;
        }

        /// <summary>
        /// Id quyền
        /// </summary>
        public int PermissionId
        {
            get => _permissionId;
            set => _permissionId = value;
        }
    }

}
