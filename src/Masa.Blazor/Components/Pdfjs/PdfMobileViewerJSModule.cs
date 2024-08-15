namespace Masa.Blazor.Components.Pdfjs;

internal class PdfMobileViewerJSModule : JSModule
{
    public PdfMobileViewerJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/pdf-mobile-viewer/mobile-viewer.js")
    {
    }

    public ValueTask<IJSObjectReference> Init(ElementReference el, string url, long maxCanvasPixels)
    {
        return InvokeAsync<IJSObjectReference>("init", el, url, maxCanvasPixels);
    }
}
