#region

using System;
using System.Data;

#endregion

namespace Infrastructure.Database
{
    public interface ISqlConnectionFactory : IDisposable
    {
        IDbConnection GetOrCreateConnection();
    }
}