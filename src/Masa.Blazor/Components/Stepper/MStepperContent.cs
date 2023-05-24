using BlazorComponent.Web;

namespace Masa.Blazor;

public partial class MStepperContent : BStepperContent
{
    [Inject]
    public MasaBlazor MasaBlazor { get; set; } = null!;

    [CascadingParameter]
    public MStepper? Stepper { get; set; }

    [Parameter]
    public int Step { get; set; }

    private bool _firstRender = true;
    private StringNumber _height = 0;

    protected override bool IsVertical => Stepper?.Vertical is true;

    protected override bool IsRtl => MasaBlazor.RTL;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Stepper?.RegisterContent(this);
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<bool?>(nameof(NullableIsActive), IsActiveChangeCallback, immediate: true);
    }

    private async void IsActiveChangeCallback(bool? current, bool? previous)
    {
        // If active and the previous state
        // was null, is just booting up
        if (current is true && previous == null)
        {
            _height = "auto";
            await InvokeStateHasChangedAsync();
            return;
        }

        if (!IsVertical)
        {
            return;
        }

        if (IsActive)
        {
            await Enter();
        }
        else
        {
            await Leave();
        }
    }

    protected override void SetComponentClass()
    {
        CssProvider
            .Apply(cssBuilder =>
            {
                cssBuilder
                    .Add("m-stepper__content");
            }, styleBuilder =>
            {
                styleBuilder
                    .Add("transform-origin: center top 0px")
                    .AddIf("display:none", () => IsVertical && !IsActive && _firstRender);
            })
            .Apply("wrapper", cssBuilder =>
            {
                cssBuilder
                    .Add("m-stepper__wrapper")
                    .AddIf("active", () => IsActive);
            }, styleBuilder =>
            {
                styleBuilder
                    .AddIf($"height:{_height.ToUnit()}", () => _height != null && Stepper?.Vertical is true);
            });
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            _firstRender = false;

            if (Ref.TryGetSelector(out var selector))
            {
                _ =  Js.AddHtmlElementEventListener<StepperTransitionEventArgs>(selector, "transitionend", OnTransition, false);
            }
        }
    }

    private async Task OnTransition(StepperTransitionEventArgs e)
    {
        if (!IsActive || e.PropertyName != "height")
        {
            return;
        }

        _height = "auto";
        await InvokeStateHasChangedAsync();
    }

    private async Task Enter()
    {
        var scrollHeight = await GetProp<double>("scrollHeight");

        _height = 0;

        StateHasChanged();

        // Give the collapsing element time to collapse
        await Task.Delay(450);

        if (IsActive)
        {
            _height = scrollHeight == 0 ? "auto" : scrollHeight;
            StateHasChanged();
        }
    }

    private async Task Leave()
    {
        _height = await GetProp<double>("clientHeight");
        StateHasChanged();

        await Task.Delay(10);

        _height = 0;
        StateHasChanged();
    }

    public void Toggle(int step, bool reverse)
    {
        NullableIsActive = Step == step;
        IsReverse = reverse;

        StateHasChanged();
    }

    private async Task<T> GetProp<T>(string identifier)
    {
        return await Js.InvokeAsync<T>(JsInteropConstants.GetProp, Ref, identifier);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        Stepper?.UnRegisterContent(this);

        if (Ref.TryGetSelector(out var selector))
        {
            try
            {
                _ =  Js.RemoveHtmlElementEventListener(selector, "transitionend");
            }
            catch
            {
                // ignored
            }
        }
    }

    private class StepperTransitionEventArgs
    {
        public string PropertyName { get; set; } = null!;
    }
}
