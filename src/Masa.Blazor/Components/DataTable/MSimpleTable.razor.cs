namespace Masa.Blazor;

public partial class MSimpleTable : ThemeComponentBase
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

    private int _scrollState;
    private int _prevScrollState;

    private CancellationTokenSource? _resizeCts;

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
            .AddTheme(ComputedTheme)
            .Build();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (HasFixed && WrapperElement.Context is not null)
            {
                _ = Js.InvokeVoidAsync(JsInteropConstants.RegisterTableScrollEvent, WrapperElement);
            }
        }
    }

    internal async Task DebounceRenderForColResizeAsync()
    {
        _resizeCts?.Cancel();
        _resizeCts = new CancellationTokenSource();
        await RunTaskInMicrosecondsAsync(StateHasChanged, 16 * 2, _resizeCts.Token);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (HasFixed)
        {
            _ = Js.InvokeVoidAsync(JsInteropConstants.UnregisterTableScrollEvent, WrapperElement);
        }
    }
}