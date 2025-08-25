using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Shared.DTOs.Requests.Apartment
{
    /// <summary>
    /// DTO dùng để cập nhật thông tin căn hộ hiện có.
    /// Được sử dụng khi gửi yêu cầu cập nhật từ client (UI/API).
    /// Bao gồm các thông tin cần thiết như ID, loại căn hộ, tên, địa chỉ, số tầng và giá.
    /// </summary>
    public class ApartmentUpdateDto
    {
        /// <summary>
        /// Mã định danh (ID) của căn hộ cần cập nhật.
        /// </summary>
        /// <example>12</example>
        /// <remarks>
        /// Bắt buộc. Phải lớn hơn hoặc bằng 1.
        /// Dùng để xác định căn hộ cụ thể trong cơ sở dữ liệu.
        /// </remarks>
        [Range(1, int.MaxValue, ErrorMessage = "Id phải lớn hơn hoặc bằng 1")]
        public int Id { get; set; }

        /// <summary>
        /// Mã loại căn hộ mới.
        /// </summary>
        /// <example>2</example>
        /// <remarks>
        /// Bắt buộc. Dùng để chuyển đổi hoặc xác nhận lại loại căn hộ.
        /// </remarks>

        [Required(ErrorMessage = "Vui lòng chọn loại căn hộ.")]
        [Range(1, int.MaxValue, ErrorMessage = "Loại căn hộ không hợp lệ.")]
        public int ApartmentTypeId { get; set; }

        /// <summary>
        /// Tên mới của căn hộ.
        /// </summary>
        /// <example>Vinhomes Grand Park A3-09</example>
        /// <remarks>
        /// Bắt buộc. Tối đa 100 ký tự.
        /// </remarks>

        [Required(ErrorMessage = "Vui lòng nhập tên căn hộ.")]
        [StringLength(100, ErrorMessage = "Tên căn hộ không được vượt quá 100 ký tự.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Địa chỉ mới của căn hộ.
        /// </summary>
        /// <example>72 Lê Thánh Tôn, Quận 1, TP.HCM</example>
        /// <remarks>
        /// Bắt buộc. Tối đa 200 ký tự.
        /// </remarks>

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự.")]
        public string Address { get; set; } = null!;

        /// <summary>
        /// Số tầng (mới) của căn hộ (nếu thay đổi).
        /// </summary>
        /// <example>25</example>
        /// <remarks>
        /// Không bắt buộc. Nếu có, phải từ 0 đến 200.
        /// </remarks>

        [Range(0, 200, ErrorMessage = "Số tầng phải từ 0 đến 200.")]
        public int? FloorCount { get; set; }

        /// <summary>
        /// Giá mới của căn hộ.
        /// </summary>
        /// <example>3000000000</example>
        /// <remarks>
        /// Bắt buộc. Phải lớn hơn 0 và nhỏ hơn 100 tỉ.
        /// </remarks>
        [Required(ErrorMessage = "Vui lòng nhập giá.")]
        [Range(0.01, 100000000000, ErrorMessage = "Giá phải lớn hơn 0 và nhỏ hơn 100 tỉ")]
        public decimal Price { get; set; }
    }
}
