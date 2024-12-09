namespace POD.API.Common.Core.Exception
{
    public class EntityNotFoundException(string message = "Entity you've requested couldn't be found.") : BaseException(message)
    {
    }
}