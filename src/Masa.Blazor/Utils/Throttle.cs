namespace Masa.Blazor.Utils;

public class Throttle(TimeSpan delay)
{
    private DateTime _lastExecutionTime = DateTime.MinValue;
    private readonly object _lock = new();

    public Throttle(int delay) : this(TimeSpan.FromMilliseconds(delay))
    {
    }

    public void Execute(Action action)
    {
        lock (_lock)
        {
            var now = DateTime.UtcNow;
            if (now - _lastExecutionTime >= delay)
            {
                _lastExecutionTime = now;
                action();
            }
        }
    }
}