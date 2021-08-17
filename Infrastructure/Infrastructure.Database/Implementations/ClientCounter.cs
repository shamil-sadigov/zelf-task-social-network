using System.Threading.Tasks;
using Dapper;
using Domain.Contracts;
using Domain.ValueObjects;

namespace Infrastructure.Database.Implementations
{
    public class ClientCounter:IClientCounter
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ClientCounter(ISqlConnectionFactory sqlConnectionFactory)
            => _sqlConnectionFactory = sqlConnectionFactory;

        public async Task<int> CountByNameAsync(ClientName name)
        {
            var connection = _sqlConnectionFactory.GetOrCreateConnection();

            int clientCount = await connection.QuerySingleOrDefaultAsync<int>(
                "SELECT COUNT(Id) FROM clients " +
                "WHERE [Name]=@Name", new
                {
                    Name = name.Value,
                });

            return clientCount;
        }
    }
}