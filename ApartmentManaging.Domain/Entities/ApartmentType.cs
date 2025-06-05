using System.ComponentModel.DataAnnotations;

namespace ApartmentManaging.Domain.Entities
{
    /// <summary>
    /// Đại diện cho loại căn hộ trong hệ thống
    /// </summary>
    public class ApartmentType
    {
        // Trường private lưu Id loại căn hộ
        private int? _id;

        // Trường private lưu tên loại căn hộ
        private string _name;

        // Trường private lưu mô tả loại căn hộ (nếu có)
        private string? _description;

        /// <summary>
        /// Id loại căn hộ
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Id phải lớn hơn hoặc bằng 1")]
        public int? Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// Tên loại căn hộ
        /// </summary>
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Mô tả loại căn hộ (tối đa 150 ký tự)
        /// </summary>
        [StringLength(150, ErrorMessage = "Mô tả không được vượt quá 150 ký tự")]
        public string? Description
        {
            get => _description;
            set => _description = value;
        }
    }
}
