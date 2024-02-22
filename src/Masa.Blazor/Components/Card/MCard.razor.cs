using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MCard : MSheet, IRoutable
{
    private IRoutable? _router;

    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    [Parameter] public RenderFragment? ProgressContent { get; set; }

    /// <summary>
    /// Removes the card’s elevation.
    /// </summary>
    [Parameter]
    public bool Flat { get; set; }

    /// <summary>
    /// Will apply an elevation of 4dp when hovered (default 2dp)
    /// </summary>
    [Parameter]
    public bool Hover { get; set; }

    /// <summary>
    /// Designates that the component is a link. This is automatic when using the href or to prop.
    /// </summary>
    [Parameter]
    public bool Link { get; set; }

    /// <summary>
    /// Removes the ability to click or target the component.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Specifies a higher default elevation (8dp). 
    /// </summary>
    [Parameter]
    public bool Raised { get; set; }

    [Parameter] public string? Img { get; set; }

    [Parameter, MasaApiParameter(4)] public StringNumber LoaderHeight { get; set; } = 4;

    [Parameter, MasaApiParameter(false)] public StringBoolean Loading { get; set; } = false;

    [Parameter] public string? ActiveClass { get; set; }

    [Parameter] public string? Href { get; set; }

    [Parameter, MasaApiParameter(true)] public bool Ripple { get; set; } = true;

    [Parameter] public string? Target { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

    public bool Exact { get; }

    public string? MatchPattern { get; }

    public bool IsClickable => _router?.IsClickable is true;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _router = new Router(this);

        (Tag, Attributes) = _router.GenerateRouteLink();

        Attributes["ripple"] = Ripple && IsClickable;
        Attributes["onclick"] = OnClick;
    }

    private Block _block = new("m-card");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            _block.Modifier(Flat)
                .And(Hover)
                .And("link", IsClickable)
                .And(Disabled)
                .And(Raised)
                .GenerateCssClasses()
        );
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        var style = StyleBuilder.Create().AddIf("background", $"url(\"{Img}\") center center / cover no-repeat",
            !string.IsNullOrWhiteSpace(Img)).GenerateCssStyles();

        return base.BuildComponentStyle().Concat(style);
    }
}