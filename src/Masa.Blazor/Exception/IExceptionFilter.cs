namespace Masa.Blazor
{
    public interface IExceptionFilter
    {
        void OnException(ExceptionContext context);
    }
}
