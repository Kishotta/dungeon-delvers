using DungeonDelvers.Common.Infrastructure.Auditing;
using DungeonDelvers.Common.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace DungeonDelvers.Common.Infrastructure.Database;

public static class Postgres
{
    public static Action<IServiceProvider, DbContextOptionsBuilder> StandardOptions(string connectionString, string schema) =>
        (serviceProvider, options) =>
        {
            options.UseNpgsql(
                    connectionString,
                    optionsBuilder =>
                    {
                        optionsBuilder.MigrationsHistoryTable(HistoryRepository.DefaultTableName, schema);
                    }).UseSnakeCaseNamingConvention()
                .AddInterceptors(
                    serviceProvider.GetRequiredService<InsertOutboxMessagesInterceptor>(),
                    serviceProvider.GetRequiredService<WriteAuditLogInterceptor>());
        };
}