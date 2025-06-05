using ApartmentManaging.Shared.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Domain.Interfaces.BaseInterfaces
{
    /// <summary>
    /// Interface định nghĩa phương thức lấy dữ liệu phân trang.
    /// </summary>
    /// <typeparam name="TFilter">Kiểu dữ liệu chứa thông tin lọc, thường là một class kế thừa từ <see cref="PagingRequest"/>.</typeparam>
    /// <typeparam name="TResult">Kiểu dữ liệu trả về, thường là một kiểu chứa kết quả phân trang (ví dụ: danh sách kèm tổng số trang, tổng số bản ghi, ...).</typeparam>
    public interface IGetPagedRepository<TFilter, TResult>
        where TFilter : PagingRequest
    {
        /// <summary>
        /// Lấy dữ liệu phân trang không đồng bộ dựa trên bộ lọc truyền vào.
        /// </summary>
        /// <param name="filter">Đối tượng chứa thông tin phân trang và các điều kiện lọc.</param>
        /// <returns>Kết quả phân trang dạng bất đồng bộ, trả về kiểu <typeparamref name="TResult"/>.</returns>
        Task<TResult> GetPagedAsync(TFilter filter);
    }
}
