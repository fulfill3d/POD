
using Microsoft.EntityFrameworkCore;
using POD.Functions.PublishSchedule.Data.Database;
using POD.Functions.PublishSchedule.Data.Models;
using POD.Functions.PublishSchedule.Services.Interfaces;

namespace POD.Functions.PublishSchedule.Services
{
    public class SellerProductService(PublishScheduleContext dbContext): ISellerProductService
    {
        public async Task<IEnumerable<PublishProcessingProduct>> GetPublishProcessReadyProducts(
            int maxProduct,
            PublishProcessingRetryStrategy retryStrategy)
        {
            var candidates = (await dbContext.StoreProducts
                    .Include(sp => sp.Store)
                    .Where(sp =>
                        sp.IsEnabled
                        && sp.Store.IsEnabled
                        && sp.PublishingStatus == (int)Common.Core.Enum.PublishStatus.ProcessStarted
                        && (sp.PublishRetryCount ?? 0) < retryStrategy.MaxRetryCount)
                    .OrderBy(sp => sp.LastPublishingStatusChangeDate)
                    .Select(sp => new PublishProcessingCandidateProduct
                    {
                        StoreId = sp.Store.Id,
                        StoreProductId = sp.Id,
                        MarketPlace = (POD.Common.Core.Enum.MarketPlace)sp.Store.MarketPlaceId,
                        LastPublishingStatusChangeDate = sp.LastPublishingStatusChangeDate.GetValueOrDefault(DateTime.UtcNow),
                        PublishRetryCount = sp.PublishRetryCount.GetValueOrDefault(0)
                    })
                    .ToListAsync())
                .GroupBy(ppcp => ppcp.StoreId)
                .OrderBy(ppcp => ppcp.Count())
                .Take(maxProduct);
            // TODO Sometimes return empty product list
            return PublishProcessProductSelector(maxProduct, retryStrategy, candidates);
        }

        private List<PublishProcessingProduct> PublishProcessProductSelector(
                int maxProduct,
                PublishProcessingRetryStrategy retryStrategy,
                IEnumerable<IGrouping<int, PublishProcessingCandidateProduct>> candidates)
        {
            //On each batch we want to get equal number of products from each customer who tries to publish a product.
            //So that if a customer tries to publish 100 products, other customers who tries to publish a 1 product won't wait for him.

            var productsToProcess = new List<PublishProcessingProduct>();
            if (candidates == null || !candidates.Any()) return productsToProcess;
            var skip = 0;
            var skipCheck = maxProduct;

            //To avoid an infinite loop when the number of available products smaller than maxProduct
            //we are checking how many products we have skipped
            while (maxProduct > 0 && skip < skipCheck)
            {
                foreach (var candidate in candidates)
                {
                    if (maxProduct <= 0) break;

                    var product = candidate
                            .Where(p => IsProductRetryReady(retryStrategy, p))
                            .Skip(skip)
                            .FirstOrDefault();

                    if (product == null) continue;

                    productsToProcess.Add(new PublishProcessingProduct
                        {
                            StoreId = product.StoreId,
                            StoreProductId = product.StoreProductId,
                            MarketPlace = product.MarketPlace
                        });

                    maxProduct--;
                }

                skip++;
            }

            return productsToProcess;
        }
        
        private static bool IsProductRetryReady(
            PublishProcessingRetryStrategy retryStrategy,
            PublishProcessingCandidateProduct candidateProduct)
        {
            var retrySeconds = retryStrategy.MinRetryInterval 
                               + ((retryStrategy.MaxRetryInterval 
                                   - retryStrategy.MinRetryInterval) 
                                   / retryStrategy.MaxRetryCount 
                                   * candidateProduct.PublishRetryCount);

            var retryLimit = DateTime.UtcNow.AddSeconds(retrySeconds * -1);
            return candidateProduct.LastPublishingStatusChangeDate < retryLimit;
        }
    }
}