using E_Learning.Infrastructure;
using E_Learning.Infrastructure.Notifications;
using E_Learning.Application;
using E_Learning.Api.Extensions;
using System.IdentityModel.Tokens.Jwt;

namespace E_Learning.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
           JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var builder = WebApplication.CreateBuilder(args);
            // Add Dependencies
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddPresentation(builder.Configuration);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.ApplyMigrations();
            app.UseCustomExceptionHandler();
            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseTokenCheck();
            app.UseAuthorization();
            app.MapHub<NotificationHub>("/notificationHub");

            app.MapControllers();

            app.Run();
        }
    }
}
