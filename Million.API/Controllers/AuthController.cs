using Microsoft.AspNetCore.Mvc;

namespace Million.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginDto dto)
        //{
        //    // Simulación de validación de usuario (puedes conectar a DB o Identity)
        //    if (dto.Username == "admin" && dto.Password == "1234")
        //    {
        //        var claims = new[]
        //        {
        //        new Claim(ClaimTypes.Name, dto.Username),
        //        new Claim(ClaimTypes.Role, "Admin")
        //    };

        //        var key = new SymmetricSecurityKey(
        //            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //        var token = new JwtSecurityToken(
        //            issuer: _configuration["Jwt:Issuer"],
        //            audience: _configuration["Jwt:Audience"],
        //            claims: claims,
        //            expires: DateTime.UtcNow.AddHours(2),
        //            signingCredentials: creds);

        //        return Ok(new
        //        {
        //            token = new JwtSecurityTokenHandler().WriteToken(token)
        //        });
        //    }

        //    return Unauthorized("Credenciales inválidas");
        //}

    }
}
