﻿namespace Masa.Blazor;

public partial class MBreadcrumbs : ThemeComponentBase
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public bool Large { get; set; }

    [Parameter, MasaApiParameter("/")] public string? Divider { get; set; } = "/";

    [Parameter] public RenderFragment? DividerContent { get; set; }

    [Parameter] public bool Routable { get; set; }

    [Parameter] public IReadOnlyList<BreadcrumbItem> Items { get; set; } = new List<BreadcrumbItem>();

    [Parameter] public RenderFragment<BreadcrumbItem>? ItemContent { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] [MasaApiParameter(ReleasedIn = "v1.10.0")]
    public string? Color { get; set; }

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

    private static Block _block = new("m-breadcrumbs");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder
            .Add(Large)
            .AddTextColor(Color)
            .AddTheme(ComputedTheme)
            .Build();
    }

    protected override IEnumerable<string?> BuildComponentStyle()
    {
        yield return CssStyleUtils.GetTextColor(Color);
    }
}