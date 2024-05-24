using Masa.Blazor.Components.Bootable;

namespace Masa.Blazor;

public partial class MSpeedDial : MBootable
{
    [Inject] private OutsideClickJSModule? OutsideClickJSModule { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] [MasaApiParameter("top")] public string Direction { get; set; } = "top";

    [Parameter] public bool Top { get; set; }

    [Parameter] public bool Right { get; set; }

    [Parameter] public bool Bottom { get; set; }

    [Parameter] public bool Left { get; set; }

    [Parameter] public bool Fixed { get; set; }

    [Parameter] public bool Absolute { get; set; }

    [Parameter]
    [MasaApiParameter("scale-transition")]
    public string Transition { get; set; } = "scale-transition";

    [Parameter] public string? Origin { get; set; }
    private string Tag { get; set; } = "div";

    protected ElementReference ContentElement { get; set; }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<bool>(nameof(OpenOnHover), () => ResetPopupEvents(true));
    }

    protected override async Task WhenIsActiveUpdating(bool value)
    {
        await base.WhenIsActiveUpdating(value);

        if (OutsideClickJSModule is { Initialized: false } && ContentElement.TryGetSelector(out var selector))
        {
            await OutsideClickJSModule.InitializeAsync(this, ActivatorSelector, selector);

            RegisterPopupEvents(selector, true);
        }
    }

    private Dictionary<string, object> ContentAttributes => new(Attributes) { { "close-condition", IsActive } };

    public override Task HandleOnOutsideClickAsync()
    {
        if (IsActive)
        {
            RunDirectly(false);
        }

        return Task.CompletedTask;
    }

    private Block _block = new("m-speed-dial");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier(Top)
            .And(Right)
            .And(Bottom)
            .And(Left)
            .And(Absolute)
            .And(Fixed)
            .And("direction-" + Direction)
            .And("is-active", Value)
            .GenerateCssClasses();
    }
}