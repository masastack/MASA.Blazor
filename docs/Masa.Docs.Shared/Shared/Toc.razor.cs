using BlazorComponent.I18n;
using Microsoft.JSInterop;
using System.Text;
using BlazorComponent.Web;
using Element = BlazorComponent.Web.Element;

namespace Masa.Docs.Shared.Shared;

public partial class Toc : NextTickComponentBase
{
    [Inject] private AppService AppService { get; set; } = null!;

    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    [Inject] private IntersectJSModule IntersectJSModule { get; set; } = null!;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private I18n I18n { get; set; } = null!;

    [CascadingParameter(Name = "Culture")] private string? Culture { get; set; }

    [Parameter] public bool RTL { get; set; }

    private List<MarkdownItTocContent> _toc = new();
    private string? _initialHash;

    private bool _scrolling = false;
    private List<string> _activeStack = new();
    private string? _activeItem;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        AppService.TocChanged += AppServiceOnTocChanged;
        _initialHash = NavigationManager.GetHash();
    }

    private string? ActiveItem
    {
        get => _activeItem;
        set
        {
            if (_activeItem != value)
            {
                _activeItem = value;

                _ = JsRuntime.TryInvokeVoidAsync("updateHash", value);
            }
        }
    }

    private async Task ObserveToc()
    {
        const double titleHeight = 32;

        var document = await JsRuntime.InvokeAsync<Element>(JsInteropConstants.GetDomInfo, "document");
        var documentClientHeight = document.ClientHeight;
        var bottomMargin = documentClientHeight - AppService.AppBarHeight - titleHeight;

        foreach (var item in _toc)
        {
            await IntersectJSModule.ObserverAsync($"#{item.Anchor}", HandleOnIntersect, new IntersectionObserverInit()
            {
                RootMargin = $"-{AppService.AppBarHeight}px 0px -{bottomMargin}px 0px",
            });
        }
    }

    private async Task HandleOnIntersect(IntersectEventArgs e)
    {
        e.Entries.ForEach(entry =>
        {
            if (entry.IsIntersecting)
            {
                _activeStack.Add(entry.Target.Selector);
            }
            else if (_activeStack.Contains(entry.Target.Selector))
            {
                _activeStack.Remove(entry.Target.Selector);
            }
        });

        if (_activeStack.Count > 0)
        {
            ActiveItem = _activeStack.Last();
        }
        else if (ActiveItem is null)
        {
            ActiveItem = "#" + _toc.First().Anchor;
        }

        StateHasChanged();
    }

    private async Task ScrollIntoView(string hash, bool force = false)
    {
        if (NavigationManager.GetHash() == hash && !force)
        {
            return;
        }

        await JsRuntime.InvokeVoidAsync("scrollToElement", hash, AppService.AppBarHeight + 12);
    }

    private async void AppServiceOnTocChanged(object? sender, List<MarkdownItTocContent>? toc)
    {
        if (toc is null)
        {
            return;
        }

        if (_initialHash is not null)
        {
            ActiveItem = _initialHash;
            NextTick(async () =>
            {
                await Task.Delay(300);
                await ScrollIntoView(ActiveItem, force: true);
                StateHasChanged();
            });
            _initialHash = null;
        }
        else
        {
            _activeStack.Clear();
            ActiveItem = null;
        }

        foreach (var item in _toc)
        {
            await IntersectJSModule.UnobserveAsync($"#{item.Anchor}");
        }

        _toc = toc.Where(c => c.Level > 1).ToList();

        await ObserveToc();

        await InvokeAsync(StateHasChanged);
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

        if (!string.IsNullOrWhiteSpace(ActiveItem) && ActiveItem[1..].Equals(tocContent.Anchor, StringComparison.OrdinalIgnoreCase))
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
        AppService.TocChanged -= AppServiceOnTocChanged;
        base.Dispose(disposing);
    }
}
