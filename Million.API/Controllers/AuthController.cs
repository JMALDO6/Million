using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Million.Domain.Entities;
using Million.Domain.Settings;
using Million.Infrastructure.Security.Jwt;

namespace Million.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="jwtOptions"></param>
        public AuthController(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtOptions)
        {
            _userManager = userManager;
            _jwtSettings = jwtOptions.Value;
        }

        /// <summary>
        /// Login endpoint to authenticate users and issue JWT tokens.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return Unauthorized("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);
            var token = TokenGenerator.GenerateJwtToken(user, roles, _jwtSettings);

            return Ok(new { token });
        }
    }
}
