namespace Masa.Blazor.Components.Pdfjs;

internal class PdfMobileViewerJSModule : JSModule
{
    public PdfMobileViewerJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/pdf-mobile-viewer/mobile-viewer.js")
    {
    }

    public async ValueTask Init(string container, string viewer, string url)
    {
        await InvokeVoidAsync("init", container, viewer, url);
    }
}
