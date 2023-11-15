namespace Masa.Blazor.Utils;

public class ThrottleTask
{
    private readonly object _lock = new();

    private Task _currentTask = Task.CompletedTask;
    private bool _canceled;

    public ThrottleTask() : this(300)
    {
    }

    public ThrottleTask(int interval)
    {
        Interval = interval;
    }

    public int Interval { get; }

    public async Task RunAsync(Func<Task> task)
    {
        lock (_lock)
        {
            if (!_currentTask.IsCompleted)
            {
                return; // ignore if the previous task is not completed
            }

            _currentTask = Task.Delay(Interval);
            _canceled = false;
        }

        try
        {
            await _currentTask;

            if (_canceled)
            {
                return;
            }

            await task.Invoke();
        }
        catch (TaskCanceledException)
        {
            // ignored
        }
    }

    public Task RunAsync(Action task)
        => RunAsync(() =>
        {
            task();
            return Task.CompletedTask;
        });

    /// <summary>
    /// Cancel the task if it's running.
    /// It's useful when you have got your result
    /// and don't want the rest of the task to be executed.
    /// </summary>
    public void Cancel()
    {
        _canceled = true;
    }
}
