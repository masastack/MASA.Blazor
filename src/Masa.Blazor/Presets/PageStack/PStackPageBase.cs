using Masa.Blazor.Presets.PageStack;

namespace Masa.Blazor.Presets;

public class PStackPageBase : ComponentBase, IAsyncDisposable
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [CascadingParameter] private PPageStack? PageStack { get; set; }

    private StackPageData? Page { get; set; }

    protected string? PageSelector => Page?.Selector;

    protected override void OnInitialized()
    {
        if (PageStack is null)
        {
            return;
        }

        var currentPath = NavigationManager.GetAbsolutePath();
        if (PageStack.Pages.TryPeek(out var page) && page.AbsolutePath == currentPath)
        {
            Page = page;
            Page.ActiveChanged += PageOnActiveChanged;
            _ = RunPageActivatedAsync(Page.State);
        }
        else
        {
            PageStack.Pages.PagePushed += PagesOnPagePushed;
        }
    }

    private void PagesOnPagePushed(object? sender, StackPagesPushedEventArgs e)
    {
        if (Page is null)
        {
            Page = e.Page;
            Page.ActiveChanged += PageOnActiveChanged;
        }

        _ = RunPageActivatedAsync(Page?.State);
    }

    private void PageOnActiveChanged(object? sender, PageActiveStateEventArgs e)
    {
        _ = e.Active ? RunPageActivatedAsync(Page!.State) : RunPageDeactivatedAsync();
    }

    /// <summary>
    /// This event is triggered when the page is on the top of the stack.
    /// In other words, it is triggered when the page is rendered in the
    /// viewport, including the <see cref="OnInitialized"/>.
    /// </summary>
    protected virtual void OnPageActivated(object? state)
    {
    }

    /// <summary>
    /// This event is triggered when the page is on the top of the stack.
    /// In other words, it is triggered when the page is rendered in the
    /// viewport, including the <see cref="OnInitialized"/>.
    /// </summary>
    protected virtual Task OnPageActivatedAsync(object? state) => Task.CompletedTask;

    /// <summary>
    /// This event is triggered when a new page is pushed into the stack.
    /// </summary>
    protected virtual void OnPageDeactivated()
    {
    }

    /// <summary>
    /// This event is triggered when a new page is pushed into the stack.
    /// </summary>
    protected virtual Task OnPageDeactivatedAsync() => Task.CompletedTask;

    private async Task RunPageActivatedAsync(object? state)
    {
        OnPageActivated(state);
        var task = OnPageActivatedAsync(state);

        if (task.Status != TaskStatus.RanToCompletion && task.Status != TaskStatus.Canceled)
        {
            try
            {
                await task;
            }
            catch
            {
                if (!task.IsCanceled)
                {
                    throw;
                }
            }
        }

        await InvokeAsync(StateHasChanged);
    }

    private async Task RunPageDeactivatedAsync()
    {
        OnPageDeactivated();
        var task = OnPageDeactivatedAsync();

        if (task.Status != TaskStatus.RanToCompletion && task.Status != TaskStatus.Canceled)
        {
            try
            {
                await task;
            }
            catch
            {
                if (!task.IsCanceled)
                {
                    throw;
                }
            }
        }
    }
    
    ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (PageStack is not null)
        {
            PageStack.Pages.PagePushed -= PagesOnPagePushed;
        }

        if (Page is not null)
        {
            Page.ActiveChanged -= PageOnActiveChanged;
        }
        
        return DisposeAsyncCore();
    }

    protected virtual ValueTask DisposeAsyncCore()
    {
        return ValueTask.CompletedTask;
    }
}