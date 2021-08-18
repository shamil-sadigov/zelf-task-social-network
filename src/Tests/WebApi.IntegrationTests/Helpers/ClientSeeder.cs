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

        public void Seed(ApplicationContext dbContext)
        {
            dbContext.Clients.AddRange(_clients);
            dbContext.SaveChanges();
        }
    }
}