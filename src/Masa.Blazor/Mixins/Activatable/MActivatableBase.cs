using Masa.Blazor.Mixins.Toggleable;

namespace Masa.Blazor.Mixins.Activatable;

public class MActivatableBase : MToggleable, IActivatableJsCallbacks
{
    [Inject]
    private ActivatableJsModule? ActivatableJSModule { get; set; }

    [Parameter]
    public bool Disabled
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter]
    public bool OpenOnHover
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter]
    public bool OpenOnClick { get; set; } = true;

    [Parameter]
    public bool OpenOnFocus
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter]
    public bool Eager { get; set; }

    [Parameter]
    public string? Activator { get; set; }

    private string? _activatorId;

    protected bool IsBooted { get; set; }

    public virtual Dictionary<string, object> ActivatorAttributes => new()
    {
        { ActivatorId, true },
        { "aria-haspopup", true },
        { "aria-expanded", IsActive }
    };

    protected string ActivatorId => _activatorId ??= $"_activator_{Guid.NewGuid()}";

    public string ActivatorSelector => Activator == "parent" ? $"$parent.{Ref.GetSelector()}" : (Activator ?? $"[{ActivatorId}]");

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await ActivatableJSModule!.InitializeAsync(this);
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!IsBooted && (IsActive || Eager))
        {
            IsBooted = true;
        }
    }

    protected override bool AfterHandleEventShouldRender() => false;

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher
            .Watch<bool>(nameof(Disabled), ResetActivatorEvents)
            .Watch<bool>(nameof(OpenOnFocus), ResetActivatorEvents)
            .Watch<bool>(nameof(OpenOnHover), ResetActivatorEvents)
            .Watch<int>(nameof(OpenDelay), ResetDelay)
            .Watch<int>(nameof(CloseDelay), ResetDelay);
    }

    protected override void OnValueChanged(bool value)
    {
        if (!IsBooted)
        {
            NextTick(() => RunDirectly(value));
        }
        else
        {
            RunDirectly(value);
        }
    }

    private void ResetActivatorEvents()
    {
        _ = ActivatableJSModule?.ResetEvents();
    }

    public virtual Task HandleOnClickAsync(MouseEventArgs args)
    {
        return Task.CompletedTask;
    }

    public virtual Task HandleOnOutsideClickAsync() => Task.CompletedTask;

    public async Task SetActive(bool val)
    {
        await SetActiveInternal(val);
    }

    protected void UpdateActiveInJS(bool val)
    {
        _ = ActivatableJSModule?.SetActive(val);
    }

    protected virtual void RunDirectly(bool val)
    {
        UpdateActiveInJS(val);
    }

    protected void RegisterPopupEvents(string selector, bool closeOnContentClick)
    {
        _ = ActivatableJSModule?.RegisterPopup(selector, closeOnContentClick);
    }

    protected void ResetPopupEvents(bool closeOnContentClick)
    {
        _ = ActivatableJSModule?.ResetPopupEvents(closeOnContentClick);
    }

    protected void ResetDelay()
    {
        _ = ActivatableJSModule?.ResetDelay(OpenDelay, CloseDelay);
    }
}
