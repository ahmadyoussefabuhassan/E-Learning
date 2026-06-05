using E_Learning.Api.Middleware;
using E_Learning.Infrastructure;
using E_Learning.Infrastructure.DateSeeds;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Api.Extensions
{
    public static class AppServices
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
            RolesSeed.SeedRolesAsync(dbContext).GetAwaiter().GetResult();
            AdminSeed.SeedAdminAsync(dbContext).GetAwaiter().GetResult();

        }

        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            
        }
        public static void UseTokenCheck(this IApplicationBuilder app)
        {
            app.UseMiddleware<TokenMiddleware>();
        }
    }
}
