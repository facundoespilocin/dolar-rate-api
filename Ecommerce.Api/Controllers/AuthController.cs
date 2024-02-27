using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ecommerce.DataAccessLayer.Models;
using Newtonsoft.Json;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUsersService _usersService;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IUsersService usersService, IAuthService authService)
        {
            _logger = logger;
            _usersService = usersService;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(JsonConvert.SerializeObject(ModelState.Values.Select(e => e.Errors).ToList()));
            }

            var response = await _authService.Authenticate(request);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = response.RefreshToken.Expires
            };

            Response.Cookies.Append("refreshToken", response.RefreshToken.Token, cookieOptions);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var response = await _authService.RefreshToken(refreshToken);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = response.RefreshToken.Expires
            };

            Response.Cookies.Append("refreshToken", response.RefreshToken.Token, cookieOptions);

            return Ok(response.UserData);
        }
    }
}