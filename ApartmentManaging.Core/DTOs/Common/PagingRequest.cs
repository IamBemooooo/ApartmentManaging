using System.ComponentModel.DataAnnotations;
namespace ApartmentManaging.Shared.DTOs.Common
{
    /// <summary>
    /// Lớp đại diện cho yêu cầu phân trang khi truy xuất dữ liệu từ cơ sở dữ liệu hoặc API.
    /// Dùng để chỉ định trang hiện tại và số lượng bản ghi trên mỗi trang.
    /// </summary>
    public class PagingRequest
    {
        /// <summary>
        /// Số trang hiện tại
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "PageIndex phải lớn hơn hoặc bằng 1")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Số lượng bản ghi trên mỗi trang
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "PageSize phải lớn hơn hoặc bằng 1")]
        public int PageSize { get; set; } = 10;
    }
}
