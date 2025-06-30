using Masa.Blazor.Presets.PageStack;
using Masa.Blazor.Presets.PageStack.NavController;
using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor.Presets;

public class PPageStackTab : ComponentBase, IDisposable
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IPageStackNavControllerFactory PageStackNavControllerFactory { get; set; } = null!;

    /// <summary>
    /// The name of <see cref="PageStackNavController"/>.
    /// </summary>
    [Parameter] public string? Name { get; set; }

    [Parameter] [EditorRequired] public RenderFragment<PageStackTabContext>? ChildContent { get; set; }

    [Parameter] public EventCallback<string?> OnNavigate { get; set; }

    [Parameter] [EditorRequired] public string Href { get; set; } = null!;

    [Parameter] [EditorRequired] public TabRule TabRule { get; set; } = null!;

    /// <summary>
    /// Initialize the badge for the tab.
    /// </summary>
    [Parameter] public PageStackTabBadgeProps? InitialBadge { get; set; }

    private readonly PageStackTabContext _context = new();

    private bool _flagForInitBadge;
    private PageStackNavController? _internalPageStackNavController;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ArgumentNullException.ThrowIfNull(TabRule, nameof(TabRule));
        ArgumentNullException.ThrowIfNull(Href, nameof(Href));

        _context.Attrs["href"] = Href;
        _context.Attrs["matchPattern"] = TabRule.Pattern;

        if (!_flagForInitBadge && InitialBadge is not null)
        {
            _flagForInitBadge = true;
            _context.InternalBadgeContent = CreateBadgeContent(InitialBadge);
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _internalPageStackNavController = PageStackNavControllerFactory.Create(Name ?? string.Empty);
        _internalPageStackNavController.TabBadgeUpdated += InternalPageStackNavControllerOnTabBadgeUpdated;

        _context.Attrs["__internal_preventDefault_onclick"] = true;
        _context.Attrs["onclick"] = EventCallback.Factory.Create<MouseEventArgs>(this, HandleOnClick);
    }

    private void InternalPageStackNavControllerOnTabBadgeUpdated(object? sender,
        PageStackTabBadgeUpdateRequestedEventArgs e)
    {
        if (e.TargetTab != TabRule)
        {
            return;
        }

        _flagForInitBadge = true;
        _context.InternalBadgeContent = e.Value ? CreateBadgeContent(e) : v => v;

        InvokeAsync(StateHasChanged);
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
        if (TabRule.Regex.IsMatch(tabPath))
        {
            _internalPageStackNavController?.NotifyTabRefresh(TabRule);
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

    private static RenderFragment<RenderFragment> CreateBadgeContent(bool dot, StringNumber? badge = null,
        bool overLap = false, string? color = "red")
    {
        return childContent =>
        {
            return builder =>
            {
                builder.OpenComponent<MBadge>(0);
                builder.AddAttribute(1, nameof(MBadge.Dot), dot);
                builder.AddAttribute(2, nameof(MBadge.Content), badge);
                builder.AddAttribute(3, nameof(MBadge.Color), color);
                builder.AddAttribute(4, nameof(MBadge.OverLap), overLap);
                builder.AddAttribute(5, nameof(MBadge.ChildContent),
                    (RenderFragment)(badgeBuilder => { badgeBuilder.AddContent(6, childContent); }));
                builder.CloseComponent();
            };
        };
    }

    private static RenderFragment<RenderFragment> CreateBadgeContent(PageStackTabBadgeProps badgeProps)
    {
        return CreateBadgeContent(badgeProps.Dot, badgeProps.Badge, badgeProps.OverLap, badgeProps.Color);
    }

    private static RenderFragment<RenderFragment> CreateBadgeContent(PageStackTabBadgeUpdateRequestedEventArgs args)
        => CreateBadgeContent(args.Dot, args.Badge, args.OverLap, args.Color);

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

    public RenderFragment GenBadgeContent(string childContent)
    {
        return InternalBadgeContent(builder => { builder.AddContent(0, childContent); });
    }

    public RenderFragment GenBadgeContent(RenderFragment childContent)
    {
        return InternalBadgeContent(childContent);
    }

    public Dictionary<string, object?> Attrs { get; } = new();
}