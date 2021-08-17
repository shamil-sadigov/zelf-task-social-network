#region

using System.Threading.Tasks;
using Infrastructure.Database;

#endregion

namespace WebApi.IntegrationTests.Helpers
{
    public interface IDataSeeder
    {
        Task Seed(ApplicationContext dbContext);
    }
}