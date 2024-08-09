using System.Reflection;
using DungeonDelvers.Common.Application.Clock;
using DungeonDelvers.Common.Application.Data;
using DungeonDelvers.Common.Infrastructure.Outbox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Outbox;

public sealed class ProcessOutboxJob(
    IDbConnectionFactory dbConnectionFactory,
    IServiceScopeFactory serviceScopeFactory,
    IDateTimeProvider dateTimeProvider, 
    IOptions<OutboxOptions> outboxOptions, 
    ILogger<ProcessOutboxJob> logger) 
    : ProcessOutboxJobBase<OutboxOptions>(
        dbConnectionFactory, 
        serviceScopeFactory, 
        dateTimeProvider, 
        outboxOptions,
        logger)
{
    protected override string ModuleName => MonstersModule.Name;
    protected override string Schema => MonstersModule.Schema;
    protected override Assembly ApplicationAssembly => Application.AssemblyReference.Assembly;
}