using E_Learning.Application.Abstractions.Behaviors;
using E_Learning.Application.Abstractions.Subscriptions;
using E_Learning.Application.Courses.Activator;
using E_Learning.Application.ExamExplanations.Activator;
using E_Learning.Application.Invtensives.Activator;
using E_Learning.Application.Sections.Activator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace E_Learning.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration => 
            {

                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

            });
            services.AddScoped<ISubscriptionActivator, CourseActivator>();
            services.AddScoped<ISubscriptionActivator, SectionActivator>();
            services.AddScoped<ISubscriptionActivator, InvtensiveActivator>();
            services.AddScoped<ISubscriptionActivator, ExamExplanationActivator>();
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            return services;
        }
    }
}
