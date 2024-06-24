using Element = Masa.Blazor.JSInterop.Element;

namespace Masa.Blazor;

public partial class MSimpleTable : MasaComponentBase
{
    [Inject]
    private MasaBlazor MasaBlazor { get; set; } = null!;

    [CascadingParameter(Name = "DataTableHasFixed")]
    private bool HasFixed { get; set; }

    [Parameter]
    public bool Dense { get; set; }

    [Parameter]
    public bool FixedHeader { get; set; }

    [Parameter]
    public StringNumber? Height { get; set; }

    [Parameter]
    public RenderFragment? WrapperContent { get; set; }

    [Parameter]
    public StringNumber? Width { get; set; }

    [Parameter]
    public RenderFragment? TopContent { get; set; }

    [Parameter]
    public RenderFragment? BottomContent { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public bool Dark { get; set; }

    [Parameter]
    public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")]
    public bool CascadingIsDark { get; set; }

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

    private int _scrollState;
    private int _prevScrollState;

    private CancellationTokenSource? _resizeCts;
    private CancellationTokenSource? _onScrollCts;

    protected override bool AfterHandleEventShouldRender() => false;

    public ElementReference WrapperElement { get; private set; }

    private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

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

    private static Block _block = new("m-data-table");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private static BemIt.Element _wrapper = _block.Element("wrapper");

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add(Dense)
            .Add("fixed-height", Height != null && !FixedHeader)
            .Add(FixedHeader)
            .Add("has-top", TopContent != null)
            .Add("has-bottom", BottomContent != null)
            .AddTheme(IsDark, IndependentTheme)
            .Build();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await HandleOnScrollAsync(EventArgs.Empty);
            StateHasChanged();
        }
    }

    internal async Task DebounceRenderForColResizeAsync()
    {
        _resizeCts?.Cancel();
        _resizeCts = new CancellationTokenSource();
        await RunTaskInMicrosecondsAsync(StateHasChanged, 16 * 2, _resizeCts.Token);
    }

    private async Task HandleOnScrollAsync(EventArgs args)
    {
        if (!HasFixed)
        {
            return;
        }
        
        _onScrollCts?.Cancel();
        _onScrollCts = new CancellationTokenSource();
        await RunTaskInMicrosecondsAsync(Scroll, 16 * 2, _onScrollCts.Token);
        
        async Task Scroll()
        {
            var element = await Js.InvokeAsync<Element?>(JsInteropConstants.GetDomInfo, WrapperElement);
            if (element != null)
            {
                const double threshold = 1;
            
                if (Math.Abs(element.ScrollWidth - ((MasaBlazor.RTL ?  -element.ScrollLeft : element.ScrollLeft) + element.ClientWidth)) < threshold)
                {
                    _scrollState = 2;
                }
                else if (Math.Abs(element.ScrollLeft - (MasaBlazor.RTL ? element.ScrollWidth - element.ClientWidth : 0)) < threshold)
                {
                    _scrollState = 0;
                }
                else
                {
                    _scrollState = 1;
                }

                if (_prevScrollState != _scrollState)
                {
                    _prevScrollState = _scrollState;
                    StateHasChanged();
                }
            }
        }
    }
}
