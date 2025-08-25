using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Shared.Exceptions
{
    /// <summary>
    /// Exception đại diện cho lỗi nghiệp vụ (business logic).
    /// Được dùng để throw ra khi có điều kiện nghiệp vụ không hợp lệ, không thuộc lỗi hệ thống.
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// Khởi tạo một exception với thông báo lỗi nghiệp vụ.
        /// </summary>
        /// <param name="message">Thông báo lỗi sẽ được trả về cho client hoặc log.</param>
        public BusinessException(string message) : base(message) { }
    }
}
