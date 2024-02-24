using FluentValidation;
using Logic.TechnicalAssement.Core.Commands.UpdateLeaveCommand;
using Microsoft.Extensions.DependencyInjection;

namespace Logic.TechnicalAssement.Core.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Using the Mediator pattern in this service to enforce consistency in the application 
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionsExtensions).Assembly));

            // Using automapper for projections
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(ServiceCollectionsExtensions).Assembly);
            });

            // Using Fluent Validator to validate models \ requests
            services.AddValidatorsFromAssemblyContaining<UpdateLeaveRequestValidator>();

            return services;
        }
    }
}
