using BlazorComponent.I18n;
using Microsoft.JSInterop;
using System.Text;

namespace Masa.Docs.Shared.Shared;

public partial class Toc : NextTickComponentBase
{
    [Inject] private AppService AppService { get; set; } = null!;

    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private I18n I18n { get; set; } = null!;

    [CascadingParameter(Name = "Culture")] private string? Culture { get; set; }

    [Parameter] public bool RTL { get; set; }

    private string? _activeHash;
    private List<MarkdownItTocContent> _toc = new();
    private DotNetObjectReference<Toc>? _objRef;

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
            _objRef = DotNetObjectReference.Create(this);
            await JsRuntime.InvokeVoidAsync("registerWindowScrollEvent", _objRef, ".toc-li");
        }
    }

    [JSInvokable]
    public void UpdateHash(string hash)
    {
        _activeHash = hash;
        StateHasChanged();
    }

    private async Task ScrollIntoView(string hash, bool needsRender = false)
    {
        try
        {
            _activeHash = hash;

            if (needsRender)
            {
                StateHasChanged();
            }

            _ = JsRuntime.InvokeVoidAsync("scrollToElement", hash, AppService.AppBarHeight + 12, true);
        }
        catch
        {
            // ignored
        }
    }

    private void AppServiceOnTocChanged(object? sender, List<MarkdownItTocContent>? toc)
    {
        if (toc is null)
        {
            return;
        }

        _toc = toc.Where(c => c.Level > 1).ToList();

        NextTick(async () =>
        {
            await Task.Delay(300);

            var uri = new Uri(NavigationManager.Uri);
            if (string.IsNullOrWhiteSpace(uri.Fragment)) return;

            _ = ScrollIntoView(uri.Fragment, true);
        });

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
            builder.Append(" secondary--text");
        }

        return builder.ToString();
    }

    protected override void Dispose(bool disposing)
    {
        _objRef?.Dispose();
        AppService.TocChanged -= AppServiceOnTocChanged;
        base.Dispose(disposing);
    }
}
