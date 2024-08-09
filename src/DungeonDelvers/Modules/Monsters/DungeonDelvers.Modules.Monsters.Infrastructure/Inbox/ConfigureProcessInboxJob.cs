using DungeonDelvers.Common.Infrastructure.Inbox;
using Microsoft.Extensions.Options;

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Inbox;

public sealed class ConfigureProcessInboxJob(IOptions<InboxOptions> inboxOptions) 
    : ConfigureProcessInboxJobBase<InboxOptions, ProcessInboxJob>(inboxOptions);