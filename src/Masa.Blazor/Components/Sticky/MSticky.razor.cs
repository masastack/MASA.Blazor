namespace Masa.Blazor;

public partial class MSticky : MasaComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public double? OffsetTop { get; set; }

    [Parameter] public double? OffsetBottom { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter]
    [MasaApiParameter("window")]
    public string? Container { get; set; } = "window";

    [Parameter] public int ZIndex { get; set; }

    private static Block _block = new("m-sticky");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private StyleBuilder _contentStyleBuilder = new();
    private string? _top;
    private string? _bottom;
    private double? _height;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await OnScrollAsync();
            await Js.AddHtmlElementEventListener(Container, "scroll", OnScrollAsync, false,
                new EventListenerExtras(key: Ref.Id));
        }
    }

    private async Task OnScrollAsync()
    {
        var fixedResult =
            await Js.InvokeAsync<FixedResult>(JsInteropConstants.prepareSticky, Container, Ref, OffsetTop,
                OffsetBottom);

        var prevTop = _top;
        var prevBottom = _bottom;

        _top = null;
        _bottom = null;
        _height = null;

        if (fixedResult.FixedTop is not null)
        {
            _top = fixedResult.FixedTop;
            _height = fixedResult.Height;
        }
        else if (fixedResult.FixedBottom is not null)
        {
            _bottom = fixedResult.FixedBottom;
            _height = fixedResult.Height;
        }

        if (_top != prevTop || _bottom != prevBottom)
        {
            await InvokeAsync(StateHasChanged);
        }
    }

    protected override IEnumerable<string?> BuildComponentStyle()
    {
        return base.BuildComponentStyle().Concat(StyleBuilder.Create().AddHeight(_height).GenerateCssStyles());
    }

    protected override IEnumerable<string?> BuildComponentClass()
    {
        var @fixed = !Disabled && (_top != null || _bottom != null);

        yield return _modifierBuilder.Add("fixed", @fixed).Build();
    }

    private string? GetContentStyle()
    {
        var hasTop = _top != null;
        var hasBottom = _bottom != null;

        return _contentStyleBuilder.Reset()
            .Add("z-index", ZIndex.ToString(CultureInfo.InvariantCulture), ZIndex > 0)
            .AddIf("top", _top, hasTop)
            .AddIf("bottom", _bottom, hasBottom)
            .ToString();
    }

    record FixedResult(double Width, double Height, string? FixedTop, string? FixedBottom);
}