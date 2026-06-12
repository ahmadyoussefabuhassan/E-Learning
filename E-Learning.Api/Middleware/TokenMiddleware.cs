using E_Learning.Domain.RefreshTokens;
using E_Learning.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace E_Learning.Api.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ApplicationDbContext db)
        {
            if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
            {
                await _next(context);
                return;
            }
            var jti = context.User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value
                        ?? context.User.FindFirst("jti")?.Value;
            if (string.IsNullOrWhiteSpace(jti))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            var isTokenInvalid = await db.Set<RefreshToken>()
            .AnyAsync(x => x.JWTId == jti.Trim() && (x.IsRevoked || x.IsUsed));
            if (isTokenInvalid)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            await _next(context);
        }

    }
}
