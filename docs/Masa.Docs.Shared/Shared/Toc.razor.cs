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

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private I18n I18n { get; set; } = null!;

    [CascadingParameter(Name = "Culture")] private string? Culture { get; set; }

    [CascadingParameter] private MScrollToTarget? ScrollToTargetComp { get; set; }

    [Parameter] public bool RTL { get; set; }

    private List<MarkdownItTocContent> _toc = new();

    private bool _scrolling = false;
    private List<string> _activeStack = new();
    private string? _activeItem;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        AppService.TocChanged += AppServiceOnTocChanged;
    }

    [Parameter]
    public string? ActiveItem
    {
        get => _activeItem;
        set
        {
            if (_activeItem != value)
            {
                _activeItem = value;

                _ = JsRuntime.TryInvokeVoidAsync("updateHash", $"#{value}");
            }
        }
    }

    private async void AppServiceOnTocChanged(object? sender, List<MarkdownItTocContent>? toc)
    {
        if (toc is null)
        {
            return;
        }

        var hash = NavigationManager.GetHash();

        if (hash is not null)
        {
            NextTick(async () =>
            {
                await Task.Delay(300);
                ScrollToTargetComp?.ScrollToTarget(hash.TrimStart('#'));
            });
        }

        _toc = toc.Where(c => c.Level > 1).ToList();

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

        return builder.ToString();
    }

    protected override ValueTask DisposeAsync(bool disposing)
    {
        AppService.TocChanged -= AppServiceOnTocChanged;
        return base.DisposeAsync(disposing);
    }
}
