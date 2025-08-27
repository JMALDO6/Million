using Microsoft.IdentityModel.Tokens;
using Million.Domain.Entities;
using Million.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Million.Infrastructure.Security.Jwt
{
    /// <summary>
    /// Token generator for creating JWT tokens.
    /// </summary>
    public static class TokenGenerator
    {
        /// <summary>
        /// Generates a JWT token for the specified user and roles.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string GenerateJwtToken(ApplicationUser user, IList<string> roles, JwtSettings settings)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}