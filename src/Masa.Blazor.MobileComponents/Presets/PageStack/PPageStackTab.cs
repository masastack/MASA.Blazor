using Masa.Blazor.Presets.PageStack.NavController;
using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor.Presets;

public class PPageStackTab : ComponentBase, IDisposable
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
        _internalPageStackNavController.TabBadgeUpdated += InternalPageStackNavControllerOnTabBadgeUpdated;

        _context.Attrs["__internal_preventDefault_onclick"] = true;
        _context.Attrs["onclick"] = EventCallback.Factory.Create<MouseEventArgs>(this, HandleOnClick);
    }

    private void InternalPageStackNavControllerOnTabBadgeUpdated(object? sender, PageStackTabBadgeUpdateEventArgs e)
    {
        if (Href?.Equals(e.TargetHref, StringComparison.OrdinalIgnoreCase) is not true)
        {
            return;
        }

        _context.InternalBadgeContent = e.Value ? CreateBadgeContent(e) : v => v;

        InvokeAsync(StateHasChanged);
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

    private static RenderFragment<RenderFragment> CreateBadgeContent(PageStackTabBadgeUpdateEventArgs args)
    {
        return value =>
        {
            return builder =>
            {
                builder.OpenComponent<MBadge>(0);
                builder.AddAttribute(1, nameof(MBadge.Dot), args.Dot);
                builder.AddAttribute(2, nameof(MBadge.Content), args.Content);
                builder.AddAttribute(3, nameof(MBadge.Color), args.Color);
                builder.AddAttribute(4, nameof(MBadge.OverLap), args.OverLap);
                builder.AddAttribute(5, nameof(MBadge.ChildContent),
                    (RenderFragment)(badgeBuilder => { badgeBuilder.AddContent(6, value); }));
                builder.CloseComponent();
            };
        };
    }

    public void Dispose()
    {
        if (_internalPageStackNavController is not null)
        {
            _internalPageStackNavController.TabBadgeUpdated -= InternalPageStackNavControllerOnTabBadgeUpdated;
        }
    }
}

public class PageStackTabContext
{
    internal RenderFragment<RenderFragment> InternalBadgeContent { get; set; } = value => value;

    public RenderFragment GenBadgeContent(string value)
    {
        return InternalBadgeContent(builder => { builder.AddContent(0, value); });
    }

    public RenderFragment GenBadgeContent(RenderFragment value)
    {
        return InternalBadgeContent(value);
    }

    public Dictionary<string, object?> Attrs { get; } = new();
}