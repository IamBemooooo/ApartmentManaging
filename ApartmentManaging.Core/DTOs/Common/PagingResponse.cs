namespace ApartmentManaging.Shared.DTOs.Common
{
    /// <summary>
    /// Lớp đại diện cho phản hồi phân trang khi trả về danh sách dữ liệu.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu của thực thể dữ liệu trong danh sách</typeparam>
    public class PagingResponse<TEntity> where TEntity : class
    {
        /// <summary>
        /// Danh sách dữ liệu thuộc trang hiện tại.
        /// </summary>
        public List<TEntity>? ListData { get; set; }

        /// <summary>
        /// Số trang hiện tại đang được truy vấn (bắt đầu từ 1).
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Số lượng bản ghi trên mỗi trang.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Tổng số trang được tính toán từ tổng số bản ghi và kích thước trang.
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// Tổng số bản ghi thỏa mãn điều kiện truy vấn.
        /// </summary>
        public long TotalRecord { get; set; }
    }

}
