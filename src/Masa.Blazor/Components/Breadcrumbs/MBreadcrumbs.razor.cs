namespace Masa.Blazor;

public partial class MBreadcrumbs : MasaComponentBase
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public bool Large { get; set; }

    [Parameter, MasaApiParameter("/")] public string? Divider { get; set; } = "/";

    [Parameter] public RenderFragment? DividerContent { get; set; }

    [Parameter] public bool Routable { get; set; }

    [Parameter] public IReadOnlyList<BreadcrumbItem> Items { get; set; } = new List<BreadcrumbItem>();

    [Parameter] public RenderFragment<BreadcrumbItem>? ItemContent { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    public bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            if (Light)
            {
                return false;
            }

            return CascadingIsDark;
        }
    }

    #region When using razor definition without Items parameter

    internal List<MBreadcrumbsItem> SubBreadcrumbsItems { get; } = new();

    internal void AddSubBreadcrumbsItem(MBreadcrumbsItem item)
    {
        if (!SubBreadcrumbsItems.Contains(item))
        {
            SubBreadcrumbsItems.Add(item);
        }
    }

    #endregion

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

#if NET8_0_OR_GREATER
        if (MasaBlazor.IsSsr && !IndependentTheme)
        {
            CascadingIsDark = MasaBlazor.Theme.Dark;
        }
#endif
    }

    private Block _block = new("m-breadcrumbs");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier(Large)
            .AddTheme(IsDark, IndependentTheme)
            .GenerateCssClasses();
    }
}