using System.ComponentModel.DataAnnotations;

namespace ApartmentManaging.Shared.DTOs.Requests.ApartmentType
{
    /// <summary>
    /// DTO dùng để tạo mới loại căn hộ.
    /// Dữ liệu này sẽ được gửi từ client khi thêm loại căn hộ mới.
    /// </summary>
    public class ApartmentTypeCreateDto
    {
        /// <summary>
        /// Tên của loại căn hộ.
        /// </summary>
        /// <example>Studio</example>
        /// <remarks>
        /// Bắt buộc. Không được để trống.
        /// Ví dụ: "Studio", "1 phòng ngủ", "Penthouse".
        /// </remarks>
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Mô tả chi tiết về loại căn hộ.
        /// </summary>
        /// <example>Loại căn hộ dành cho 1 người, diện tích nhỏ.</example>
        /// <remarks>
        /// Không bắt buộc. Tối đa 150 ký tự.
        /// Dùng để hiển thị chi tiết hoặc giải thích cho người dùng.
        /// </remarks>
        [StringLength(150, ErrorMessage = "Mô tả không được vượt quá 150 ký tự")]
        public string? Description { get; set; }
    }

}
