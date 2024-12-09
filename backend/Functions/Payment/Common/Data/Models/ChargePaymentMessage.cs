namespace POD.Functions.Payment.Common.Data.Models
{
    public class ChargePaymentMessage<T> where T : PaymentDetails
    {
        public int SellerId { get; set; }
        public int StoreId { get; set; }
        public int PaymentMethodId { get; set; }
        public int SellerPaymentMethodId { get; set; }  // TODO This is already in the PaymentTransaction model. Maybe no need
        public decimal TotalCost { get; set; }
        public string Currency { get; set; }
        public PaymentTransaction PaymentTransaction { get; set; }
        public T PaymentDetails { get; set; }
    }
}
