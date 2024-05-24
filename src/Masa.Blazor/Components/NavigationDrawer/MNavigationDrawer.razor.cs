using System.ComponentModel;
using System.Reflection.Metadata;
using Masa.Blazor.Mixins;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MNavigationDrawer : MasaComponentBase, IOutsideClickJsCallback, IDependent
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    [Parameter] public bool Bottom { get; set; }

    [Parameter] public bool Clipped { get; set; }

    [Parameter] public bool DisableResizeWatcher { get; set; }

    [Parameter] public bool DisableRouteWatcher { get; set; }

    [Parameter] public bool Floating { get; set; }

    [Parameter]
    public StringNumber? Height
    {
        get => GetValue<StringNumber>(App ? "100vh" : "100%");
        set => SetValue(value);
    }

    [Parameter] [MasaApiParameter(56)] public StringNumber? MiniVariantWidth { get; set; } = 56;

    [Parameter] public bool Right { get; set; }

    [Parameter] public bool Touchless { get; set; }

    [Parameter]
    [MasaApiParameter("256px")]
    public StringNumber Width { get; set; } = "256px";

    [Parameter] public string? Color { get; set; }

    [Parameter]
    public OneOf<Breakpoints, double>? MobileBreakpoint
    {
        get => GetValue(MasaBlazor.Breakpoint.MobileBreakpoint);
        set => SetValue(value);
    }

    [Parameter] public string? OverlayColor { get; set; }

    [Parameter] public StringNumber? OverlayOpacity { get; set; }

    [Parameter] public bool Absolute { get; set; }

    [Parameter] public bool Fixed { get; set; }

    [Parameter] public RenderFragment? AppendContent { get; set; }

    [Parameter] public RenderFragment? PrependContent { get; set; }

    /// <summary>
    /// Indicates the component should not be render as a SSR component.
    /// It's useful when you want render components interactively under SSR.
    /// </summary>
    [Parameter]
    public bool NoSsr { get; set; }

    private CancellationTokenSource? _cancellationTokenSource;
    private bool _disposed;

    [Inject] private OutsideClickJSModule OutsideClickJsModule { get; set; } = null!;

    [CascadingParameter] public IDependent? CascadingDependent { get; set; }

    [Parameter]
    public bool ExpandOnHover
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter]
    public bool MiniVariant
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter] public EventCallback<bool> MiniVariantChanged { get; set; }

    [Parameter]
    public bool Permanent { get; set; }

    [Parameter] public string? Src { get; set; }

    [Parameter] public bool Stateless { get; set; }

    [Parameter]
    public string Tag
    {
        get => GetValue(App ? "nav" : "aside")!;
        set => SetValue(value);
    }

    [Parameter] public bool Temporary { get; set; }

    [Parameter]
    public bool? Value
    {
        get => GetValue<bool?>();
        set => SetValue(value);
    }

    [Parameter] public EventCallback<bool?> ValueChanged { get; set; }

    [Parameter] public bool App { get; set; }

    [Parameter] public bool HideOverlay { get; set; }

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

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public RenderFragment<Dictionary<string, object?>>? ImgContent { get; set; }

    private bool _prevPermanent;
    private readonly List<IDependent> _dependents = new();
    private readonly Block _block = new("m-navigation-drawer");

    protected object? Overlay { get; set; }

    protected ElementReference? OverlayRef => (Overlay as MOverlay)?.Ref;

    protected bool IsMouseover
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    //TODO: TouchArea,StackMinZIndex

    protected bool IsActive
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    protected bool IsMobile => !Stateless && !Permanent && IsMobileBreakpoint; //TODO: fix mobile

    protected bool ReactsToClick => !Stateless && !Permanent && (IsMobile || Temporary);

    protected bool ShowOverlay => !HideOverlay && IsActive && (IsMobile || Temporary);

    public void RegisterChild(IDependent dependent)
    {
        _dependents.Add(dependent);

        NextTickWhile(() => { OutsideClickJsModule.UpdateDependentElementsAsync(DependentSelectors.ToArray()); },
            () => OutsideClickJsModule.Initialized == false);
    }

    public virtual async Task HandleOnMouseEnterAsync(MouseEventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        await RunTaskInMicrosecondsAsync(() =>
            {
                if (ExpandOnHover)
                {
                    IsMouseover = true;
                }
            },
            millisecondsDelay: 150,
            _cancellationTokenSource.Token);
    }

    public virtual async Task HandleOnMouseLeaveAsync(MouseEventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        await RunTaskInMicrosecondsAsync(() =>
            {
                if (ExpandOnHover)
                {
                    IsMouseover = false;
                }
            },
            millisecondsDelay: 150,
            _cancellationTokenSource.Token);
    }

    //TODO ontransitionend事件

    protected bool CloseConditional()
    {
        return IsActive && !IsDisposed && ReactsToClick;
    }

    public async Task HandleOnOutsideClickAsync()
    {
        if (!CloseConditional()) return;
        IsActive = false;
    }

    private bool IsSsr => MasaBlazor.IsSsr && !NoSsr;

    private readonly string[] _applicationProperties = new string[]
    {
        "Bottom", "Footer", "Bar", "Top"
    };

    protected StringNumber? ComputedMaxHeight
    {
        get
        {
            if (!HasApp) return null;

            var computedMaxHeight = MasaBlazor!.Application.Bottom + MasaBlazor.Application.Footer +
                                    MasaBlazor.Application.Bar;

            if (!Clipped) return computedMaxHeight;

            return computedMaxHeight + MasaBlazor.Application.Top;
        }
    }

    protected StringNumber ComputedTop
    {
        get
        {
            if (!HasApp)
            {
                return 0;
            }

            var computedTop = MasaBlazor!.Application.Bar;
            computedTop += Clipped ? MasaBlazor.Application.Top : 0;

            return computedTop;
        }
    }

    protected StringNumber ComputedTransform
    {
        get
        {
            if (IsActive)
            {
                return 0;
            }

            if (IsBottom)
            {
                return 100;
            }

            return Right ? 100 : -100;
        }
    }

    protected StringNumber? ComputedWidth => IsMiniVariant ? MiniVariantWidth : Width;

    protected bool HasApp => App && (!IsMobile && !Temporary);

    protected bool IsBottom => Bottom && IsMobile;

    protected bool IsMiniVariant => (!ExpandOnHover && MiniVariant) || (ExpandOnHover && !IsMouseover);

    protected int ZIndex { get; set; }

    protected bool IsMobileBreakpoint
    {
        get
        {
            var mobile = MasaBlazor.Breakpoint.Mobile;
            var width = MasaBlazor.Breakpoint.Width;
            var name = MasaBlazor.Breakpoint.Name;
            var mobileBreakpoint = MasaBlazor.Breakpoint.MobileBreakpoint;

            if (Equals(mobileBreakpoint.Value, MobileBreakpoint?.Value))
            {
                return mobile;
            }

            return mobileBreakpoint.IsT1 ? width < mobileBreakpoint.AsT1 : name == mobileBreakpoint.AsT0;
        }
    }

    protected bool ReactsToResize => !DisableResizeWatcher && !Stateless;

    protected bool ReactsToMobile => App && !DisableResizeWatcher && !Permanent && !Stateless && !Temporary;

    protected bool ReactsToRoute => !DisableRouteWatcher && !Stateless && (Temporary || IsMobile);

    protected bool IsFullscreen => MasaBlazor != null && MasaBlazor.Breakpoint.SmAndDown;

    public IEnumerable<string> DependentSelectors
    {
        get
        {
            var elements = _dependents.SelectMany(dependent => dependent.DependentSelectors).ToList();
            // do not use the Ref elementReference because it's delay assignment.
            elements.Add(Ref.GetSelector());
            elements.Add(MSnackbar.ROOT_CSS_SELECTOR);
            elements.Add(PEnqueuedSnackbars.ROOT_CSS_SELECTOR);
            return elements.Distinct();
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Init();

        _prevPermanent = Permanent;

        MasaBlazor.Breakpoint.OnUpdate += OnBreakpointOnUpdate;

        if (Value == null && ValueChanged.HasDelegate)
        {
            var val = !MasaBlazor.Breakpoint.Mobile && !Temporary;
            _ = ValueChanged.InvokeAsync(val);
        }

        MasaBlazor.Application.PropertyChanged += ApplicationPropertyChanged;

        NavigationManager.LocationChanged += OnLocationChanged;
    }

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (_prevPermanent != Permanent)
        {
            _prevPermanent = Permanent;

            await UpdateApplicationAsync();
        }

#if NET8_0_OR_GREATER
        if (MasaBlazor.IsSsr && !IndependentTheme)
        {
            CascadingIsDark = MasaBlazor.Theme.Dark;
        }
#endif
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await OutsideClickJsModule!.InitializeAsync(this, DependentSelectors.ToArray());

            await UpdateApplicationAsync();
            ZIndex = await GetActiveZIndexAsync();
            StateHasChanged();
        }
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher
            .Watch<bool?>(nameof(Value), val =>
            {
                if (Permanent)
                {
                    return;
                }

                if (val == null)
                {
                    Init();
                    return;
                }

                if (val != IsActive)
                {
                    IsActive = val.Value;
                }
            })
            .Watch<bool>(nameof(IsActive), val =>
            {
                if (ValueChanged.HasDelegate)
                {
                    ValueChanged.InvokeAsync(val);
                }
                else
                {
                    Value = val;
                }

                //We will remove this when mixins applicationable finished
                _ = UpdateApplicationAsync();
            })
            .Watch<bool>(nameof(MiniVariant), CallUpdate)
            .Watch<bool>(nameof(ExpandOnHover), val => { UpdateMiniVariant(val, false); })
            .Watch<bool>(nameof(IsMouseover), val => { UpdateMiniVariant(!val); });
    }

    private async void OnLocationChanged(object? sender,
        Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
    {
        if (ReactsToRoute && CloseConditional())
        {
            IsActive = false;
            await ValueChanged.InvokeAsync(false);
            await InvokeStateHasChangedAsync();
        }
    }

    private async void OnBreakpointOnUpdate(object? sender, BreakpointChangedEventArgs e)
    {
        if (!e.MobileChanged)
        {
            return;
        }

        if (!ReactsToResize || !ReactsToMobile)
        {
            return;
        }

        NextTick(async () =>
        {
            //When window resize,we should update ZIndex for Overlay 
            ZIndex = await GetActiveZIndexAsync();
            StateHasChanged();
        });

        IsActive = !IsMobile;

        await UpdateApplicationAsync();

        await InvokeStateHasChangedAsync();
    }

    private ValueTask<int> GetActiveZIndexAsync() => Js.InvokeAsync<int>(JsInteropConstants.GetZIndex, Ref);

    private void ApplicationPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_applicationProperties.Contains(e.PropertyName))
        {
            InvokeStateHasChanged();
        }
    }

    private void UpdateMiniVariant(bool val, bool shouldRender = true)
    {
        if (ExpandOnHover && MiniVariant != val)
        {
            if (MiniVariantChanged.HasDelegate)
            {
                MiniVariantChanged.InvokeAsync(val);
            }
            else
            {
                if (shouldRender)
                {
                    StateHasChanged();
                }
            }
        }
    }

    private void Init()
    {
        if (Permanent)
        {
            IsActive = true;
        }
        else if (Stateless || Value != null)
        {
            IsActive = Value!.Value;
        }
        else if (!Temporary)
        {
            IsActive = !IsMobile;
        }
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier(Absolute)
            .And(Bottom)
            .And(Clipped)
            .And("close", !IsActive)
            .And(App)
            .And("fixed", !Absolute && (App || Fixed))
            .And(Floating)
            .And("is-mobile", IsMobile)
            .And("is-mouseover", IsMouseover)
            .And("mini-variant", IsMiniVariant)
            .And("custom-mini-variant", MiniVariantWidth?.ToString() != "56")
            .And("open", IsActive)
            .And("open-on-hover", ExpandOnHover)
            .And(Right)
            .And(Temporary)
            .AddTheme(IsDark, IndependentTheme)
            .AddBackgroundColor(Color)
            .GenerateCssClasses();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create()
            .AddHeight(Height)
            .Add("top", !IsBottom ? ComputedTop.ToUnit() : "auto", !IsSsr)
            .Add("max-height", $"calc(100% - {ComputedMaxHeight.ToUnit()})", !IsSsr && ComputedMaxHeight != null)
            .Add("transform", $"{(IsBottom ? "translateY" : "translateX")}({ComputedTransform.ToUnit("%")})",
                ComputedTransform != null)
            .AddWidth(ComputedWidth)
            .AddBackgroundColor(Color)
            .GenerateCssStyles();
    }

    protected async void CallUpdate()
    {
        await UpdateApplicationAsync();
    }

    protected async Task UpdateApplicationAsync()
    {
        if (!App)
        {
            return;
        }

        var val = (!IsActive || IsMobile || Temporary)
            ? 0
            : (ComputedWidth.ToDouble() <= 0 ? await GetClientWidthAsync() : ComputedWidth.ToDouble());

        if (Right)
        {
            MasaBlazor.Application.Right = val;
        }
        else
        {
            MasaBlazor.Application.Left = val;
        }
    }

    private async Task<double> GetClientWidthAsync()
    {
        if (Ref.Context == null)
        {
            return 0;
        }

        var element = await Js.InvokeAsync<BlazorComponent.Web.Element>(
            JsInteropConstants.GetDomInfo, Ref);
        return element.ClientWidth;
    }

    public async Task HandleOnClickAsync(MouseEventArgs e)
    {
        if (MiniVariantChanged.HasDelegate)
        {
            await MiniVariantChanged.InvokeAsync(false);
        }
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        RemoveApplication();
        MasaBlazor!.Breakpoint.OnUpdate -= OnBreakpointOnUpdate;
        MasaBlazor.Application.PropertyChanged -= ApplicationPropertyChanged;
        NavigationManager!.LocationChanged -= OnLocationChanged;
        await OutsideClickJsModule.UnbindAndDisposeAsync();
        await base.DisposeAsyncCore();
    }

    private void RemoveApplication()
    {
        if (!App)
        {
            return;
        }

        if (Right)
            MasaBlazor!.Application.Right = 0;
        else
            MasaBlazor!.Application.Left = 0;
    }
}