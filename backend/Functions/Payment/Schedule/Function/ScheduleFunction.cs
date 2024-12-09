using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using POD.Functions.Payment.Schedule.Services.Interfaces;

namespace POD.Functions.Payment.Schedule
{
    public class ScheduleFunction(IScheduleService scheduleService)
    {
        [Singleton]
        [Function("payment-processing-schedule")]
        public async Task Run(
            [TimerTrigger("%PaymentProcessingSchedule%"
            #if DEBUG
                , RunOnStartup=true
            #endif
            )]TimerInfo timerInfo, ILogger logger)
        {
            logger.LogInformation("Payment processing started.");
            try
            {
                await scheduleService.TriggerPaymentProcessing();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
            logger.LogInformation("Payment processing finished.");
        }
    }
}