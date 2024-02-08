using Domain.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly SignInManager<User> UserSignInManager;
        private readonly UserManager<User> UserManager;
        private readonly IConfiguration Configuration;

        public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            UserManager = userManager;
            UserSignInManager = signInManager;
            Configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var user = await UserManager.FindByNameAsync(username);
            if (!await UserManager.CheckPasswordAsync(user, password))
                return Unauthorized();

            var res = await UserSignInManager.PasswordSignInAsync(user, password, false, false);
            if (!res.Succeeded)
                return Unauthorized();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(Configuration["Jwt:Issuer"],
              Configuration["Jwt:Audience"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await UserSignInManager.SignOutAsync();
            return Ok();
        }
    }
}
