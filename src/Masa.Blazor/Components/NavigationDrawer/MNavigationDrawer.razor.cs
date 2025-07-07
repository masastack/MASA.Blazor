using System.ComponentModel;
using Masa.Blazor.Components.NavigationDrawer;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MNavigationDrawer : ThemeComponentBase, IOutsideClickJsCallback, IDependent
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private OutsideClickJSModule OutsideClickJsModule { get; set; } = null!;

    [CascadingParameter] public IDependent? CascadingDependent { get; set; }

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

    [Parameter]
    [MasaApiParameter(DefaultMiniVariantWidth)]
    public StringNumber? MiniVariantWidth { get; set; } = DefaultMiniVariantWidth;

    [Parameter] public bool Right { get; set; }

    [Parameter] public bool Touchless { get; set; }

    [Parameter]
    [MasaApiParameter(DefaultWidth)]
    public StringNumber? Width { get; set; } = DefaultWidth;

    [Parameter] public string? Color { get; set; }

    [Parameter]
    public OneOf<Breakpoints, double>? MobileBreakpoint
    {
        get => GetValue<OneOf<Breakpoints, double>?>();
        set => SetValue(value);
    }

    [Parameter] public string? OverlayColor { get; set; }

    [Parameter] public StringNumber? OverlayOpacity { get; set; }

    [Parameter] public bool Absolute { get; set; }

    [Parameter] public bool Fixed { get; set; }

    [Parameter] public RenderFragment? AppendContent { get; set; }

    [Parameter] public RenderFragment? PrependContent { get; set; }

    /// <summary>
    /// Indicates the component should not be rendered as an SSR component.
    /// It's useful when you want to render components interactively under SSR.
    /// </summary>
    [Parameter]
    public bool NoSsr { get; set; }

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

    [Parameter] public bool Permanent { get; set; }

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

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public RenderFragment<Dictionary<string, object?>>? ImgContent { get; set; }

    private const double DefaultMiniVariantWidth = 56;
    private const double DefaultWidth = 256;

    private static Block _block = new("m-navigation-drawer");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    private readonly Dictionary<string, IDictionary<string, object?>> _defaults
        = new()
        {
            [nameof(MList)] = new Dictionary<string, object?>()
            {
                [nameof(MList.BackgroundColor)] = "transparent"
            }
        };

    private bool _prevPermanent;
    private bool _prevIsMobile;
    private StringNumber _prevWidth;
    private readonly List<IDependent> _dependents = new();

    private bool _isDragging;
    private TouchJSObjectResult? _touchJSObjectResult;
    private double _overlayOpacity;
    private string? _overlayScrimStyle;

    private CancellationTokenSource? _cancellationTokenSource;

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

    protected bool IsMobile => !Stateless && !Permanent && IsMobileBreakpoint;

    protected bool ReactsToClick => !Stateless && !Permanent && (IsMobile || Temporary);

    protected bool ShowOverlay => !HideOverlay && (IsActive || _isDragging) && (IsMobile || Temporary);

    public void AddDependent(IDependent dependent)
    {
        _dependents.Add(dependent);
        NextTickIf(
            () => { _ = OutsideClickJsModule.UpdateDependentElementsAsync(DependentSelectors.ToArray()); },
            () => !OutsideClickJsModule.Initialized);
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
        StateHasChanged();
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

    protected StringNumber ComputedWidth =>
        IsMiniVariant ? MiniVariantWidth ?? DefaultMiniVariantWidth : Width ?? DefaultWidth;

    protected bool HasApp => App && (!IsMobile && !Temporary);

    protected bool IsBottom => Bottom && IsMobile;

    protected bool IsMiniVariant => (!ExpandOnHover && MiniVariant) || (ExpandOnHover && !IsMouseover);

    protected int ZIndex { get; set; }

    protected bool IsMobileBreakpoint
    {
        get
        {
            var (width, mobile, name, mobileBreakpoint) = MasaBlazor.Breakpoint;

            if (MobileBreakpoint is null || Equals(mobileBreakpoint.Value, MobileBreakpoint.Value))
            {
                return mobile;
            }

            return MobileBreakpoint.Value.IsT1
                ? width < MobileBreakpoint.Value.AsT1
                : name <= MobileBreakpoint.Value.AsT0;
        }
    }

    protected bool ReactsToResize => !DisableResizeWatcher && !Stateless;

    protected bool ReactsToMobile => App && !DisableResizeWatcher && !Permanent && !Stateless && !Temporary;

    protected bool ReactsToRoute => !DisableRouteWatcher && !Stateless && (Temporary || IsMobile);

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

        MasaBlazor.WindowSizeChanged += MasaBlazorWindowSizeChanged;

        if (Value == null && ValueChanged.HasDelegate)
        {
            var val = !MasaBlazor.Breakpoint.Mobile && !Temporary;
            _ = ValueChanged.InvokeAsync(val);
        }

        MasaBlazor.Application.PropertyChanged += ApplicationPropertyChanged;

        NavigationManager.LocationChanged += OnLocationChanged;
    }
    
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        
        if (_prevPermanent != Permanent)
        {
            _prevPermanent = Permanent;

            await UpdateApplicationAsync();
        }

        if (_prevWidth != Width)
        {
            _prevWidth = Width ?? DefaultWidth;
            SyncStateToTouchJS();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await OutsideClickJsModule.InitializeAsync(this, DependentSelectors.ToArray());

            var touch = new Touch(Js, OnTouchMove, OnTouchEnd);
            _touchJSObjectResult = await touch.UseTouchAsync(Ref, GetTouchState());

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

                SyncStateToTouchJS();
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

    private async void MasaBlazorWindowSizeChanged(object? sender, WindowSizeChangedEventArgs e)
    {
        if (_prevIsMobile == IsMobile)
        {
            return;
        }

        _prevIsMobile = IsMobile;

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
            _prevIsMobile = IsMobile;
            IsActive = !IsMobile;
        }
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        var isActive = IsActive || _isDragging;

        yield return _modifierBuilder.Add(Absolute, Bottom, Clipped, App, Floating, Right, Temporary)
            .Add("close", !isActive)
            .Add("fixed", !Absolute && (App || Fixed))
            .Add("is-mobile", IsMobile)
            .Add("is-mouseover", IsMouseover)
            .Add("mini-variant", IsMiniVariant)
            .Add("custom-mini-variant", MiniVariantWidth?.ToString() != "56")
            .Add("open", isActive)
            .Add("open-on-hover", ExpandOnHover)
            .AddTheme(ComputedTheme)
            .AddBackgroundColor(Color)
            .Build();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create()
            .AddHeight(Height)
            .AddIf("top", !IsBottom ? ComputedTop.ToUnit() : "auto", !IsSsr)
            .AddIf("max-height", $"calc(100% - {ComputedMaxHeight.ToUnit()})", !IsSsr && ComputedMaxHeight != null)
            .AddIf("transform", $"{(IsBottom ? "translateY" : "translateX")}({ComputedTransform.ToUnit("%")})",
                ComputedTransform != null)
            .AddWidth(ComputedWidth)
            .AddBackgroundColor(Color)
            .GenerateCssStyles();
    }

    private double ComputedOverlayOpacity => OverlayOpacity?.ToDouble() ?? 0.32;

    private void OnTouchMove(bool dragging, double progress)
    {
        _isDragging = dragging;
        _overlayOpacity = progress * ComputedOverlayOpacity;
        _overlayScrimStyle = "transition: none;";
        StateHasChanged();
    }

    private void OnTouchEnd(bool active)
    {
        IsActive = active;
        _isDragging = false;
        _overlayOpacity = ComputedOverlayOpacity;
        _overlayScrimStyle = null;

        StateHasChanged();
    }

    private void SyncStateToTouchJS() => _touchJSObjectResult?.SyncState(GetTouchState());

    private TouchState GetTouchState()
    {
        var position = Bottom ? "bottom" : (MasaBlazor.RTL ? (Right ? "left" : "right") : (Right ? "right" : "left"));
        return new TouchState(IsActive, ReactsToClick, ComputedWidth.ToDouble(), Touchless, position);
    }

    protected async void CallUpdate()
    {
        await UpdateApplicationAsync();
    }

    protected async Task UpdateApplicationAsync()
    {
        if (!App || IsDisposed)
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

        var element = await Js.InvokeAsync<Masa.Blazor.JSInterop.Element>(
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
        MasaBlazor.WindowSizeChanged -= MasaBlazorWindowSizeChanged;
        MasaBlazor.Application.PropertyChanged -= ApplicationPropertyChanged;
        NavigationManager.LocationChanged -= OnLocationChanged;
        _touchJSObjectResult?.Un();
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
            MasaBlazor.Application.Right = 0;
        else
            MasaBlazor.Application.Left = 0;
    }
}