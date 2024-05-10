using Microsoft.AspNetCore.Components.Routing;
using Router = BlazorComponent.Router;

namespace Masa.Blazor;

public partial class MBreadcrumbsItem : MasaComponentBase, IRoutable
{
    [Parameter] public string ActiveClass { get; set; } = "m-breadcrumbs__item--disabled";

    /// <summary>
    /// TODO: ripple in breadcrumbs-item
    /// </summary>
    [Parameter]
    public bool Ripple { get; set; }

    private IRoutable? _router;

    protected string WrappedTag { get; set; } = "li";

    protected bool Matched { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    [CascadingParameter] public MBreadcrumbs? Breadcrumbs { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public bool Exact { get; set; }

    [Parameter] public string? MatchPattern { get; set; }

    [Parameter] public string? Href { get; set; }

    [Parameter] public bool Link { get; set; }

    public EventCallback<MouseEventArgs> OnClick { get; }

    [Parameter, MasaApiParameter("div")] public string? Tag { get; set; } = "div";

    [Parameter] public string? Target { get; set; }

    [Parameter] public string? Text { get; set; }

    [Parameter] public RenderFragment<(bool IsLast, bool IsDisabled)>? ChildContent { get; set; }

    private bool IsDisabled => Disabled || Matched;

    private bool IsRoutable => Href != null && (Breadcrumbs?.Routable is true);
    
    private Dictionary<string, object?> _itemAttributes = new();

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
    }

    #region When using razor definition without `Items` parameter

    protected bool IsLast => Breadcrumbs == null || Breadcrumbs.SubBreadcrumbsItems.Last() == this;

    internal void InternalStateHasChanged()
    {
        StateHasChanged();
    }

    #endregion

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