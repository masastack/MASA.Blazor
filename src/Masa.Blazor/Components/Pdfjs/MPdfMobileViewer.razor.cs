using Masa.Blazor.Components.Pdfjs;

namespace Masa.Blazor;

public partial class MPdfMobileViewer : MasaComponentBase
{
    [Inject] private PdfMobileViewerJSModule JsModule { get; set; } = null!;

    [Parameter]
    [EditorRequired]
    public string? Url { get; set; }

    [Parameter]
    public bool HideToolbar { get; set; }

    /// <summary>
    /// The maximum supported canvas size in
    /// total pixels, i.e.width* height. Use `-1` for no limit, or `0` for
    /// CSS-only zooming.
    /// </summary>
    [Parameter]
    [MasaApiParameter(0)]
    public long MaxCanvasPixels { get; set; } = 0;

    private IJSObjectReference? _jsObjectReference;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _jsObjectReference = await JsModule.Init(Ref, Url, MaxCanvasPixels);
        }
    }

    private static Block Block => new Block("m-pdf-mobile-viewer");
    private ModifierBuilder BlockModifierBuilder = Block.CreateModifierBuilder();

    protected override IEnumerable<string?> BuildComponentClass()
    {
        yield return BlockModifierBuilder.Add("has-toolbar", !HideToolbar).AddClass("viewerContainer").Build();
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        await _jsObjectReference.TryInvokeVoidAsync("destroy");
    }
}

