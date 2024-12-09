namespace POD.Functions.Payment.Schedule.Services.Interfaces
{
    public interface IScheduleService
    {
        Task TriggerPaymentProcessing();
    }
}