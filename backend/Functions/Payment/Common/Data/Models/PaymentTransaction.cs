namespace POD.Functions.Payment.Common.Data.Models
{
    public class PaymentTransaction
    {
        public PaymentTransaction()
        {
            SaleTransactions = new List<SaleTransaction>();
        }

        public int SellerPaymentMethodId { get; set; }
        public decimal Total { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public List<SaleTransaction> SaleTransactions { get; set; }
    }
}
