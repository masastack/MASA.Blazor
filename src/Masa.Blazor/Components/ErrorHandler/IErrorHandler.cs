namespace Masa.Blazor.Components.ErrorHandler;

public interface IErrorHandler
{
    Task HandleExceptionAsync(Exception exception);
}