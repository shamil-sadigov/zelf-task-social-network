#region

using Application.Contracts;
using Domain.Contracts;
using Infrastructure.Database;
using Infrastructure.Database.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApi.Extensions;

#endregion

namespace WebApi.CompositionRoot
{
    public static partial class ServiceExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddOptions<ConnectionStringOptions>()
                .BindConfiguration("ConnectionStrings")
                .ValidateDataAnnotations();

            services.AddDbContext<ApplicationContext>((provider, dbOptions) =>
            {
                var connectionString = provider.GetRequiredService<IOptions<ConnectionStringOptions>>();
                var environment = provider.GetRequiredService<IHostEnvironment>();
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

                if (environment.IsDevelopment() || environment.IsTesting())
                    dbOptions.EnableSensitiveDataLogging();

                dbOptions.EnableDetailedErrors();
                dbOptions.UseLoggerFactory(loggerFactory);

                dbOptions.UseSqlite(connectionString.Value.Default,
                    ops => ops.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
            });


            services.AddScoped<ISqlConnectionFactory, SqlLiteConnectionFactory>(provider =>
            {
                var connectionStringOptions = provider.GetRequiredService<IOptions<ConnectionStringOptions>>();

                return new SqlLiteConnectionFactory(connectionStringOptions.Value.Default);
            });

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientQueryRepository, ClientQueryRepository>();

            services.AddScoped<IDomainEventAccessor, DomainEventsAccessor>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}