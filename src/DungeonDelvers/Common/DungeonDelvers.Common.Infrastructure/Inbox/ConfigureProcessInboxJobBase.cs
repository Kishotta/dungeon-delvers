using Microsoft.Extensions.Options;
using Quartz;

namespace DungeonDelvers.Common.Infrastructure.Inbox;

public abstract class ConfigureProcessInboxJobBase<TInboxOptions, TProcessInboxJob>(IOptions<TInboxOptions> inboxOptions)
    : IConfigureOptions<QuartzOptions>
    where TInboxOptions : InboxOptionsBase
    where TProcessInboxJob : ProcessInboxJobBase<TInboxOptions>
{
    private readonly InboxOptionsBase _inboxOptionsBase = inboxOptions.Value;
    
    public void Configure(QuartzOptions options)
    {
        var jobName = typeof(TProcessInboxJob).FullName!;

        options.AddJob<TProcessInboxJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(_inboxOptionsBase.IntervalInSeconds).RepeatForever()));
    }
}