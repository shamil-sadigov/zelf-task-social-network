using System;
using System.Data;

namespace Infrastructure.Database.Implementations
{
    public interface ISqlConnectionFactory : IDisposable
    {
        IDbConnection GetOrCreateConnection();
    }
}