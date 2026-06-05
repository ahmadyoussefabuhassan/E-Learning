using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_Learning.Application.Abstractions.Services
{
    public  class BaseService
    {
        protected Guid UserId => GetUserId();


        protected readonly IHttpContextAccessor _accessor;
        public BaseService(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }
        private Guid GetUserId()
        {
            var user = _accessor.HttpContext?.User;

            // البحث عن sub بكل مسمياته (المختصر والطويل) لضمان إنه ما يضيع
            var claimValue = user?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                          ?? user?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                          ?? user?.FindFirst("sub")?.Value;

            return Guid.TryParse(claimValue, out var id) ? id : Guid.Empty;
        }
    }
}
