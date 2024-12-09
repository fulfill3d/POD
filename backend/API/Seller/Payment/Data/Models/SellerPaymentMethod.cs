namespace POD.API.Seller.Payment.Data.Models
{
	public class SellerPaymentMethod
	{
        public SellerPaymentMethod( int sellerPaymentMethodId, int paymentMethodId, bool isDefault, string text, 
            string expireDate, string cardholderName)
        {
            SellerPaymentMethodId = sellerPaymentMethodId;
            PaymentMethodId = paymentMethodId;
            IsDefault = isDefault;
            Text = text;
            ExpireDate = expireDate;
            CardholderName = cardholderName;
        }
        
        public SellerPaymentMethod( int sellerPaymentMethodId, int paymentMethodId, bool isDefault, string text,
            string cardholderName)
        {
            SellerPaymentMethodId = sellerPaymentMethodId;
            PaymentMethodId = paymentMethodId;
            IsDefault = isDefault;
            Text = text;
            CardholderName = cardholderName;
        }

        public int SellerPaymentMethodId { get; set; }
        public int PaymentMethodId { get; set; }
        public bool IsDefault { get; set; }
        public string Text { get; set; }
        public string ExpireDate { get; set; }
        public string CardholderName { get; set; }
    }
}

