#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Queries;
using Dapper;

#endregion

namespace Infrastructure.Database.Implementations
{
    public class ClientQueryRepository : IClientQueryRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ClientQueryRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<ClientDto>> GetTopPopularAsync(ushort limit)
        {
            var connection = _sqlConnectionFactory.GetOrCreateConnection();

            var clients = await connection.QueryAsync<ClientDbModel>(
                "SELECT [Id], [Name], [Popularity]" +
                "FROM Clients" +
                "ORDER BY [Popularity]" +
                "LIMIT @Limit", new
                {
                    Limit = limit
                });

            return clients
                .Select(x => MapToDto(x))
                .ToList();
        }

        public async Task<ClientDto?> GetByIdAsync(Guid clientId)
        {
            var connection = _sqlConnectionFactory.GetOrCreateConnection();

            var result = await connection.QuerySingleOrDefaultAsync<ClientDbModel>(
                "SELECT [Id], [Name], [Popularity]" +
                "FROM Clients " +
                "WHERE [Id]=@ClientId", new
                {
                    ClientId = clientId.ToString().ToUpper()
                });

            return result is null ? null : MapToDto(result);
        }

        private static ClientDto MapToDto(ClientDbModel model)
            => new
            (
                Guid.Parse(model.Id),
                model.Name,
                model.Popularity
            );

        private class ClientDbModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public ushort Popularity { get; set; }
        }
    }
}