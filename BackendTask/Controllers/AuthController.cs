
using BackendTask.Models.Users;
using BackendTask.Services.AuthServices;
using Microsoft.AspNetCore.Mvc;

namespace BackendTask.Controllers {

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly IAuthService _service;

        public AuthController(IAuthService service) {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin login) { 
            
            var user = await _service.Authenticate(login);

            if (user != null) {

                var access_token = _service.GenerateAccessToken(user);
                var refresh_token = _service.GenerateRefreshToken(user);

                HttpContext.Response.Headers.Add("Access-Token", access_token);
                HttpContext.Response.Headers.Add("Refresh-Token", refresh_token);

                return Ok();

            } else {
                return NotFound("Username or password is incorrect!");
            }
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> Refresh([FromHeader(Name = "Refresh-token")] string refreshToken) {
            
            var newToken = await _service.RegenerateAccessToken(refreshToken);

            if(newToken == null) {
                return NotFound("Username not found!");
            }
            HttpContext.Response.Headers.Add("Access-Token", newToken);

            return Ok();
        }
    }
}
