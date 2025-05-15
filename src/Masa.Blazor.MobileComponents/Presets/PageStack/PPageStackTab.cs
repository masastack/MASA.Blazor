using Masa.Blazor.Presets.PageStack.NavController;
using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor.Presets;

public class PPageStackTab : MasaComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IPageStackNavControllerFactory PageStackNavControllerFactory { get; set; } = null!;
    [Parameter] public string? Name { get; set; }

    [Parameter] [EditorRequired] public RenderFragment<PageStackTabContext>? ChildContent { get; set; }

    [Parameter] public EventCallback<string?> OnNavigate { get; set; }

    [Parameter] [EditorRequired] public string? Href { get; set; }

    private readonly PageStackTabContext _context = new();

    private PageStackNavController? _internalPageStackNavController;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _internalPageStackNavController = PageStackNavControllerFactory.Create(Name ?? string.Empty);

        _context.Attrs["__internal_preventDefault_onclick"] = true;
        _context.Attrs["onclick"] = EventCallback.Factory.Create<MouseEventArgs>(this, HandleOnClick);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _context.Attrs["href"] = Href;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, ChildContent?.Invoke(_context));
    }

    private async Task HandleOnClick()
    {
        if (string.IsNullOrWhiteSpace(Href))
        {
            return;
        }

        var tabPath = _internalPageStackNavController?.LastVisitedTabPath ?? NavigationManager.GetAbsolutePath();
        if (tabPath.Equals(Href, StringComparison.OrdinalIgnoreCase))
        {
            _internalPageStackNavController?.NotifyTabRefresh(Href);
            return;
        }

        if (OnNavigate.HasDelegate)
        {
            await OnNavigate.InvokeAsync(Href);
        }
        else
        {
            NavigationManager.Replace(Href);
        }
    }
}

public class PageStackTabContext
{
    public Dictionary<string, object?> Attrs { get; } = new();
}