using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Domain.Entities
{
    /// <summary>
    /// Đại diện cho quyền (permission) trong hệ thống
    /// </summary>
    public class Permission
    {
        // Trường private lưu Id quyền
        private int _id;

        // Trường private lưu mã quyền (ví dụ: "User.Read")
        private string _code;

        // Đường dẫn hoặc tên API được áp quyền
        private string _apiEndpoint;

        // Hoặc bạn có thể phân biệt method HTTP (GET, POST, ...)
        private string _httpMethod;

        // Trường private lưu mô tả quyền
        private string _description;

        /// <summary>
        /// Id quyền
        /// </summary>
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// Mã quyền (ví dụ: "User.Read")
        /// </summary>
        public string Code
        {
            get => _code;
            set => _code = value;
        }

        

        public string ApiEndpoint
        {
            get => _apiEndpoint;
            set => _apiEndpoint = value;
        }

       
        public string HttpMethod
        {
            get => _httpMethod;
            set => _httpMethod = value;
        }

        /// <summary>
        /// Mô tả quyền
        /// </summary>
        public string Description
        {
            get => _description;
            set => _description = value;
        }
    }

}
