using POD.Common.Database.Models;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.PostProcessing.Data.Database;
using POD.Functions.Payment.PostProcessing.Services.Interfaces;

namespace POD.Functions.Payment.PostProcessing.Services
{
    public class SellerPaymentService(PostProcessingContext dbContext): ISellerPaymentService
    {
        
        public async Task SaveBraintreePaymentTransaction(PaymentResultMessage<BraintreeResultDetails> paymentResult)
        {
            var sellerPaymentTransaction = PrepareSellerPaymentTransactionModel(paymentResult.PaymentTransaction);
            
            sellerPaymentTransaction.BraintreeTransactionDetails.Add(new BraintreeTransactionDetail
            {
                BraintreeTransactionId = paymentResult.PaymentResultDetails.TransactionId
            });

            await dbContext.SellerPaymentTransactions.AddAsync(sellerPaymentTransaction);

            await dbContext.SaveChangesAsync();
        }

        public async Task SaveStripePaymentTransaction(PaymentResultMessage<StripeResultDetails> paymentResult)
        {
            var sellerPaymentTransaction = PrepareSellerPaymentTransactionModel(paymentResult.PaymentTransaction);
            
            sellerPaymentTransaction.StripeTransactionDetails.Add(new StripeTransactionDetail
            {
                StripePaymentId = paymentResult.PaymentResultDetails.PaymentIntentId
            });

            await dbContext.SellerPaymentTransactions.AddAsync(sellerPaymentTransaction);

            await dbContext.SaveChangesAsync();
            
        }

        public async Task SavePayPalPaymentTransaction(PaymentResultMessage<PayPalResultDetails> paymentResult)
        {
            var sellerPaymentTransaction = PrepareSellerPaymentTransactionModel(paymentResult.PaymentTransaction);
            
            sellerPaymentTransaction.PayPalTransactionDetails.Add(new PayPalTransactionDetail
            {
                PayPalCorrelationId = paymentResult.PaymentResultDetails.CapturedOrderId,
                PayPalTransactionId = paymentResult.PaymentResultDetails.CapturedOrderId

            });

            await dbContext.SellerPaymentTransactions.AddAsync(sellerPaymentTransaction);

            await dbContext.SaveChangesAsync();
        }

        private static SellerPaymentTransaction PrepareSellerPaymentTransactionModel(PaymentTransaction transaction)
        {
            return new SellerPaymentTransaction
            {
                Total = transaction.Total,
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SellerPaymentMethodId = transaction.SellerPaymentMethodId,
                StoreSaleTransactions = transaction.SaleTransactions.Select(st => new StoreSaleTransaction
                {
                    IsEnabled = true,
                    UpdatedAt = DateTime.UtcNow,
                    Total = st.Total,
                    StoreSaleTransactionDetails = st.SaleTransactionDetails.Select(std => new StoreSaleTransactionDetail
                    {
                        IsEnabled = true,
                        UpdatedAt = DateTime.UtcNow,
                        Price = std.Price,
                        Discount = std.Discount ?? Decimal.Zero,
                        Tax = std.Tax,
                        Total = std.Total,
                        StoreSaleOrderDetailId = std.StoreSaleOrderDetailsId
                    }).ToList()
                }).ToList()
            };
        }
    }
}