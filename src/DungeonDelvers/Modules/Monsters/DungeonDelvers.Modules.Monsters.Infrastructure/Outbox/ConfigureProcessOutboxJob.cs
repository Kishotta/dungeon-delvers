using DungeonDelvers.Common.Infrastructure.Outbox;
using Microsoft.Extensions.Options;

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Outbox;

public sealed class ConfigureProcessOutboxJob(IOptions<OutboxOptions> outboxOptions) 
    : ConfigureProcessOutboxJobBase<OutboxOptions, ProcessOutboxJob>(outboxOptions);