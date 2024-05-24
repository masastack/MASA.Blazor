namespace Masa.Blazor.Mixins;

public class OutsideClickJSModule : JSModule
{
    private IOutsideClickJsCallback? _owner;
    private DotNetObjectReference<OutsideClickJSModule>? _selfReference;
    private IJSObjectReference? _instance;
    private CancellationTokenSource? _cts;

    public OutsideClickJSModule(IJSRuntime js) : base(js, "./_content/BlazorComponent/js/outside-click.js")
    {
    }

    public bool Initialized { get; private set; }

    public async ValueTask InitializeAsync(IOutsideClickJsCallback owner, params string[] excludedSelectors)
    {
        _owner = owner;
        _selfReference = DotNetObjectReference.Create(this);
        _instance = await InvokeAsync<IJSObjectReference>("init", _selfReference, excludedSelectors);

        Initialized = true;
    }

    public async Task UpdateDependentElementsAsync(params string[] selectors)
    {
        if (_instance is null) return;

        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        try
        {
            await Task.Delay(16, _cts.Token);

            await _instance.InvokeAsync<bool>("updateExcludeSelectors", selectors.ToList());
        }
        catch (TaskCanceledException)
        {
            // ignored
        }
    }

    /// <summary>
    /// Remove event listener from document and dispose this module
    /// </summary>

    public async ValueTask UnbindAndDisposeAsync()
    {
        if (this is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync();
        }
    }

    /// <summary>
    /// Remove event listener from document
    /// </summary>
    public async Task UnbindAsync() => await _instance.TryInvokeVoidAsync("unbind");

    [JSInvokable]
    public async Task OnOutsideClick()
    {
        if (_owner == null) return;

        await _owner.HandleOnOutsideClickAsync();
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        await UnbindAsync();

        _selfReference?.Dispose();

        if (_instance is not null)
        {
            await _instance.DisposeAsync();
        }
    }
}
