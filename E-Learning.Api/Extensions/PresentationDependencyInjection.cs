using Microsoft.OpenApi.Models;

namespace E_Learning.Api.Extensions
{
    public static class PresentationDependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    var allowedOrigins = configuration.GetSection("AllowedOrigins")
                    .Get<string[]>();
                    policy.WithOrigins(allowedOrigins!)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "فقط الصق التوكن (نسخ لصق). سواجر سيتكفل بالباقي."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }
    }
}