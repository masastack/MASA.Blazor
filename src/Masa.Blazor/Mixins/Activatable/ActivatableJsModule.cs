namespace Masa.Blazor.Mixins.Activatable;

public class ActivatableJsModule : JSModule
{
    private IActivatableJsCallbacks? _owner;
    private DotNetObjectReference<ActivatableJsModule>? _selfReference;
    private IJSObjectReference? _instance;

    public ActivatableJsModule(IJSRuntime js) : base(js, "./_content/BlazorComponent/js/activatable.js")
    {
    }

    public async ValueTask InitializeAsync(IActivatableJsCallbacks owner)
    {
        _owner = owner;
        _selfReference = DotNetObjectReference.Create(this);
        _instance = await InvokeAsync<IJSObjectReference>("init",
            _owner.ActivatorSelector,
            _owner.Disabled,
            _owner.OpenOnClick,
            _owner.OpenOnHover,
            _owner.OpenOnFocus,
            _owner.OpenDelay,
            _owner.CloseDelay,
            _selfReference
        );
    }

    public async Task ResetDelay(int openDelay, int closeDelay)
    {
        if (_instance == null) return;

        await _instance.InvokeVoidAsync("resetDelay", openDelay, closeDelay);
    }

    public async Task ResetEvents()
    {
        if (_instance == null || _owner == null) return;

        await _instance.InvokeVoidAsync("resetActivatorEvents", _owner.Disabled, _owner.OpenOnHover, _owner.OpenOnFocus);
    }

    public async Task SetActive(bool val)
    {
        if (_instance == null) return;

        await _instance.InvokeVoidAsync("setActive", val);
    }

    public async Task RegisterPopup(string popupSelector, bool closeOnContentClick)
    {
        if (_instance == null) return;

        await _instance.InvokeVoidAsync("registerPopup", popupSelector, closeOnContentClick);
    }

    public async Task ResetPopupEvents(bool closeOnContentClick)
    {
        if (_instance == null) return;

        await _instance.InvokeVoidAsync("resetPopupEvents", closeOnContentClick);
    }

    [JSInvokable("SetActive")]
    public async Task JSSetActive(bool val)
    {
        if (_owner == null) return;

        await _owner.SetActive(val);
    }

    [JSInvokable]
    public async Task OnClick(MouseEventArgs args)
    {
        if (_owner == null) return;

        await _owner.HandleOnClickAsync(args);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        _selfReference?.Dispose();

        if (_instance != null)
        {
            await _instance.DisposeAsync();
        }
    }
}
