namespace POD.Functions.Payment.Common.Data.Models
{
    public class SaleTransaction
    {
        public SaleTransaction()
        {
            SaleTransactionDetails = new List<SaleTransactionDetail>();
        }

        public int StoreSaleOrderId { get; set; }
        public decimal Total { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int UserId { get; set; }

        public List<SaleTransactionDetail> SaleTransactionDetails { get; set; }
    }
}
