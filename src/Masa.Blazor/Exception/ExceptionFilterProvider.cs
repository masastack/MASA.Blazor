namespace Masa.Blazor
{
    public class ExceptionFilterProvider : IExceptionFilterProvider
    {
        private IEnumerable<IExceptionFilter> _filters;

        public ExceptionFilterProvider(IEnumerable<IExceptionFilter> filters)
        {
            _filters = filters;
        }

        public IEnumerable<IExceptionFilter> GetExceptionFilters()
        {
            return _filters;
        }
    }
}
