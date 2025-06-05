using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Shared.DTOs.Response.Apartment
{
    /// <summary>
    /// Data Transfer Object (DTO) dùng để hiển thị thông tin căn hộ lên giao diện người dùng.
    /// Lớp này kết hợp dữ liệu từ bảng Căn Hộ và Loại Căn Hộ để phục vụ việc hiển thị.
    /// </summary>
    public class ApartmentViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int? FloorCount { get; set; }
        public decimal Price { get; set; }

        /// <summary>
        /// Tên loại căn hộ (ví dụ: "Studio", "2 phòng ngủ", "Penthouse").
        /// Thường lấy từ bảng loại căn hộ (ApartmentType).
        /// </summary>
        public string NameApartmentType { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
