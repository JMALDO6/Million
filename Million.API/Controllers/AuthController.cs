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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthController> _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="jwtOptions"></param>
        /// <param name="logger"></param>
        public AuthController(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtOptions, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _jwtSettings = jwtOptions.Value;
            _logger = logger;
        }

        /// <summary>
        /// Login endpoint to authenticate users and issue JWT tokens.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation("Login attempt for user: {Email}", request.Email);
            var user = await _userManager.FindByNameAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                _logger.LogWarning("Invalid login attempt for user: {Email}", request.Email);
                return Unauthorized("Invalid credentials");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = TokenGenerator.GenerateJwtToken(user, roles, _jwtSettings);
            _logger.LogInformation("User {Email} authenticated successfully", request.Email);

            return Ok(new { token });
        }
    }
}