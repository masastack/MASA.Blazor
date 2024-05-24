namespace Masa.Blazor.Components.Input;

public class InputJSModule : JSModule
{
    private IInputJsCallbacks? _owner;
    private DotNetObjectReference<InputJSModule>? _selfReference;
    private IJSObjectReference? _instance;
    private bool _isDisposed;

    public bool Initialized { get; private set; }

    public InputJSModule(IJSRuntime js) : base(js,  "./_content/BlazorComponent/js/input.js")
    {
    }

    public async ValueTask InitializeAsync(IInputJsCallbacks owner)
    {
        if (_isDisposed)
        {
            return;
        }

        _owner = owner;
        _selfReference = DotNetObjectReference.Create(this);

        _instance = await InvokeAsync<IJSObjectReference>("init",
            _selfReference,
            owner.InputElement,
            owner.InputSlotElement,
            owner.InternalDebounceInterval);

        Initialized = true;
    }

    [JSInvokable]
    public async Task OnInput(ChangeEventArgs args)
    {
        await _owner.HandleOnInputAsync(args);
        _owner.StateHasChangedForJsInvokable();
    }

    [JSInvokable]
    public async Task OnChange(ChangeEventArgs args)
    {
        await _owner.HandleOnChangeAsync(args);
        _owner.StateHasChangedForJsInvokable();
    }

    [JSInvokable]
    public async Task OnClick(ExMouseEventArgs args)
    {
        await _owner.HandleOnClickAsync(args);
        _owner.StateHasChangedForJsInvokable();
    }

    [JSInvokable]
    public async Task OnMouseUp(ExMouseEventArgs args)
    {
        await _owner.HandleOnMouseUpAsync(args);
        _owner.StateHasChangedForJsInvokable();
    }

    public async Task SetValue(string? val)
    {
        ArgumentNullException.ThrowIfNull(_instance);

        await _instance.InvokeVoidAsync("setValue", val);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        _isDisposed = true;

        _selfReference?.Dispose();

        await _instance.TryDisposeAsync();
    }
}
