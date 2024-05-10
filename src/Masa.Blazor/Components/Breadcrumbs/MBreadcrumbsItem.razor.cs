using Microsoft.AspNetCore.Components.Routing;
using Router = BlazorComponent.Router;

namespace Masa.Blazor;

public partial class MBreadcrumbsItem : MasaComponentBase, IRoutable
{
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    [CascadingParameter] public MBreadcrumbs? Breadcrumbs { get; set; }

    [Parameter] public string ActiveClass { get; set; } = "m-breadcrumbs__item--disabled";

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public bool Exact { get; set; }

    [Parameter] public string? MatchPattern { get; set; }

    [Parameter] public string? Href { get; set; }

    [Parameter] public bool Link { get; set; }

    [Parameter, MasaApiParameter("div")] public string? Tag { get; set; } = "div";

    [Parameter] public string? Target { get; set; }

    [Parameter] public string? Text { get; set; }

    [Parameter] public RenderFragment<(bool IsLast, bool IsDisabled)>? ChildContent { get; set; }

    [Parameter] public bool Ripple { get; set; } = false;

    private IRoutable? _router;

    private bool Matched { get; set; }

    private bool IsDisabled => Disabled || Matched;

    private bool IsRoutable => Href != null && (Breadcrumbs?.Routable is true);

    private Dictionary<string, object?> _itemAttributes = new();

    public EventCallback<MouseEventArgs> OnClick { get; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Breadcrumbs?.AddSubBreadcrumbsItem(this);

        NavigationManager.LocationChanged += OnLocationChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            var shouldRender = UpdateActiveForRoutable();
            if (shouldRender)
            {
                StateHasChanged();
            }
        }
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        var shouldRender = UpdateActiveForRoutable();
        if (shouldRender)
        {
            InvokeStateHasChanged();
        }
    }

    protected override void OnParametersSet()
    {
        _router = new Router(this);

        (Tag, _itemAttributes) = _router.GenerateRouteLink();
        _itemAttributes["ripple"] = Ripple;
    }

    private bool IsLast => Breadcrumbs == null || Breadcrumbs.SubBreadcrumbsItems.Last() == this;

    private bool UpdateActiveForRoutable()
    {
        var matched = Matched;

        if (IsRoutable)
        {
            Matched = _router!.MatchRoute();
        }

        return matched != Matched;
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;

        await base.DisposeAsyncCore();
    }

    private Block _block = new("m-breadcrumbs__item");
}