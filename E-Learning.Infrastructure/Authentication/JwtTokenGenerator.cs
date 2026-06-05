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
    public sealed class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwt;
        private readonly IDateTimeProvider _dateTimeProvider;
        public JwtTokenGenerator(IOptions<JwtSettings> jwt, IDateTimeProvider dateTimeProvider)
        {
            _jwt = jwt.Value;
            _dateTimeProvider = dateTimeProvider;
        }

        public string GenerateToken(Guid userId, string email, string FullName ,string rolename , string jit)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Claims = new[]
            {
                  new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                  new Claim(JwtRegisteredClaimNames.Email, email),
                  new Claim(ClaimTypes.Role, rolename.ToString()),
                  new Claim(JwtRegisteredClaimNames.Name , FullName),
                  new Claim(JwtRegisteredClaimNames.Jti, jit)

            };
            var securityToken = new JwtSecurityToken(

                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                expires: _dateTimeProvider.Now.AddDays(_jwt.DurationInDays),
                claims: Claims,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(securityToken);


        }
    }
}
