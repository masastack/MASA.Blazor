using BemIt;
using Masa.Blazor.Attributes;
using Masa.Blazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Masa.Blazor;

public partial class MPdfMobileViewer : MasaComponentBase
{
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
    public long MaxCanvasPixels { get; set; }

    /// <summary>
    /// The maximum allowed image size in total pixels, i.e., width * height.
    /// Images above this value will not be rendered.
    /// Use -1 for no limit.
    /// </summary>
    [Parameter]
    [MasaApiParameter("1024 * 1024")]
    public long MaxImageSize { get; set; } = 1024 * 1024;

    private IJSObjectReference? _jsObjectReference;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Url.ThrowIfNull(ComponentName);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (string.IsNullOrWhiteSpace(Url))
            {
                return;
            }

            var url = Url;
            if (url.StartsWith("_content") is true)
            {
                url = "/" + url;
            }

            var importJSReference = await Js
                .InvokeAsync<IJSObjectReference>("import",
                    "./_content/Masa.Blazor.JSComponents.PdfJS/mobile-viewer.js").ConfigureAwait(false);
            _jsObjectReference = await importJSReference
                .InvokeAsync<IJSObjectReference>("init", Ref, url, MaxCanvasPixels, MaxImageSize).ConfigureAwait(false);
            _ = importJSReference.DisposeAsync().ConfigureAwait(false);
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