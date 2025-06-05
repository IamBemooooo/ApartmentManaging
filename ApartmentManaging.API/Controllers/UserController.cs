using ApartmentManaging.Application.Interfaces;
using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Shared.DTOs.Requests.User;
using ApartmentManaging.Shared.DTOs.Response.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentManaging.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Đăng ký người dùng mới
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(typeof(APIResponse<User>), 200)]
        [ProducesResponseType(typeof(APIResponse<User>), 400)]
        public async Task<IActionResult> Register([FromBody] UserCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(new APIResponse<User>(400, "Dữ liệu không hợp lệ", null)
                {
                    Errors = errors
                });
            }

            var user = await _userService.RegisterUserAsync(dto);
            return Ok(new APIResponse<User>(200, "Đăng ký thành công", user));
        }

        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        [HttpPut("update")]
        [ProducesResponseType(typeof(APIResponse<User>), 200)]
        [ProducesResponseType(typeof(APIResponse<User>), 400)]
        [ProducesResponseType(typeof(APIResponse<User>), 404)]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(new APIResponse<User>(400, "Dữ liệu không hợp lệ", null)
                {
                    Errors = errors
                });
            }

            try
            {
                var updatedUser = await _userService.UpdateUserAsync(dto);
                return Ok(new APIResponse<User>(200, "Cập nhật thành công", updatedUser));
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponse<User>(404, ex.Message, null));
            }
        }

        /// <summary>
        /// Xóa người dùng theo ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(APIResponse<bool>), 200)]
        [ProducesResponseType(typeof(APIResponse<bool>), 404)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                return Ok(new APIResponse<bool>(200, "Xóa người dùng thành công", result));
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponse<bool>(404, ex.Message, false));
            }
        }

        /// <summary>
        /// Lấy thông tin người dùng theo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(APIResponse<User>), 200)]
        [ProducesResponseType(typeof(APIResponse<User>), 404)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new APIResponse<User>(404, $"Không tìm thấy người dùng với ID = {id}", null));
            }

            return Ok(new APIResponse<User>(200, "Lấy người dùng thành công", user));
        }
    }
}
