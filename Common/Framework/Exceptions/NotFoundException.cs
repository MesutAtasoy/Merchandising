using System.Net;
using Framework.Exceptions.Abstraction;

namespace Framework.Exceptions;

public sealed class NotFoundException : ExceptionBase
{
    public NotFoundException(string message) : base(message)
    {
        StatusCode = (int)HttpStatusCode.NotFound;
    }
}