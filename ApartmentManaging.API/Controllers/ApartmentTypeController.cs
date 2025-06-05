using ApartmentManaging.Application.Interfaces;
using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Shared.DTOs.Common;
using ApartmentManaging.Shared.DTOs.Requests.ApartmentType;
using ApartmentManaging.Shared.DTOs.Response.API;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentManaging.API.Controllers
{
    /// <summary>
    /// Controller quản lý các API liên quan đến loại căn hộ.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentTypeController : ControllerBase
    {
        private readonly IApartmentTypeService _apartmentTypeService;

        /// <summary>
        /// Khởi tạo ApartmentTypeController với service thao tác loại căn hộ.
        /// </summary>
        /// <param name="apartmentTypeService">Service xử lý nghiệp vụ loại căn hộ.</param>
        /// <exception cref="ArgumentNullException">Ném khi apartmentTypeService là null.</exception>
        public ApartmentTypeController(IApartmentTypeService apartmentTypeService)
        {
            _apartmentTypeService = apartmentTypeService ?? throw new ArgumentNullException(nameof(apartmentTypeService));
        }

        /// <summary>
        /// Thêm mới loại căn hộ.
        /// </summary>
        /// <param name="request">Dữ liệu loại căn hộ cần thêm (ApartmentTypeCreateDto).</param>
        /// <returns>Kết quả thêm mới, bao gồm trạng thái HTTP và thông điệp.</returns>
        [HttpPost(nameof(AddApartmentType))]
        public async Task<IActionResult> AddApartmentType([FromBody] ApartmentTypeCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                // Lấy lỗi theo từng field (key)
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,                            // tên field
                        kvp => kvp.Value.Errors
                                    .Select(e => e.ErrorMessage)
                                    .ToArray()                      // mảng lỗi cho field
                    );

                var fullMessage = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.";

                // Trả về response chuẩn với Errors chi tiết
                return BadRequest(new APIResponse<string>(400, fullMessage, null)
                {
                    Errors = errors
                });
            }


            var result = await _apartmentTypeService.AddApartmentTypeAsync(request);
            return result != null
                ? Ok(new APIResponse<ApartmentType>(201, "Thêm loại căn hộ thành công", result))
                : BadRequest(new APIResponse<ApartmentType?>(400, "Thêm thất bại", result));
        }

        /// <summary>
        /// Cập nhật loại căn hộ.
        /// </summary>
        /// <param name="request">Dữ liệu loại căn hộ cần cập nhật (ApartmentType).</param>
        /// <returns>Kết quả cập nhật.</returns>
        [HttpPut(nameof(UpdateApartmentType))]
        public async Task<IActionResult> UpdateApartmentType([FromBody] ApartmentType request)
        {
            if (!ModelState.IsValid)
            {
                // Lấy lỗi theo từng field (key)
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,                            // tên field
                        kvp => kvp.Value.Errors
                                    .Select(e => e.ErrorMessage)
                                    .ToArray()                      // mảng lỗi cho field
                    );

                var fullMessage = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.";

                // Trả về response chuẩn với Errors chi tiết
                return BadRequest(new APIResponse<string>(400, fullMessage, null)
                {
                    Errors = errors
                });
            }


            var result = await _apartmentTypeService.UpdateApartmentTypeAsync(request);
            return result != null
                ? Ok(new APIResponse<ApartmentType>(200, "Cập nhật thành công", result))
                : BadRequest(new APIResponse<ApartmentType?>(400, "Cập nhật thất bại", result));
        }

        /// <summary>
        /// Xóa loại căn hộ theo Id.
        /// </summary>
        /// <param name="id">Id loại căn hộ cần xóa (phải > 0).</param>
        /// <returns>Kết quả xóa thành công hoặc thất bại.</returns>
        [HttpDelete(nameof(DeleteApartmentType) + "/{id}")]
        public async Task<IActionResult> DeleteApartmentType(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new APIResponse<string>(400, "Id không hợp lệ", null));
            }

            var result = await _apartmentTypeService.DeleteApartmentTypeAsync(id);
            return result
                ? Ok(new APIResponse<bool>(200, "Xóa thành công", result))
                : BadRequest(new APIResponse<bool>(400, "Xóa thất bại", result));
        }

        /// <summary>
        /// Lấy danh sách loại căn hộ theo phân trang.
        /// </summary>
        /// <param name="request">Thông tin phân trang (PagingRequest).</param>
        /// <returns>Danh sách loại căn hộ đã phân trang.</returns>
        [HttpGet(nameof(GetPagedApartmentType))]
        public async Task<IActionResult> GetPagedApartmentType([FromQuery] PagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                // Lấy lỗi theo từng field (key)
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,                            // tên field
                        kvp => kvp.Value.Errors
                                    .Select(e => e.ErrorMessage)
                                    .ToArray()                      // mảng lỗi cho field
                    );

                var fullMessage = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.";

                // Trả về response chuẩn với Errors chi tiết
                return BadRequest(new APIResponse<string>(400, fullMessage, null)
                {
                    Errors = errors
                });
            }


            var pagedData = await _apartmentTypeService.GetPagedApartmentTypesAsync(request);
            return Ok(new APIResponse<PagingResponse<ApartmentType>>(200, "Lấy dữ liệu thành công", pagedData));
        }
    }
}

