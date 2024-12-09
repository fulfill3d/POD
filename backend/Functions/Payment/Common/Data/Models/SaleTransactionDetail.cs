namespace POD.Functions.Payment.Common.Data.Models
{
    public class SaleTransactionDetail
    {
        public int Order { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime LastModifiedDate { get; set; }
        
        public int StoreSaleOrderDetailsId { get; set; }
        public decimal Shipping { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }


    }
}