namespace ApartmentManaging.Shared.DTOs.Response.API
{
    /// <summary>
    /// Class chung dùng để chuẩn hóa định dạng phản hồi từ API.
    /// Dùng generic để có thể trả về bất kỳ kiểu dữ liệu nào.
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của phần nội dung trả về (Data).</typeparam>
    public class APIResponse<T>
    {
        public int Status { get; set; }           // Mã trạng thái, ví dụ 200, 400
        public string? Message { get; set; }       // Thông điệp
        public T? Data { get; set; }                // Dữ liệu trả về (danh sách, object,...)

        /// <summary>
        /// Các lỗi chi tiết từng field (thường dùng cho validate)
        /// </summary>
        public Dictionary<string, string[]>? Errors { get; set; }


        /// <summary>
        /// Constructor mặc định.
        /// </summary>
        public APIResponse() { }

        /// <summary>
        /// Constructor khởi tạo nhanh với các giá trị đầu vào.
        /// </summary>
        /// <param name="status">Mã trạng thái HTTP.</param>
        /// <param name="message">Thông điệp phản hồi.</param>
        /// <param name="data">Dữ liệu phản hồi.</param>
        public APIResponse(int status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }


}
