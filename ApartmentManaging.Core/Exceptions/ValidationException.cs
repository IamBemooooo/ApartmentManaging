using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Shared.Exceptions
{
    /// <summary>
    /// Exception dùng để biểu thị lỗi xác thực dữ liệu (validation).
    /// Thường được sử dụng khi dữ liệu người dùng nhập vào không hợp lệ theo các quy tắc kiểm tra đầu vào.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Khởi tạo một exception xác thực với thông báo lỗi cụ thể.
        /// </summary>
        /// <param name="message">Thông báo lỗi mô tả lý do dữ liệu không hợp lệ.</param>
        public ValidationException(string message) : base(message) { }
    }
}
