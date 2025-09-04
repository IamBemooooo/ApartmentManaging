using ApartmentManaging.Application.Interfaces;
using ApartmentManaging.Shared.DTOs.Requests.Authentication;
using ApartmentManaging.Shared.DTOs.Response.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentManaging.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(APIResponse<string>), 200)]
        [ProducesResponseType(typeof(APIResponse<string>), 400)]
        [ProducesResponseType(typeof(APIResponse<string>), 401)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                var fullMessage = "Invalid data. Please check again.";

                return BadRequest(new APIResponse<string>(400, fullMessage, null)
                {
                    Errors = errors
                });
            }

            var token = await _authService.LoginAsync(loginDto);

            if (token == null)
                return Unauthorized(new APIResponse<string>(401, "Invalid username or password", null));

            return Ok(new APIResponse<string>(200, "Login successfully!", token));
        }

    }
}
