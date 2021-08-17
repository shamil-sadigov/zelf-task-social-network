using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Queries;
using Application.Queries.GetTopPopularClients;
using Dapper;

namespace Infrastructure.Database.Implementations
{
    public class ClientQueryRepository : IClientQueryRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ClientQueryRepository(ISqlConnectionFactory sqlConnectionFactory)
            => _sqlConnectionFactory = sqlConnectionFactory;

        public async Task<List<ClientDto>> GetTopPopularAsync(ushort limit)
        {
            var connection = _sqlConnectionFactory.GetOrCreateConnection();

            var clients  = await connection.QueryAsync<ClientDto>(
                "SELECT Id, Name, Popularity"+
                "FROM clients" +
                "ORDER BY Popularity" +
                "LIMIT @Limit" , new
                {
                    Limit = limit,
                });

            return clients.ToList();
        }

        public async Task<ClientDto?> GetByIdAsync(Guid clientId)
        {
            var connection = _sqlConnectionFactory.GetOrCreateConnection();

            var result  = await connection.QuerySingleOrDefaultAsync<ClientDto>(
                "SELECT Id, Name, Popularity"+
                "FROM clients" +
                "WHERE Id=@ClientId", new
                {
                    ClientId = clientId,
                });

            return result;
        }
    }
}