namespace BlazorComponent.JSInterop;

public static class JSExceptionExtensions
{
    // TODO: after https://github.com/dotnet/aspnetcore/issues/52070 is fixed, remove this
    /// <summary>
    /// Check if the exception is thrown by trying to create a JSObjectReference from null or undefined
    /// </summary>
    /// <param name="e"></param>
    /// <exception cref="JSException"></exception>
    public static bool ForCannotCreateFromNullOrUndefined(this JSException e)
    {
        return e.Message.StartsWith("Cannot create a JSObjectReference from the");
    }
}
