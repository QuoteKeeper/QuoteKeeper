using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using QuoteKeeper.API.Config;
using Microsoft.Extensions.Options;
using QuoteKeeper.API.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace QuoteKeeper.API.Services
{
    public class AuthService
    {
        private readonly JwtSettings _jwtSettings;
        public AuthService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public string GenerateUserToken(User user)
        {
           var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
           new Claim("FirstName", user.FirstName),
          new Claim("LastName", user.LastName),
           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // TO Login verify Password 
        public bool VerifyPassword(User user, string password, IPasswordHasher<User> passwordHasher)
        {
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;

        }

    }
}