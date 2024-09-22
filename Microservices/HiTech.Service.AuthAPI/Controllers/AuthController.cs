using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HiTech.Service.AuthAPI.Controllers
{
    [Route("api/hitech/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            AuthResponse? response = await _authService.Login(login);

            if (response != null)
            {
                return Ok(new { 
                            message = "Logged in successfully",
                            data = response
                        });
            }
            return Unauthorized(new { message = "Invalid username or password" });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            AuthResponse? response = await _authService.RefreshToken(refreshToken);

            if (response != null)
            {
                return Ok(new {
                            message = "Refresh successfully",
                            data = response
                        });
            }
            return Unauthorized(new { message = "Invalid refresh token" });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool response = await _authService.Logout(id, request);

            if (response)
            {
                return Ok(new { message = "Logged out successfully" });
            }
            return BadRequest(new { message = "Invalid token" });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            AccountResponse? response = await _authService.GetProfile(id);

            if (response != null)
            {
                return Ok(new
                {
                    message = "success",
                    data = response
                });
            }
            return NotFound(new { message = "User not found" });
        }

    }
}
