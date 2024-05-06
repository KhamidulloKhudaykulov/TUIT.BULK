namespace Bulk.Service.Exceptions;

public class ArgumentIsNotValidException : Exception
{
    public ArgumentIsNotValidException() { }
    public ArgumentIsNotValidException(string message) : base(message) { }
    public ArgumentIsNotValidException(string message, Exception exception) : base(message, exception) { }
    public int StatusCode => 400;
}
