using System.Data;
using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Implementations
{
    public class SqlLiteConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public SqlLiteConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetOrCreateConnection()
        {
            if (_connection is {State: ConnectionState.Open})
                return _connection;

            _connection = new SqliteConnection(_connectionString);
            _connection.Open();

            return _connection;
        }

        public void Dispose()
        {
            if (_connection is {State: ConnectionState.Open})
                _connection.Dispose();
        }
    }
}