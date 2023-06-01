using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Business
{
    public class Security
    {
        public string CreateToken(User user)
        {
            if (user.Name == "test" && user.Password == "test")
            {
                var issuer = "google.com";
                var audience = "google.com";

                var jwtTokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes("TestMillionAndUp");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    Audience = audience,
                    Issuer = issuer,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                string jwtToken = jwtTokenHandler.WriteToken(token);
                return jwtToken;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
