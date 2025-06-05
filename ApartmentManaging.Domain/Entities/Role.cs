using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Domain.Entities
{
    /// <summary>
    /// Đại diện cho vai trò (role) trong hệ thống
    /// </summary>
    public class Role
    {
        // Trường private lưu Id vai trò
        private int _id;

        // Trường private lưu tên vai trò
        private string _name;

        // Trường private lưu mô tả vai trò
        private string _description;

        /// <summary>
        /// Id vai trò
        /// </summary>
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// Tên vai trò
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Mô tả vai trò
        /// </summary>
        public string Description
        {
            get => _description;
            set => _description = value;
        }
    }

}
