using System.Reflection;
using DungeonDelvers.Common.Infrastructure;
using DungeonDelvers.Common.Infrastructure.Database;
using DungeonDelvers.Modules.Monsters.Application.Abstractions.Data;
using DungeonDelvers.Modules.Monsters.Domain.Monsters;
using DungeonDelvers.Modules.Monsters.Infrastructure.Database;
using DungeonDelvers.Modules.Monsters.Infrastructure.Inbox;
using DungeonDelvers.Modules.Monsters.Infrastructure.Monsters;
using DungeonDelvers.Modules.Monsters.Infrastructure.Outbox;
using Microsoft.Extensions.DependencyInjection;

namespace DungeonDelvers.Modules.Monsters.Infrastructure;

public class MonstersModule : Module<
    MonstersDbContext, 
    InboxOptions,
    ProcessInboxJob,
    ConfigureProcessInboxJob,
    OutboxOptions,
    ProcessOutboxJob,
    ConfigureProcessOutboxJob>
{
    public static string Name => "Monsters";
    public static string Schema => "monsters";
    protected override string ModuleName => Name;
    protected override Assembly DomainAssembly => Domain.AssemblyReference.Assembly;
    protected override Assembly ApplicationAssembly => Application.AssemblyReference.Assembly;
    protected override Assembly PresentationAssembly => Presentation.AssemblyReference.Assembly;
    protected override Type IdempotentDomainEventHandlerType => typeof(IdempotentDomainEventHandler<>);
    protected override Type IdempotentIntegrationEventHandlerType => typeof(IdempotentIntegrationEventHandler<>);

    protected override void ConfigureDatabase(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<MonstersDbContext>(Postgres.StandardOptions(connectionString, Schema))
            .AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<MonstersDbContext>())
            .AddScoped<IMonsterRepository, MonsterRepository>();
    }
    
}