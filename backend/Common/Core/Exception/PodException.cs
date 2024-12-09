namespace POD.Common.Core.Exception
{
    public class PodException : System.Exception
    {
        public PodException() : base()
        {
        }

        public PodException(string message) : base(message)
        {
        }
    }
}