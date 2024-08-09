using System.Data.Common;
using DungeonDelvers.Common.Application.Data;
using Npgsql;

namespace DungeonDelvers.Common.Infrastructure.Data;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}
