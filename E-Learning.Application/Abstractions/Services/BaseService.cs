using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace E_Learning.Application.Abstractions.Services
{
    public  class BaseService
    {
        protected Guid UserId => GetUserId();


        protected readonly HttpContext? _httpContext;
        private Guid GetUserId()
        {
            var id = Guid.Empty;
            try
            {
                if (_httpContext?.User?.Identity is { IsAuthenticated: true })
                {
                    id = Guid.Parse(_httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
                }
            }
            catch (Exception)
            {
                id = Guid.Empty;
            }

            return id;
        }
    }
}
