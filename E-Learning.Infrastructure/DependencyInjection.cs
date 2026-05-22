using E_Learning.Domain.Abstractions;
using E_Learning.Domain.JWT;
using E_Learning.Domain.RefreshTokens;
using E_Learning.Domain.Roles;
using E_Learning.Domain.Students;
using E_Learning.Domain.Teachers;
using E_Learning.Domain.User;
using E_Learning.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace E_Learning.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("Database")
                ?? throw new InvalidOperationException("Connection string 'Database' is not configured.");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connection);
            });
            services.Configure<JWT>(configuration.GetSection("JWT"));
            // Register repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            // اضف الباقي هنا تحت التعليق اذا كان هناك اي ريبوزيتوري اخر

            // Register Unit of Work
            services.AddScoped<IUnitOfWork>(s => s.GetRequiredService<ApplicationDbContext>());
            return services;
        }
    }
}
