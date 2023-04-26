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
            try
            {
                _objRef = DotNetObjectReference.Create(this);
                await JsRuntime.InvokeVoidAsync("registerWindowScrollEvent", _objRef, ".toc-li");
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
        NavigationManager.ReplaceWithHash(hash);
        StateHasChanged();
    }

    private async Task ScrollIntoView(string elementId)
    {
        try
        {
            _activeHash = $"#{elementId}";

            // TODO: remove the following lines when #40190 of aspnetcore resolved.
            // TODO: Blazor now does not support automatic scrolling of anchor points.
            // Check this when .NET 8 released.
            NavigationManager.ReplaceWithHash($"#{elementId}");
            _ = JsRuntime.InvokeVoidAsync("scrollToElement", elementId, AppService.AppBarHeight + 12);
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

        InvokeAsync(StateHasChanged);

        var uri = new Uri(NavigationManager.Uri);
        if (!string.IsNullOrWhiteSpace(uri.Fragment))
        {
            ScrollIntoView(uri.Fragment.Substring(1)).ConfigureAwait(false);
        }
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
