using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Shared.DTOs.Requests.Apartment
{
    /// <summary>
    /// DTO dùng để tạo mới thông tin căn hộ.
    /// Được sử dụng khi nhận dữ liệu từ client (UI/API) để thêm căn hộ vào hệ thống.
    /// Bao gồm các thông tin bắt buộc như loại căn hộ, tên, địa chỉ, giá và một số thông tin tùy chọn.
    /// </summary>
    public class ApartmentCreateDto
    {
        /// <summary>
        /// Mã loại căn hộ (liên kết với bảng loại căn hộ trong cơ sở dữ liệu).
        /// </summary>
        /// <example>1</example>
        /// <remarks>
        /// Bắt buộc. Giá trị tối thiểu là 1. Sử dụng để phân loại căn hộ như: chung cư, studio, penthouse, v.v.
        /// </remarks>
        [Required(ErrorMessage = "Vui lòng chọn loại căn hộ.")]
        [Range(1, int.MaxValue, ErrorMessage = "Loại căn hộ không hợp lệ.")]
        public int ApartmentTypeId { get; set; }

        /// <summary>
        /// Tên căn hộ.
        /// </summary>
        /// <example>Sunrise City View</example>
        /// <remarks>
        /// Bắt buộc. Tối đa 100 ký tự. Là tên hiển thị của căn hộ cho người dùng.

        [Required(ErrorMessage = "Vui lòng nhập tên căn hộ.")]
        [StringLength(100, ErrorMessage = "Tên căn hộ không được vượt quá 100 ký tự.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Địa chỉ cụ thể của căn hộ.
        /// </summary>
        /// <example>123 Nguyễn Văn Linh, Quận 7, TP.HCM</example>
        /// <remarks>
        /// Bắt buộc. Tối đa 200 ký tự. Dùng để xác định vị trí căn hộ.
        /// </remarks>

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự.")]
        public string Address { get; set; } = null!;

        /// <summary>
        /// Số tầng của căn hộ.
        /// </summary>
        /// <example>12</example>
        /// <remarks>
        /// Không bắt buộc. Nếu nhập thì giá trị phải từ 0 đến 200. Dùng để mô tả cấu trúc căn hộ hoặc toà nhà.
        /// </remarks>

        [Range(0, 200, ErrorMessage = "Số tầng phải từ 0 đến 200.")]
        public int? FloorCount { get; set; }

        /// <summary>
        /// Giá căn hộ (theo đơn vị tiền tệ mặc định, thường là VNĐ).
        /// </summary>
        /// <example>2500000000</example>
        /// <remarks>
        /// Bắt buộc. Giá trị phải lớn hơn 0 và nhỏ hơn hoặc bằng 100 tỉ.
        /// </remarks>

        [Required(ErrorMessage = "Vui lòng nhập giá.")]
        [Range(0.01, 100000000000, ErrorMessage = "Giá phải lớn hơn 0 và nhỏ hơn 100 tỉ")]
        public decimal Price { get; set; }
    }
}
