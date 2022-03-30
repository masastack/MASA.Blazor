namespace Masa.Blazor
{
    public interface IExceptionFilterProvider
    {
        IEnumerable<IExceptionFilter> GetExceptionFilters();
    }
}
