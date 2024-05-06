﻿namespace Bulk.Service.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception exception) : base(message, exception) { }
    public int StatusCode => 404;
}
