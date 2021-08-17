using System.Threading.Tasks;
using Infrastructure.Database;

namespace WebApi.IntegrationTests.Helpers
{
    public interface IDataSeeder
    {
        Task Seed(ApplicationContext dbContext);
    }
}