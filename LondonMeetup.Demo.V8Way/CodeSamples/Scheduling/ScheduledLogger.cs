
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Web.Scheduling;

namespace LondonMeetup.Demo.V8Way.CodeSamples.Scheduling
{
    //lots of good examples in core - https://github.com/umbraco/Umbraco-CMS/tree/v8/contrib/src/Umbraco.Web/Scheduling HealthCheckNotifier, ScheduledPublishing etc
    public class ScheduledLogger : RecurringTaskBase
    {
         private ILogger _logger;

        public ScheduledLogger(IBackgroundTaskRunner<RecurringTaskBase> runner, int delayBeforeWeStart, int howOftenWeRepeat, ILogger logger)
            : base(runner, delayBeforeWeStart, howOftenWeRepeat)
        {
            _logger = logger;
        }

        public override bool PerformRun()
        {
          _logger.Info<ScheduledLogger>("The scheduler has ran");
          return true;
        }

        public override bool IsAsync => false;
    }
}