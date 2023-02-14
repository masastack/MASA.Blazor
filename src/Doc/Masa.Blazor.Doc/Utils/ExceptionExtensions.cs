using System.Net;

namespace Microsoft.AspNetCore.Components;

public static class ExceptionExtensions
{
    public static void ThrowNotFoundException(this ComponentBase component, string? msg = null, Exception? innerException = null) => throw new HttpRequestException(msg, innerException, HttpStatusCode.NotFound);
}

