namespace Masa.Blazor
{
    public class DelayTask
    {
        private CancellationTokenSource _cancellationTokenSource;

        public DelayTask()
            : this(300)
        {
        }

        public DelayTask(int delay)
        {
            Delay = delay;
        }

        public int Delay { get; set; }

        public async Task Run(Func<Task> function)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            await Task.Delay(Delay, _cancellationTokenSource.Token);
            await function?.Invoke();
        }
    }
}
