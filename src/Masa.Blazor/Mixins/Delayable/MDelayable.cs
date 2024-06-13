namespace Masa.Blazor.Mixins.Delayable;

public abstract class MDelayable : MasaComponentBase, IDelayable
{
    [Parameter]
    public int OpenDelay
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    [Parameter]
    public int CloseDelay
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    protected bool IsActive { get; private set; }

    public Func<Task>? BeforeShowContent { get; set; }

    public Func<bool, Task>? AfterShowContent { get; set; }

    private CancellationTokenSource? _cancellationTokenSource;

    protected async Task SetActiveInternal(bool value)
    {
        _cancellationTokenSource?.Cancel();

        if (_cancellationTokenSource is null || value)
        {
            _cancellationTokenSource = new();
        }

        var isLazyContent = false;

        if (value)
        {
            isLazyContent = await ShowLazyContent();
        }

        await WhenIsActiveUpdating(value);

        if (value && _cancellationTokenSource.Token.IsCancellationRequested)
        {
            return;
        }

        IsActive = value;

        if (value && BeforeShowContent is not null)
        {
            await BeforeShowContent();
        }

        StateHasChanged();

        if (value && AfterShowContent is not null)
        {
            await AfterShowContent(isLazyContent);
        }
    }

    /// <summary>
    /// Show lazy content
    /// </summary>
    /// <returns>shown for the first time if true</returns>
    protected virtual Task<bool> ShowLazyContent()
    {
        return Task.FromResult(false);
    }

    protected virtual Task WhenIsActiveUpdating(bool value)
    {
        return Task.CompletedTask;
    }
}