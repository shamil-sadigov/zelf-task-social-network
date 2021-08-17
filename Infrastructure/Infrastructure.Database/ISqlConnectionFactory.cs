using System;
using System.Data;

namespace Infrastructure.Database
{
    public interface ISqlConnectionFactory : IDisposable
    {
        IDbConnection GetOrCreateConnection();
    }
}