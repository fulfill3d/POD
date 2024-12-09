namespace POD.Common.Core.Enum
{
    public enum OrderStatus
    {
        Received = 1,
        ProcessingPayment = 2,
        PaymentCleared = 3,
        PaymentError = 4,
        ProcessingFulfillment = 5,
        Fulfilled = 6,
        Shipped = 7,
        Cancelled = 8,
        TestOrder = 9,
    }
}