using E_Learning.Application.Abstractions.Authentication;
using E_Learning.Application.Abstractions.Clock;
using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Notifications;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Classes;
using E_Learning.Domain.Courses;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.ExamVideos;
using E_Learning.Domain.Invtensives;
using E_Learning.Domain.InvtensivesVideos;
using E_Learning.Domain.JWT;
using E_Learning.Domain.Lessons;
using E_Learning.Domain.Notification;
using E_Learning.Domain.RefreshTokens;
using E_Learning.Domain.Roles;
using E_Learning.Domain.Sections;
using E_Learning.Domain.Students;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.Teachers;
using E_Learning.Domain.Units;
using E_Learning.Domain.User;
using E_Learning.Infrastructure.Authentication;
using E_Learning.Infrastructure.Clock;
using E_Learning.Infrastructure.Files;
using E_Learning.Infrastructure.Notifications;
using E_Learning.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace E_Learning.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'Database' is not configured.");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connection);
            });
            services.Configure<JwtSettings>(configuration.GetSection("JWT"));
            // Register repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICourseRepository , CourseRepository>();
            services.AddScoped<ISectionRepository , SectionRepository>();
            services.AddScoped<ILessonRepository , LessonRepository>();
            services.AddScoped<IUnitRepository , UnitRepository>();
            services.AddScoped<IExamExplanationRepository , ExamExplanationRepository>();
            services.AddScoped<IExamVideoRepository , ExamVideoRepository>();
            services.AddScoped<IClassesRepositry, ClassesRepositry>();
            services.AddScoped<IInvtensivesRepositry, InvtensivesRepositry>();
            services.AddScoped<IInvtensivesVideosRepositry, InvtensivesVideosRepositry>();
            services.AddScoped<INotificationRepositry, NotificationRepositry>();
            services.AddScoped<IStudentSubscriptionRepositry , StudentSubscriptionRepositry>();
            // Register Unit of Work
            services.AddScoped<IUnitOfWork>(s => s.GetRequiredService<ApplicationDbContext>());
            // Register other services like file handling, JWT service, etc. if needed
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<INotificationService, NotificationService>();
            // Register SignalR
            services.AddSignalR();
            // Bind JWT settings
            var jwtSection = configuration.GetSection("JWT");
            var jwtSettings = jwtSection.Get<JwtSettings>()
                ?? throw new InvalidOperationException("JWT settings are not configured in appsettings.json");
            services.Configure<JwtSettings>(jwtSection);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
           .AddJwtBearer(options =>
           {
              
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = false,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = jwtSettings.Issuer,
                   ValidAudience = jwtSettings.Audience,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                   ClockSkew = TimeSpan.FromMinutes(5),
                   NameClaimType = JwtRegisteredClaimNames.Sub,
                   RoleClaimType = ClaimTypes.Role
               };
               options.Events = new JwtBearerEvents
               {
                   OnMessageReceived = context =>
                   {
                       var authHeader = context.Request.Headers["Authorization"].ToString();
                       if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                       {
                           context.Token = authHeader.Substring("Bearer ".Length).Trim();
                       }
                       return Task.CompletedTask;
                   }
               };

           });

            return services;
        }
    }
}
