using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MList : MasaComponentBase, ITransitionIf, IAncestorRoutable, IThemeable
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

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

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

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

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

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

#if NET8_0_OR_GREATER
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif

    private Block _block = new("m-list");
    private Block _sheetBlock = new("m-sheet");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _sheetBlock.Modifier(Outlined)
            .And(Shaped)
            .AddTheme(IsDark, IndependentTheme)
            .AddElevation(Elevation)
            .GenerateCssClasses().Concat(
                _block.Modifier(Dense)
                    .And(Disabled)
                    .And(Flat)
                    .And(Nav)
                    .And(Rounded)
                    .And(Subheader)
                    .And(TwoLine)
                    .And(ThreeLine)
                    .GenerateCssClasses()
            );
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
            .GenerateCssStyles();
    }
}