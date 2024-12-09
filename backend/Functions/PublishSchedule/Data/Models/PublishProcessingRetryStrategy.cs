namespace POD.Functions.PublishSchedule.Data.Models
{
    public class PublishProcessingRetryStrategy
    {
        /// <summary>
        /// Max number of retries before we stop trying to publish the product.
        /// </summary>
        public int MaxRetryCount { get; set; }
        /// <summary>
        /// Min How many seconds it should pass before retrying publishing
        /// </summary>
        public int MinRetryInterval { get; set; }
        /// <summary>
        /// Max How many seconds it should pass before retrying publishing
        /// </summary>
        public int MaxRetryInterval { get; set; }
    }
}