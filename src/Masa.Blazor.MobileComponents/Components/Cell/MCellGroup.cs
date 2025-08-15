using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor;

public class MCellGroup : MasaComponentBase
{
    [Parameter] public bool Dense { get; set; }

    [Parameter] public bool HideDivider { get; set; }

    [Parameter] public string? Subheader { get; set; }

    [Parameter]
    [MasaApiParameter("transparent")]
    public string? BackgroundColor { get; set; } = "transparent";

    [Parameter] public bool Outlined { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public string? Theme { get; set; }

    [Parameter] public StringNumber? CellMinHeight { get; set; }

    private static Block _block = new("m-cell-group");
    private readonly ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private readonly List<MCell> _items = [];

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<MCellGroup>>(0);
        builder.AddAttribute(1, "Value", this);
        builder.AddAttribute(2, "IsFixed", true);
        builder.AddAttribute(3, "ChildContent", RenderList());
        builder.CloseComponent();
    }

    private RenderFragment RenderList() => builder =>
    {
        if (!string.IsNullOrWhiteSpace(Subheader))
        {
            builder.OpenComponent<MSubheader>(0);
            builder.AddAttribute(1, nameof(ChildContent), RenderFragments.RenderText(Subheader));
            builder.CloseComponent();
        }

        builder.OpenComponent<MList>(2);
        builder.AddAttribute(3, nameof(MList.Dense), Dense);
        builder.AddAttribute(4, nameof(MList.Class), GetClass());
        builder.AddAttribute(5, nameof(MList.Style), GetStyle());
        builder.AddAttribute(6, nameof(MList.Theme), Theme);
        builder.AddAttribute(7, nameof(MList.Outlined), Outlined);
        builder.AddAttribute(8, nameof(MList.BackgroundColor), BackgroundColor);
        builder.AddAttribute(9, nameof(ChildContent), ChildContent);
        builder.CloseComponent();
    };

    protected override IEnumerable<string?> BuildComponentClass()
    {
        var css = _modifierBuilder.Build();
        return [css, ..base.BuildComponentClass()];
    }

    internal void AddCell(MCell cell)
    {
        _items.Add(cell);
    }

    internal bool AppendDivider(MCell cell)
    {
        return !HideDivider && _items.IndexOf(cell) != _items.Count - 1;
    }
}