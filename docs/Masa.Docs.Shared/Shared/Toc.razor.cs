using BlazorComponent.I18n;
using Microsoft.JSInterop;
using System.Text;
using BlazorComponent.Web;

namespace Masa.Docs.Shared.Shared;

public partial class Toc : NextTickComponentBase
{
    [Inject] private AppService AppService { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private I18n I18n { get; set; } = null!;

    private string? _activeHash;
    private List<MarkdownItTocContent> _toc = new();
    private DotNetObjectReference<Toc>? _objRef;
    private ElementReference _tocRef;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        AppService.TocChanged += AppServiceOnTocChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            try
            {
                _objRef = DotNetObjectReference.Create(this);
                NextTick(async () => { await JsRuntime.InvokeVoidAsync("registerWindowScrollEvent", _objRef, _tocRef.GetSelector()); });
            }
            catch
            {
                // ignored
            }
        }
    }

    [JSInvokable]
    public void UpdateHash(string hash)
    {
        _activeHash = hash;
        StateHasChanged();
    }

    private void AppServiceOnTocChanged(object? sender, List<MarkdownItTocContent>? toc)
    {
        if (toc is null)
        {
            return;
        }

        _toc = toc.Where(c => c.Level > 1).ToList();

        InvokeAsync(StateHasChanged);
    }

    private string GenClass(MarkdownItTocContent tocContent)
    {
        var builder = new StringBuilder();

        switch (tocContent.Level)
        {
            case 3:
                builder.Append("pl-6");
                break;
            case 4:
                builder.Append("pl-9");
                break;
            case 5:
                builder.Append("pl-12");
                break;
        }

        if (!string.IsNullOrWhiteSpace(_activeHash) && _activeHash[1..].Equals(tocContent.Anchor, StringComparison.OrdinalIgnoreCase))
        {
            builder.Append(" primary--text");
        }
        else
        {
            builder.Append(" subordinary-color");
        }

        return builder.ToString();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        AppService.TocChanged -= AppServiceOnTocChanged;
        _objRef?.Dispose();
    }
}
