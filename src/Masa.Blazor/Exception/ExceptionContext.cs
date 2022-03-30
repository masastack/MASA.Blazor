namespace Masa.Blazor
{
    public class ExceptionContext
    {
        public ExceptionContext(Exception exception, IComponent component)
        {
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
            Component = component ?? throw new ArgumentNullException(nameof(component));
        }

        public bool ExceptionHandled { get; set; }

        public Exception Exception { get; set; }

        public IComponent Component { get; set; }
    }
}
