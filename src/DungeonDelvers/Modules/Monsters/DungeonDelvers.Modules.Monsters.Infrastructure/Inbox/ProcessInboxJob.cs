using System.Reflection;
using DungeonDelvers.Common.Application.Clock;
using DungeonDelvers.Common.Application.Data;
using DungeonDelvers.Common.Infrastructure.Inbox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Inbox;

public sealed class ProcessInboxJob(
    IDbConnectionFactory dbConnectionFactory,
    IServiceScopeFactory serviceScopeFactory,
    IDateTimeProvider dateTimeProvider, 
    IOptions<InboxOptions> inboxOptions, 
    ILogger<ProcessInboxJob> logger) 
    : ProcessInboxJobBase<InboxOptions>(
        dbConnectionFactory, 
        serviceScopeFactory, 
        dateTimeProvider, 
        inboxOptions,
        logger)
{
    protected override string ModuleName => MonstersModule.Name;
    protected override string Schema => MonstersModule.Schema;
    protected override Assembly PresentationAssembly => Presentation.AssemblyReference.Assembly;
}