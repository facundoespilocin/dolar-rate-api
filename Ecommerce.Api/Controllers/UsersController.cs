using Microsoft.AspNetCore.Mvc;
using Ecommerce.DataAccessLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Ecommerce.DataAccessLayer.Models;
using Newtonsoft.Json;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _usersService;

        public UsersController(ILogger<UsersController> logger, IUsersService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _usersService.GetAll();

            return Ok(result);
        }

        [HttpGet("id/{userId}")]
        public async Task<IActionResult> GetById(int userId)
        {
            var result = await _usersService.GetById(userId);

            return Ok(result);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetCustomerByEmail(string email)
        {
            var result = await _usersService.GetCustomerByEmail(email);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest user)
        {
            var result = await _usersService.Create(user);

            if (result.Metadata.Status == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            await _usersService.UpdateUser(user);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(JsonConvert.SerializeObject(ModelState.Values.Select(e => e.Errors).ToList()));
            }

            var response = await _usersService.UpdateUserPassword(request);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(JsonConvert.SerializeObject(ModelState.Values.Select(e => e.Errors).ToList()));
            }

            await _usersService.PostForgotPassword(request.Email);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("confirm-account/{token}")]
        public async Task<IActionResult> ConfirmAccount(string token)
        {
            var confirmation = await _usersService.ConfirmAccount(token);

            return Ok(confirmation);
        }
    }
}