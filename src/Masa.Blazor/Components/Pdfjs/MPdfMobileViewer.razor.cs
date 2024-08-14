using Masa.Blazor.Components.Pdfjs;

namespace Masa.Blazor;

public partial class MPdfMobileViewer : MasaComponentBase
{
    [Inject] private PdfMobileViewerJSModule JsModule { get; set; } = null!;

    [Parameter]
    [EditorRequired]
    public string? Url { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await JsModule.Init(Ref, Url);
        }
    }
}

