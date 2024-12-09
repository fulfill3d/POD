namespace POD.Functions.Payment.Common.Data.Models
{
    public class PaymentResultMessage<T> where T : PaymentResultDetails
    {
        public int SellerId { get; set; }
        public int StoreId { get; set; }
        public int SellerPaymentMethodId { get; set; }
        public PaymentTransaction PaymentTransaction { get; set; }
        public T PaymentResultDetails { get; set; }
        public bool IsSucceeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}
