namespace POD.Functions.Shopify.WebhookEndpoints.Data.Models
{
    public class ServiceResult<T>
    {
        public T Result { get; private set; }
        public bool IsContentReturn { get; private set; }
        public ServiceResult(T result, bool hasError, bool isContentReturn = false)
        {
            this.Result = result;
            this.IsContentReturn = isContentReturn;
        }
    }
}
