using ApartmentManaging.Application.Interfaces;
using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Shared.DTOs.Common;
using ApartmentManaging.Shared.DTOs.Requests.Apartment;
using ApartmentManaging.Shared.DTOs.Response.Apartment;
using ApartmentManaging.Shared.DTOs.Response.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentManaging.API.Controllers
{
    /// <summary>
    /// Controller quản lý các API liên quan đến căn hộ.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;

        /// <summary>
        /// Khởi tạo ApartmentController với service xử lý nghiệp vụ căn hộ.
        /// </summary>
        /// <param name="apartmentService">Service thao tác với dữ liệu căn hộ.</param>
        /// <exception cref="ArgumentNullException">Ném khi apartmentService là null.</exception>
        public ApartmentController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService ?? throw new ArgumentNullException(nameof(apartmentService));
        }

        /// <summary>
        /// Tạo mới căn hộ.
        /// </summary>
        /// <param name="request">Dữ liệu căn hộ mới (ApartmentCreateDto).</param>
        /// <returns>Trả về kết quả tạo căn hộ, bao gồm trạng thái HTTP và thông điệp.</returns>
        [HttpPost(nameof(CreateApartment))]
        [ProducesResponseType(typeof(APIResponse<Apartment>), 201)]
        [ProducesResponseType(typeof(APIResponse<Apartment?>), 400)]
        [ProducesResponseType(typeof(APIResponse<string>), 400)]
        public async Task<IActionResult> CreateApartment([FromBody] ApartmentCreateDto request)
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


            var result = await _apartmentService.CreateApartmentAsync(request);
            return result != null
                ? Ok(new APIResponse<Apartment>(201, "Tạo căn hộ thành công", result))
                : BadRequest(new APIResponse<Apartment?>(400, "Tạo căn hộ thất bại", result));
        }

        /// <summary>
        /// Cập nhật thông tin căn hộ.
        /// </summary>
        /// <param name="request">Dữ liệu căn hộ cập nhật (ApartmentUpdateDto).</param>
        /// <returns>Trả về kết quả cập nhật căn hộ.</returns>
        [HttpPut(nameof(UpdateApartment))]
        [ProducesResponseType(typeof(APIResponse<Apartment>), 200)]
        [ProducesResponseType(typeof(APIResponse<Apartment?>), 400)]
        [ProducesResponseType(typeof(APIResponse<string>), 400)]
        public async Task<IActionResult> UpdateApartment([FromBody] ApartmentUpdateDto request)
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


            var result = await _apartmentService.UpdateApartmentAsync(request);
            return result != null
                ? Ok(new APIResponse<Apartment>(200, "Cập nhật căn hộ thành công", result))
                : BadRequest(new APIResponse<Apartment?>(400, "Cập nhật thất bại", result));
        }

        /// <summary>
        /// Xóa căn hộ theo Id.
        /// </summary>
        /// <param name="id">Id căn hộ cần xóa (phải > 0).</param>
        /// <returns>Trả về kết quả xóa, hoặc lỗi nếu Id không hợp lệ hoặc căn hộ không tồn tại.</returns>
        [HttpDelete(nameof(DeleteApartment) + "/{id}")]
        [ProducesResponseType(typeof(APIResponse<bool>), 200)]
        [ProducesResponseType(typeof(APIResponse<string>), 400)]
        [ProducesResponseType(typeof(APIResponse<bool>), 404)]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new APIResponse<string>(400, "Id không hợp lệ", null));
            }

            var result = await _apartmentService.DeleteApartmentByIdAsync(id);
            return result
                ? Ok(new APIResponse<bool>(200, "Xóa căn hộ thành công", result))
                : NotFound(new APIResponse<bool>(404, "Không tìm thấy căn hộ để xóa", result));
        }

        /// <summary>
        /// Lấy thông tin căn hộ theo Id.
        /// </summary>
        /// <param name="id">Id căn hộ cần lấy (phải > 0).</param>
        /// <returns>Thông tin căn hộ nếu tìm thấy, hoặc lỗi nếu không tồn tại.</returns>
        [HttpGet(nameof(GetApartmentById) + "/{id}")]
        [ProducesResponseType(typeof(APIResponse<Apartment>), 200)]
        [ProducesResponseType(typeof(APIResponse<string>), 400)]
        [ProducesResponseType(typeof(APIResponse<Apartment>), 404)]
        public async Task<IActionResult> GetApartmentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new APIResponse<string>(400, "Id không hợp lệ", null));
            }

            var apartment = await _apartmentService.GetApartmentByIdAsync(id);

            return apartment != null
                ? Ok(new APIResponse<Apartment>(200, "Lấy thông tin căn hộ thành công", apartment))
                : NotFound(new APIResponse<Apartment>(404, "Không tìm thấy căn hộ", null));
        }

        /// <summary>
        /// Lấy danh sách căn hộ theo phân trang.
        /// </summary>
        /// <param name="request">Thông tin phân trang (PageIndex, PageSize).</param>
        /// <returns>Danh sách căn hộ đã phân trang kèm theo thông tin phân trang.</returns>
        [HttpGet(nameof(GetPagedApartments))]
        [ProducesResponseType(typeof(APIResponse<PagingResponse<ApartmentViewDto>>), 200)]
        [ProducesResponseType(typeof(APIResponse<string>), 400)]
        public async Task<IActionResult> GetPagedApartments([FromQuery] PagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                // Lấy lỗi theo từng field 
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


            var result = await _apartmentService.GetPagedApartmentsAsync(request);
            return Ok(new APIResponse<PagingResponse<ApartmentViewDto>>(200, "Lấy danh sách căn hộ thành công", result));
        }
    }
}
