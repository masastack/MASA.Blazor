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

    private bool _firstActive = true;

    public bool IsActive { get; protected set; }

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

        await OnActiveUpdatingAsync(value);

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

        await OnActiveUpdatedAsync(_firstActive, value);
        _firstActive = false;

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

    /// <summary>
    /// Called before the active state is updated
    /// </summary>
    /// <param name="active">The active state</param>
    /// <returns></returns>
    protected virtual Task OnActiveUpdatingAsync(bool active)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Called after the active state is updated and the component is rendered
    /// </summary>
    /// <param name="firstActive">The first time when component is activated and rendered</param>
    /// <param name="active">The active state</param>
    /// <returns></returns>
    protected virtual Task OnActiveUpdatedAsync(bool firstActive, bool active)
    {
        return Task.CompletedTask;
    }
}