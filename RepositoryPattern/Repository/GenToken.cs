using Microsoft.IdentityModel.Tokens;
using RepositoryPattern.Helper;
using RepositoryPattern.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static RepositoryPattern.Models.Jwt;

namespace RepositoryPattern.Repository
{
    public class GenToken
    {
        public string GenerateJSONWebToken(int empid, UserLogin userProfile)
        {
            var jwtSection = ConfigHelper.GetSection<Jwt>("Jwt");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, empid.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSection.Key);
            var ExpDay = int.Parse(jwtSection.ExpiresDay);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", empid.ToString()),
                    new Claim("expdate", DateTime.Now.AddDays(ExpDay).ToString("yyyyMMddHHmmss")),
                    new Claim("FullName",$"{userProfile.Username}"),
                    new Claim("Email", userProfile.Username),
                }),
                Expires = DateTime.Now.AddDays(ExpDay),
                IssuedAt = DateTime.UtcNow,
                Issuer = jwtSection.Issuer,
                Audience = jwtSection.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
