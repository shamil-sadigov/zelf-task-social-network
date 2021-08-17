#region

using System.Threading.Tasks;
using Domain;
using Infrastructure.Database;

#endregion

namespace WebApi.IntegrationTests.Helpers
{
    public class ClientSeeder : IDataSeeder
    {
        private readonly Client[] _clients;

        public ClientSeeder(params Client[] clients)
        {
            _clients = clients;
        }

        public async Task Seed(ApplicationContext dbContext)
        {
            await dbContext.Clients.AddRangeAsync(_clients);

            await dbContext.SaveChangesAsync();
        }
    }
}