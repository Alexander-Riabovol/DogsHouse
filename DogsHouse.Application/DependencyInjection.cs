using DogsHouse.Application.CQRS.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DogsHouse.Application
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds dependencies of services declared in the Application layer.
        /// </summary>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Add Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Add MediatR
            services.AddMediatR(config =>
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            // Add Behaviors
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
