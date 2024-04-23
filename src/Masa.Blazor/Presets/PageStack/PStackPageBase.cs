using Microsoft.AspNetCore.Components.Routing;

// ReSharper disable MethodHasAsyncOverload

namespace Masa.Blazor.Presets;

public class PStackPageBase : ComponentBase, IDisposable
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [CascadingParameter] private PPageStack? PageStack { get; set; }

    private StackPageData? Page { get; set; }
    private bool _isActive;

    protected string? PageSelector => Page?.Selector;

    protected override async Task OnInitializedAsync()
    {
        if (PageStack is null)
        {
            return;
        }

        Page = PageStack.Pages.FirstOrDefault(u => u.AbsolutePath == NavigationManager.GetAbsolutePath());
        // Todo: Page is possibly null?

        NavigationManager.LocationChanged += NavigationManagerOnLocationChanged;

        await RunPageActivatedAsync();
        _isActive = true;
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        var absolutePath = new Uri(e.Location).AbsolutePath;
        var isActive = Regex.IsMatch(absolutePath, Page!.Pattern);
        if (_isActive == isActive) return;
        _isActive = isActive;

        _ = isActive ? RunPageActivatedAsync() : RunPageDeactivatedAsync();
    }

    /// <summary>
    /// This event is triggered when the page is on the top of the stack.
    /// In other words, it is triggered when the page is rendered in the
    /// viewport, including the <see cref="OnInitializedAsync"/>.
    /// </summary>
    protected virtual void OnPageActivated()
    {
    }

    /// <summary>
    /// This event is triggered when the page is on the top of the stack.
    /// In other words, it is triggered when the page is rendered in the
    /// viewport, including the <see cref="OnInitializedAsync"/>.
    /// </summary>
    protected virtual Task OnPageActivatedAsync() => Task.CompletedTask;

    /// <summary>
    /// This event is triggered when a new page is pushed into the stack
    /// or current page is popped.
    /// </summary>
    protected virtual void OnPageDeactivated()
    {
    }

    /// <summary>
    /// This event is triggered when a new page is pushed into the stack
    /// or current page is popped.
    /// </summary>
    protected virtual Task OnPageDeactivatedAsync() => Task.CompletedTask;

    private async Task RunPageActivatedAsync()
    {
        OnPageActivated();
        var task = OnPageActivatedAsync();

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


    public void Dispose()
    {
        NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;
    }
}