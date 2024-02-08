using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Filters;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("Api/UserManagment")]
    public class UserManagmentController : ControllerBase
    {
        private readonly IUserAppService UserAppService;

        public UserManagmentController(IUserAppService userAppService)
        {
            UserAppService = userAppService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto request)
        {
            var result = await UserAppService.CreateUserAsync(request.Username, request.Email, request.Password);
            return result.Errors is not null ? BadRequest(result.Errors) : Ok("Create User Succeed");
        }

        [JwtTokenAuthorization]
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await UserAppService.GetUserByIdAsync(userId);
            return user == null ? NotFound() : Ok(user);
        }

        [JwtTokenAuthorization]
        [HttpGet("GetUserByUsername")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await UserAppService.GetUserByUsernameAsync(username);
            return user == null ? NotFound() : Ok(user);
        }
    }
}