namespace POD.Integrations.BrainTreeClient.Exceptions
{
    public class BraintreeException : Exception
    {
        public string Method { get; private set; }

        public BraintreeException(string method, string? message) : base(message)
        {
            this.Method = method;
        }
    }
}