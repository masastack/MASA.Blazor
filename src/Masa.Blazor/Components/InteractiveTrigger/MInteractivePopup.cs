namespace Masa.Blazor;

/// <summary>
/// A abstract class for components that use the <see cref="MInteractiveTrigger{TValue}"/>
/// or <see cref="MInteractiveTriggers{TValue}"/> component as a trigger.
/// </summary>
public abstract class MInteractivePopup : ComponentBase, IOutsideClickJsCallback, IAsyncDisposable
{
    [Inject] private OutsideClickJSModule OutsideClickJSModule { get; set; } = null!;

    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// The query name of url for trigger a interactive popup. 
    /// </summary>
    [Parameter] public string QueryName { get; set; } = null!;

    /// <summary>
    /// The activator selector.
    /// </summary>
    [Parameter] public string Activator { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await OutsideClickJSModule.InitializeAsync(this, Activator, ".m-interactive-trigger__popup");
        }
    }

    public async Task HandleOnOutsideClickAsync()
    {
        await Task.Delay(1); // https://github.com/dotnet/aspnetcore/issues/52705
        NavigationManager.NavigateWithQueryParameter(QueryName, (string?)null);
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            await DisposeAsync();
            await OutsideClickJSModule.UnbindAndDisposeAsync();
        }
        catch (JSDisconnectedException)
        {
            // ignore
        }
    }

    protected virtual Task DisposeAsync() => Task.CompletedTask;
}
