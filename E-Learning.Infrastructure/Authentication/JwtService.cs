using E_Learning.Application.Abstractions.Authentication;
using E_Learning.Domain.JWT;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using E_Learning.Application.Abstractions.Clock;

namespace E_Learning.Infrastructure.Authentication
{
    public sealed class JwtService : IJwtService
    {
        private readonly JWT _jwt;
        private readonly IDateTimeProvider _dateTimeProvider;
        public JwtService(IOptions<JWT> jwt, IDateTimeProvider dateTimeProvider)
        {
            _jwt = jwt.Value;
            _dateTimeProvider = dateTimeProvider;
        }

        public string GenerateToken(Guid userId, string email, string rolename)
        {
            var Claims = new[]
       {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, rolename.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: Claims,
                expires:  _dateTimeProvider.UtcNow.AddDays(_jwt.DurationInDays),
                signingCredentials: credentials
            );
            var telemetry = new JwtSecurityTokenHandler().WriteToken(token);
            return  telemetry;
        }
    }
}
