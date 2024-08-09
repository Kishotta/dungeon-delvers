using Microsoft.Extensions.Options;
using Quartz;

namespace DungeonDelvers.Common.Infrastructure.Outbox;

public abstract class ConfigureProcessOutboxJobBase<TOutboxOptions, TProcessOutboxJob>(IOptions<TOutboxOptions> inboxOptions)
    : IConfigureOptions<QuartzOptions>
    where TOutboxOptions : OutboxOptionsBase
    where TProcessOutboxJob : ProcessOutboxJobBase<TOutboxOptions>
{
    private readonly OutboxOptionsBase _outboxOptionsBase = inboxOptions.Value;
    
    public void Configure(QuartzOptions options)
    {
        var jobName = typeof(TProcessOutboxJob).FullName!;

        options.AddJob<TProcessOutboxJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(_outboxOptionsBase.IntervalInSeconds).RepeatForever()));
    }
}