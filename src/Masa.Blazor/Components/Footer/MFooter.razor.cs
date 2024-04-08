using System.ComponentModel;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MFooter : MasaComponentBase, IThemeable
{
    private readonly string[] _applicationProperties =
    {
        "Bottom", "Left", "Right"
    };

    [Inject] public MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public bool Absolute { get; set; }

    [Parameter]
    public bool App
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter] public string? Color { get; set; }

    [Parameter] public StringNumber? Elevation { get; set; }

    [Parameter] public bool Fixed { get; set; }

    [Parameter]
    [MasaApiParameter("auto")]
    public StringNumber? Height
    {
        get => GetValue((StringNumber)"auto");
        set => SetValue(value);
    }

    [Parameter] public StringNumber? Width { get; set; }

    [Parameter]
    public bool Inset
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter] public StringNumber? MaxHeight { get; set; }

    [Parameter] public StringNumber? MinHeight { get; set; }

    [Parameter] public StringNumber? MaxWidth { get; set; }

    [Parameter] public StringNumber? MinWidth { get; set; }

    [Parameter] public bool Padless { get; set; }

    [Parameter] public StringBoolean? Rounded { get; set; }

    [Parameter] public bool Tile { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

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

    private StringNumber? ComputedBottom => ComputeBottom();

    private StringNumber? ComputeBottom()
    {
        if (!IsPositioned) return null;

        return App && Inset ? MasaBlazor.Application.Bottom : 0;
    }

    private StringNumber? ComputedLeft => ComputeLeft();

    private StringNumber? ComputeLeft()
    {
        if (!IsPositioned) return null;

        return App && Inset ? MasaBlazor.Application.Left : 0;
    }

    private StringNumber? ComputedRight => ComputeRight();

    private StringNumber? ComputeRight()
    {
        if (!IsPositioned) return null;

        return App && Inset ? MasaBlazor.Application.Right : 0;
    }

    private bool IsPositioned => Absolute || Fixed || App;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        MasaBlazor.Application.PropertyChanged += ApplicationPropertyChanged;
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<bool>(nameof(App), (_, prev) =>
            {
                if (prev)
                {
                    RemoveApplication(true);
                }
                else
                {
                    CallUpdate();
                }
            }, immediate: true).Watch<StringNumber>(nameof(Height), CallUpdate)
            .Watch<bool>(nameof(Inset), CallUpdate);
    }

    private void ApplicationPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_applicationProperties.Contains(e.PropertyName))
        {
            InvokeStateHasChanged();
        }
    }

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif
    
    private Block _block = new("m-footer");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier(Absolute)
            .And("fixed", !Absolute && (App || Fixed))
            .And(Padless)
            .And(Inset)
            .AddTheme(IsDark, IndependentTheme)
            .AddBackgroundColor(Color)
            .AddElevation(Elevation)
            .AddRounded(Rounded, Tile)
            .AddClass("m-sheet")
            .GenerateCssClasses();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create()
            .AddBackgroundColor(Color)
            .AddHeight(Height)
            .AddWidth(Width)
            .AddMaxHeight(MaxHeight)
            .AddMaxWidth(MaxWidth)
            .AddMinHeight(MinHeight)
            .AddMinWidth(MinWidth)
            .AddIf("left", ComputedLeft.ToUnit(), ComputedLeft != null)
            .AddIf("right", ComputedRight.ToUnit(), ComputedRight != null)
            .AddIf("bottom", ComputedBottom.ToUnit(), ComputedBottom != null)
            .GenerateCssStyles();
    }

    private async void CallUpdate()
    {
        await NextTickIf(async () => { await UpdateApplicationAsync(); }, () => Ref.Context is null);
    }

    private async Task UpdateApplicationAsync()
    {
        if (!App)
        {
            return;
        }

        var val = Height?.ToDouble() > 0 ? Height.ToDouble() : await GetClientHeightAsync();
        if (Inset)
            MasaBlazor.Application.InsetFooter = val;
        else
            MasaBlazor.Application.Footer = val;
    }

    private async Task<double> GetClientHeightAsync()
    {
        if (Ref.Context == null)
        {
            return 0;
        }

        var element = await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, Ref);
        return element.ClientHeight;
    }

    private void RemoveApplication(bool force = false)
    {
        if (!force && !App)
        {
            return;
        }

        if (Inset)
            MasaBlazor.Application.InsetFooter = 0;
        else
            MasaBlazor.Application.Footer = 0;
    }

    protected override ValueTask DisposeAsyncCore()
    {
        MasaBlazor.Application.PropertyChanged -= ApplicationPropertyChanged;
        return base.DisposeAsyncCore();
    }
}