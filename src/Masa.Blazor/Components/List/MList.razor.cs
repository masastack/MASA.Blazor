using Masa.Blazor.Components.Transition;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MList : ThemeComponentBase, ITransitionIf, IAncestorRoutable, IThemeable
{
    // TODO: add cascading value in Menu
    [CascadingParameter(Name = "IsInMenu")]
    public bool IsInMenu { get; set; }

    // TODO: add cascading value in Nav
    [CascadingParameter(Name = "IsInNav")] public bool IsInNav { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    // TODO: bool? _expand
    [Parameter] public virtual bool Expand { get; set; }

    [Parameter] public bool Routable { get; set; }

    [Parameter] [MasaApiParameter("div")] public virtual string Tag { get; set; } = "div";

    [Parameter] [MasaApiParameter(true)] public bool If { get; set; } = true;

    /// <summary>
    /// Removes elevation (box-shadow) and adds a thin border.
    /// </summary>
    [Parameter]
    public bool Outlined { get; set; }

    /// <summary>
    /// Provides an alternative active style for MListItem
    /// </summary>
    [Parameter]
    public bool Shaped { get; set; }

    [Parameter] public StringNumber? Elevation { get; set; }

    /// <summary>
    /// Lowers max height of list tiles
    /// </summary>
    [Parameter]
    public bool Dense { get; set; }

    /// <summary>
    /// Disables all children MListItem components
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Remove the highlighted background on active MListItems
    /// </summary>
    [Parameter]
    public bool Flat { get; set; }

    /// <summary>
    /// An alternative styling that reduces MListItem width and rounds the corners. Typically used with MNavigationDrawer
    /// </summary>
    [Parameter]
    public bool Nav { get; set; }

    /// <summary>
    /// Rounds the MListItem edges
    /// </summary>
    [Parameter]
    public bool Rounded { get; set; }

    /// <summary>
    /// Removes top padding. Used when previous sibling is a header
    /// </summary>
    [Parameter]
    public bool Subheader { get; set; }

    /// <summary>
    /// Increases list-item height for two lines. This prop uses line-clamp and is not supported in all browsers.
    /// </summary>
    [Parameter]
    public bool TwoLine { get; set; }

    /// <summary>
    /// Increases list-item height for three lines. This prop uses line-clamp and is not supported in all browsers.
    /// </summary>
    [Parameter]
    public bool ThreeLine { get; set; }

    /// <summary>
    /// Sets the height for the component.
    /// </summary>
    [Parameter]
    public StringNumber? Height { get; set; }

    /// <summary>
    /// Sets the maximum height for the component.
    /// </summary>
    [Parameter]
    public StringNumber? MinHeight { get; set; }

    /// <summary>
    /// Sets the minimum width for the component.
    /// </summary>
    [Parameter]
    public StringNumber? MinWidth { get; set; }

    /// <summary>
    /// Sets the maximum height for the component.
    /// </summary>
    [Parameter]
    public StringNumber? MaxHeight { get; set; }

    /// <summary>
    /// Sets the maximum width for the component.
    /// </summary>
    [Parameter]
    public StringNumber? MaxWidth { get; set; }

    /// <summary>
    /// Sets the width for the component.
    /// </summary>
    [Parameter]
    public StringNumber? Width { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.9.0")]
    public bool Slim { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.10.0")]
    public string? BackgroundColor { get; set; }

    protected List<MListGroup> Groups { get; } = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Attributes["role"] = IsInNav || IsInMenu ? null : "list";
        Attributes["id"] = Id;
    }

    internal void Register(MListGroup listGroup)
    {
        Groups.Add(listGroup);
    }

    internal void Unregister(MListGroup listGroup)
    {
        Groups.Remove(listGroup);
    }

    private static Block _block = new("m-list");
    private static Block _sheetBlock = new("m-sheet");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private ModifierBuilder _sheetModifierBuilder = _sheetBlock.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _sheetModifierBuilder
            .Add(Outlined)
            .Add(Shaped)
            .AddTheme(ComputedTheme)
            .AddElevation(Elevation)
            .Build();
        yield return _modifierBuilder
            .Add(
                Dense,
                Disabled,
                Flat,
                Nav,
                Rounded,
                Subheader,
                TwoLine)
            .Add(ThreeLine)
            .Add(Slim)
            .AddBackgroundColor(BackgroundColor)
            .Build();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create()
            .AddHeight(Height)
            .AddWidth(Width)
            .AddMinWidth(MinWidth)
            .AddMaxWidth(MaxWidth)
            .AddMinHeight(MinHeight)
            .AddMaxHeight(MaxHeight)
            .AddBackgroundColor(BackgroundColor)
            .GenerateCssStyles();
    }
}