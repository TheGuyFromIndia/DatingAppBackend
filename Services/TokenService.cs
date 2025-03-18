using DatingApp.Domain.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DatingApp.Services
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        public string CreateToken(User user)
        {
            var tokenKey = configuration["TokenKey"];

            if (string.IsNullOrEmpty(tokenKey) || tokenKey.Length < 64) {

                throw new Exception("Token is null,empty or invalid");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Name),
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token  = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
