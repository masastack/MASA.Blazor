using System.Timers;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;
using Timer = System.Timers.Timer;

namespace Masa.Blazor;

public partial class MSnackbar : MasaComponentBase
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;
        
    [Parameter] public bool Absolute { get; set; }

    [Parameter] public bool Value { get; set; }

    [Parameter] public EventCallback<bool> ValueChanged { get; set; }

    [Parameter] public EventCallback OnClosed { get; set; }

    [Parameter] public bool Bottom { get; set; }

    [Parameter] public bool Centered { get; set; }

    [Parameter] public bool Left { get; set; }

    [Parameter] public bool MultiLine { get; set; }

    [Parameter] public bool Right { get; set; }

    [Parameter] public bool Top { get; set; }

    [Parameter] public bool Vertical { get; set; }

    [Parameter] public bool Shaped { get; set; }

    [Parameter] [MasaApiParameter(5000)] public int Timeout { get; set; } = 5000;

    [Parameter] public string? Color { get; set; }

    [Parameter] public StringNumber? Elevation { get; set; }

    [Parameter] public bool Tile { get; set; }

    [Parameter] public StringBoolean? Rounded { get; set; }

    [Parameter] public string? Action { get; set; }

    [Parameter] public RenderFragment? ActionContent { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public string? Transition { get; set; }

    [Parameter] public string? ContentClass { get; set; }

    [Parameter] public bool Text { get; set; }

    [Parameter] public bool Outlined { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    public bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            if (Light)
            {
                return false;
            }

            return CascadingIsDark;
        }
    }

    private const string ROOT_CSS = "m-snack";
    internal const string ROOT_CSS_SELECTOR = $".{ROOT_CSS}";

    private Timer? Timer { get; set; }

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

#if NET8_0_OR_GREATER
            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
#endif

        Transition ??= "m-snack-transition";

        if (Value && Timeout > 0)
        {
            if (Timer == null)
            {
                Timer = new Timer(Timeout);
                Timer.Elapsed += Timer_Elapsed;
            }

            Timer.Enabled = true;
        }
    }

    private Block _block = new(ROOT_CSS);

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier(Absolute)
            .And("active", Value)
            .And("bottom", Bottom || !Top)
            .And(Centered)
            .And("has-background", !Text && !Outlined)
            .And(Left)
            .And("multi-line", MultiLine && !Vertical)
            .And(Right)
            .And(Text)
            .And(Top)
            .And(Vertical)
            .GenerateCssClasses();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create()
            .Add("padding-bottom", "0")
            .Add("padding-top", "64px")
            .GenerateCssStyles();
    }

    private string GetWrapperClass()
    {
        return _block.Element("wrapper")
            .AddClass("m-sheet")
            .AddClass("m-sheet--outlined", Outlined)
            .AddClass("m-sheet--shaped", Shaped)
            .AddBackgroundColor(Color, !Text && !Outlined)
            .AddTextColor(Color, Text || Outlined)
            .AddRounded(Rounded, Tile)
            .AddElevation(Elevation)
            .AddTheme(IsDark, IndependentTheme)
            .Build();
    }

    private string GetWrapperStyle()
    {
        return StyleBuilder.Create()
            .AddBackgroundColor(Color)
            .AddTextColor(Color, () => Text || Outlined)
            .Build();
    }

    private void HandleOnAction()
    {
        Timer?.Stop();

        if (ValueChanged.HasDelegate)
        {
            _ = ValueChanged.InvokeAsync(false);
        }
        else
        {
            Value = false;
        }

        _ = OnClosed.InvokeAsync();
    }

    private void HandleOnPointerEnter()
    {
        Timer?.Stop();
    }

    private void HandleOnPointerLeave()
    {
        if (Value && Timeout > 0)
        {
            Timer?.Start();
        }
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        Value = false;
        if (ValueChanged.HasDelegate)
        {
            InvokeAsync(() => ValueChanged.InvokeAsync(Value));
        }

        if (OnClosed.HasDelegate)
        {
            InvokeAsync(() => OnClosed.InvokeAsync());
        }

        Timer!.Enabled = false;
        InvokeStateHasChanged();
    }
}