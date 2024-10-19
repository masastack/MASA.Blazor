﻿namespace Masa.Blazor;

public partial class MSticky : MasaComponentBase
{
    [Parameter] public RenderFragment<bool>? ChildContent { get; set; }

    [Parameter] public double? OffsetTop { get; set; }

    [Parameter] public double? OffsetBottom { get; set; }

    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// The selector of the scroll target. Default is window.
    /// </summary>
    [Parameter]
    [MasaApiParameter("window")]
    public string? ScrollTarget { get; set; } = "window";

    [Parameter] [MasaApiParameter(1)] public int ZIndex { get; set; } = 1;

    private static Block _block = new("m-sticky");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private StyleBuilder _contentStyleBuilder = new();

    private bool _sticky;
    private string? _top;
    private string? _bottom;
    private double? _height;
    private double? _width;

    private string? _selector;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await OnScrollAsync();

            _selector = ScrollTarget ?? "window";
            await Js.AddHtmlElementEventListener(
                _selector,
                "scroll",
                OnScrollAsync,
                false,
                new EventListenerExtras(key: Ref.Id));
        }
    }

    private async Task OnScrollAsync()
    {
        if (Disabled)
        {
            return;
        }

        var fixedResult = await Js.InvokeAsync<StickyScrollResult>(
            JsInteropConstants.prepareSticky,
            ScrollTarget,
            Ref,
            OffsetTop,
            OffsetBottom);

        var prevTop = _top;
        var prevBottom = _bottom;

        var sticky = false;
        string? top = null;
        string? bottom = null;
        double? height = null;
        double? width = null;

        if (fixedResult.FixedTop is not null)
        {
            top = fixedResult.FixedTop;
            height = fixedResult.Height;
            width = fixedResult.Width;
            sticky = true;
        }
        else if (fixedResult.FixedBottom is not null)
        {
            bottom = fixedResult.FixedBottom;
            height = fixedResult.Height;
            width = fixedResult.Width;
            sticky = true;
        }

        if (top != prevTop || bottom != prevBottom)
        {
            _top = top;
            _bottom = bottom;
            _height = height;
            _width = width;
            _sticky = sticky;

            await InvokeAsync(StateHasChanged);
        }
    }

    protected override IEnumerable<string?> BuildComponentStyle()
    {
        return base.BuildComponentStyle()
            .Concat(StyleBuilder.Create().AddHeight(_height).AddWidth(_width).GenerateCssStyles());
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
            .AddHeight(_height, predicate: () => _sticky)
            .AddWidth(_width, predicate: () => _sticky)
            .ToString();
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (_selector is not null)
        {
            await Js.RemoveHtmlElementEventListener(_selector, "scroll", Ref.Id);
        }
    }
}

file record StickyScrollResult(double Width, double Height, string? FixedTop, string? FixedBottom);