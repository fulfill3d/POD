namespace POD.Common.Core.Exception
{
    public class ProductAlreadyInPublishingProcessException: PodException
    {
        public ProductAlreadyInPublishingProcessException(int customerProductId)
            : base($"{customerProductId} is already in a publishing process.")
        {
        }
    }
}