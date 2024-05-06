namespace Bulk.Service.Exceptions;

public class AlreadyExistException : Exception
{
    public AlreadyExistException() { }
    public AlreadyExistException(string message) : base(message) { }
    public AlreadyExistException(string message, Exception exception) : base(message, exception) { }
    public int StatusCode => 409;
}
