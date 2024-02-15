using System.Net;

namespace Framework.Exceptions.Abstraction;

public abstract class ExceptionBase : Exception, IStatusCodedException
{
    protected ExceptionBase()
    {
    }

    protected ExceptionBase(string message) : base(message)
    {
    }

    protected ExceptionBase(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    protected ExceptionBase(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = (int)statusCode;
    }

    public int StatusCode { get; set; } = 500;
}