#region

using Application;
using Application.Contracts;
using Application.Pipelines;
using Domain.Contracts;
using Infrastructure.Database.Implementations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace WebApi.CompositionRoot
{
    public static partial class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDomainEventsPublisher, DomainEventsPublisher>();

            services.AddMediatR(typeof(DomainEventsPublisher));

            // TODO: Add LoggingPipeline
            // services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipeline<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipeline<,>));

            return services;
        }
    }
}