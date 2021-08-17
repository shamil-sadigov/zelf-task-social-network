using System;
using System.IO;
using Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.IntegrationTests.Extensions;
using WebApi.IntegrationTests.Helpers;

namespace WebApi.IntegrationTests
{
    public class ClientsWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private IDataSeeder? _dataSeeder;

        public ClientsWebApplicationFactory WithPredefinedData(IDataSeeder dataSeeder)
        {
            _dataSeeder = dataSeeder;
            return this;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(ops =>
            {
                ops.AddJsonFile("appsettings.testing.json");
            });

            builder.UseEnvironment("testing");

            builder.UseContentRoot(Directory.GetCurrentDirectory());

            builder.ConfigureServices(services =>
            {
                using var provider = services.BuildServiceProvider();
                using var scope = provider.CreateScope();

                var servicesProvider = scope.ServiceProvider;

                servicesProvider.RecreateDatabase<ApplicationContext>(_dataSeeder);
            });
        }
    }
}