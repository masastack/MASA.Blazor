namespace BlazorComponent
{
    public class Invoker
    {
        private readonly Action? _action;
        private readonly Func<Task>? _func;

        public Invoker(Action action)
        {
            _action = action;
        }

        public Invoker(Func<Task> func)
        {
            _func = func;
        }

        [JSInvokable]
        public async Task Invoke()
        {
            if (_action != null)
            {
                _action.Invoke();
            }
            else if (_func != null)
            {
                await _func.Invoke();
            }
        }
    }

    public class Invoker<T>
    {
        private readonly Action<T>? _action;
        private readonly Func<T, Task>? _func;

        public Invoker(Action<T> action)
        {
            _action = action;
        }

        public Invoker(Func<T, Task> func)
        {
            _func = func;
        }

        [JSInvokable]
        public async Task Invoke(T param)
        {
            if (_action != null)
            {
                _action.Invoke(param);
            }
            else if (_func != null)
            {
                await _func(param);
            }
        }
    }
}
