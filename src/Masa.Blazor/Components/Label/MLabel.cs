using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public class MLabel : ThemeContainer
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public bool Disabled { get; set; }

    [Parameter] [MasaApiParameter(0)] public StringNumber? Left { get; set; } = 0;

    [Parameter] [MasaApiParameter("auto")] public StringNumber? Right { get; set; } = "auto";

    [Parameter] public bool Absolute { get; set; }

    [Parameter] public bool Focused { get; set; }

    [Parameter]
    [MasaApiParameter("primary")]
    public string? Color { get; set; } = "primary";

    [Parameter] public bool Value { get; set; }

    [Parameter] public string? For { get; set; }

    [Parameter] public bool Required { get; set; }

    [Parameter]
    [MasaApiParameter("label")]
    public string Tag { get; set; } = "label";

    protected override string TagName => Tag;

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
        Attributes["for"] = For;
        Attributes["required"] = Required;
    }

    private static Block _block = new("m-label");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add("active", Value)
            .Add("is-disabled", Disabled)
            .AddTheme(IsDark, IndependentTheme)
            .AddTextColor(Color, Focused)
            .Build();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create().Add("left", Left.ToUnit())
            .Add("right", Right.ToUnit())
            .Add("position", (Absolute ? "absolute" : "relative"))
            .AddTextColor(Color, () => Focused)
            .GenerateCssStyles();
    }
}