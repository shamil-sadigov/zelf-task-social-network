using System;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.IntegrationTests.Helpers;

namespace WebApi.IntegrationTests.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void RecreateDatabase<TDbContext>(
            this IServiceProvider serviceProvider,
            IDataSeeder? dataSeeder = null)
            where TDbContext : ApplicationContext
        {
            var dbContext = serviceProvider.GetRequiredService<TDbContext>();

            dbContext.Database.EnsureDeleted();
            dbContext.Database.Migrate();
            dataSeeder?.Seed(dbContext);
        }
    }
}