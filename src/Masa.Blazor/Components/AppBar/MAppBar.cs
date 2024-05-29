﻿using System.ComponentModel;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public class MAppBar : MToolbar, IScrollable
{
    [Inject] private MasaBlazor? MasaBlazor { get; set; }

    [Parameter] public bool App { get; set; }

    [Parameter] public bool Fixed { get; set; }

    [Parameter] public bool ClippedLeft { get; set; }

    [Parameter] public bool ClippedRight { get; set; }

    [Parameter] public bool CollapseOnScroll { get; set; }

    [Parameter]
    [MasaApiParameter("window")]
    public string ScrollTarget { get; set; } = "window";

    /// <summary>
    /// Elevates the app-bar when scrolling.
    /// </summary>
    [Parameter]
    public bool ElevateOnScroll { get; set; }

    [Parameter] public bool FadeImgOnScroll { get; set; }

    [Parameter] public bool HideOnScroll { get; set; }

    [Parameter] public bool InvertedScroll { get; set; }

    [Parameter] public bool ShrinkOnScroll { get; set; }

    [Parameter] public double ScrollThreshold { get; set; }

    [Parameter] public bool ScrollOffScreen { get; set; }

    [Parameter] [MasaApiParameter(true)] public bool Value { get; set; } = true;

    /// <summary>
    /// Indicates the component should not be render as a SSR component.
    /// It's useful when you want render components interactively under SSR.
    /// </summary>
    [Parameter]
    public bool NoSsr { get; set; }

    private bool IsSsr => MasaBlazor.IsSsr && !NoSsr;

    private readonly string[] _applicationProperties =
    {
        "Left", "Bar", "Right"
    };

    private bool _isBooted;
    private Scroller? _scroller;
    private bool _sized;

    public int? Transform { get; private set; } = 0;

    public bool CanScroll => InvertedScroll ||
                             ElevateOnScroll ||
                             HideOnScroll ||
                             CollapseOnScroll ||
                             _isBooted ||
                             !Value;

    internal double ScrollRatio
    {
        get
        {
            var threshold = ComputedScrollThreshold;

            return Math.Max((threshold - (_scroller?.CurrentScroll ?? 0)) / threshold, 0);
        }
    }

    protected override StringNumber ComputedContentHeight
    {
        get
        {
            if (!ShrinkOnScroll)
            {
                return base.ComputedContentHeight;
            }

            var min = Dense ? 48 : 56;
            var max = ComputedOriginalHeight;

            return min + (max - min) * ScrollRatio;
        }
    }

    protected StringNumber? ComputedFontSize
    {
        get
        {
            if (!IsProminent) return null;

            var min = 1.25;
            var max = 1.5;

            return min + (max - min) * ScrollRatio;
        }
    }

    protected double ComputedLeft
    {
        get
        {
            if (MasaBlazor == null) return 0;

            if (!App || ClippedLeft) return 0;

            return MasaBlazor.Application.Left;
        }
    }

    protected double ComputedMarginTop
    {
        get
        {
            if (MasaBlazor == null) return 0;

            if (!App) return 0;

            return MasaBlazor.Application.Bar;
        }
    }

    protected double? ComputedOpacity
    {
        get
        {
            if (!FadeImgOnScroll) return null;

            return ScrollRatio;
        }
    }

    protected double ComputedOriginalHeight
    {
        get
        {
            var height = NumberHelper.ParseInt(base.ComputedContentHeight.ToString());
            if (IsExtended) height += NumberHelper.ParseInt(ExtensionHeight?.ToString());

            return height;
        }
    }

    protected double ComputedRight
    {
        get
        {
            if (MasaBlazor == null) return 0;

            if (!App || ClippedRight) return 0;

            return MasaBlazor.Application.Right;
        }
    }

    protected double ComputedScrollThreshold
    {
        get
        {
            if (ScrollThreshold > 0) return ScrollThreshold;

            return ComputedOriginalHeight - (Dense ? 48 : 56);
        }
    }

    protected double ComputedTransform
    {
        get
        {
            if (_scroller == null) return 0;

            if (!CanScroll || (ElevateOnScroll && _scroller.CurrentScroll == 0 && _scroller.IsActive)) return 0;

            if (_scroller.IsActive) return 0;

            var scrollOffScreen = ScrollOffScreen ? ComputedHeight.ToDouble() : ComputedContentHeight.ToDouble();

            return Bottom ? scrollOffScreen : -scrollOffScreen;
        }
    }

    public bool HideShadow
    {
        get
        {
            if (_scroller == null) return false;

            if (ElevateOnScroll && IsExtended)
            {
                return _scroller.CurrentScroll < ComputedScrollThreshold;
            }

            if (ElevateOnScroll)
            {
                return _scroller.CurrentScroll == 0 || ComputedTransform < 0;
            }

            return (!IsExtended || ScrollOffScreen) && ComputedTransform != 0;
        }
    }

    protected override bool IsCollapsed
    {
        get
        {
            if (_scroller == null) return false;

            if (!CollapseOnScroll) return base.IsCollapsed;

            return _scroller.CurrentScroll > 0;
        }
    }

    protected override bool IsProminent => base.IsProminent || ShrinkOnScroll;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _scroller = new Scroller(this)
        {
            IsActive = Value
        };

        if (InvertedScroll)
        {
            _scroller.IsActive = false;
        }

        MasaBlazor!.Application.PropertyChanged += ApplicationPropertyChanged;

        if (IsSsr)
        {
            _isBooted = true;
            Attributes["data-booted"] = "true";
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            _isBooted = true;
            Attributes["data-booted"] = "true";
            StateHasChanged();
        }
    }

    private void ApplicationPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_applicationProperties.Contains(e.PropertyName))
        {
            _sized = true;
            InvokeStateHasChanged();
        }
    }

    protected override string GetImageStyle()
    {
        return new StyleBuilder().AddIf("opacity", ComputedOpacity.ToString(), ComputedOpacity.HasValue).Build();
    }

    private static Block _block = new("m-app-bar");
    private ModifierBuilder _blockModifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            new[]
            {
                _blockModifierBuilder
                    .Add("clipped", ClippedLeft || ClippedRight)
                    .Add(ClippedLeft)
                    .Add(ClippedRight)
                    .Add(FadeImgOnScroll)
                    .Add(ElevateOnScroll)
                    .Add(App)
                    .Add("fixed", !Absolute && (App || Fixed))
                    .Add(HideShadow)
                    .Add("is-scrolled", _scroller is { CurrentScroll: > 0 })
                    .Add(ShrinkOnScroll)
                    .AddClass("app--sized", _sized)
                    .Build()
            }
        );
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return base.BuildComponentStyle().Concat(
            new StyleBuilder().Add("transform", $"translateY({ComputedTransform}px)")
                .AddIf("font-size", ComputedFontSize?.ToUnit("rem"), ComputedFontSize != null)
                .AddIf("margin-top", $"{ComputedMarginTop}px", !IsSsr)
                .Add("left", $"{ComputedLeft}px")
                .Add("right", $"{ComputedRight}px")
                .GenerateCssStyles()
        );
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (_scroller != null)
        {
            _scroller.ScrollThreshold = ScrollThreshold;
        }

        UpdateApplication();
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<MAppBar>>(0);
        builder.AddAttribute(1, "Value", this);
        builder.AddAttribute(2, "IsFixed", true);
        builder.AddAttribute(3, "ChildContent", (RenderFragment)(sb => base.BuildRenderTree(sb)));
        builder.CloseComponent();
    }

    private void UpdateApplication()
    {
        if (MasaBlazor == null) return;
        if (!App) return;

        var val = InvertedScroll ? 0 : ComputedHeight.ToDouble() + ComputedTransform;

        if (!Bottom)
        {
            MasaBlazor.Application.Top = val;
        }
        else
        {
            MasaBlazor.Application.Bottom = val;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await Js.AddHtmlElementEventListener(
                ScrollTarget,
                "scroll",
                async () =>
                {
                    if (!CanScroll) return;

                    await _scroller!.OnScroll(ThresholdMet);

                    StateHasChanged();
                },
                false,
                new EventListenerExtras(key: Ref.Id));
        }
    }

    protected void ThresholdMet(Scroller _)
    {
        if (_scroller == null) return;

        if (InvertedScroll)
        {
            _scroller.IsActive = _scroller.CurrentScroll > ComputedScrollThreshold;
            return;
        }

        if (HideOnScroll)
        {
            _scroller.IsActive = _scroller.IsScrollingUp || _scroller.CurrentScroll < ComputedScrollThreshold;
        }

        if (_scroller.CurrentThreshold < ComputedScrollThreshold) return;
        _scroller.SavedScroll = _scroller.CurrentScroll;
    }

    private void RemoveApplication()
    {
        if (!App)
        {
            return;
        }

        if (!Bottom)
            MasaBlazor!.Application.Top = 0;
        else
            MasaBlazor!.Application.Bottom = 0;
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        try
        {
            await Js.RemoveHtmlElementEventListener(ScrollTarget, "scroll", key: Ref.Id);
        }
        catch (Exception)
        {
            // ignored
        }

        if (MasaBlazor == null) return;

        RemoveApplication();

        MasaBlazor.Application.PropertyChanged -= ApplicationPropertyChanged;
    }
}