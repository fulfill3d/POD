using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using POD.Functions.PublishSchedule.Services.Interfaces;

namespace POD.Functions.PublishSchedule
{
    public class PublishScheduleFunction(IPublishScheduleService publishScheduleService)
    {
        
        [Function("publish-processing-schedule")]
        public async Task Run(
            [TimerTrigger("%PublishProcessingTimerSchedule%"
            #if DEBUG
                            , RunOnStartup=true
            #endif
            )]TimerInfo myTimer, ILogger logger)
        {
            // TODO Implement Caching. Because trigger might send double start-publish message,
            // if the receiver does not process
            logger.LogInformation("PublishProcessingSchedule called");
            await publishScheduleService.TriggerPublishProcessing();
        }
    }
}