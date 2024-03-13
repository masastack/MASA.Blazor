using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MToolbar : MSheet
{
    [Parameter] public bool Absolute { get; set; }

    [Parameter] public bool Bottom { get; set; }

    [Parameter] public bool Collapse { get; set; }

    [Parameter] public bool Floating { get; set; }

    [Parameter] public bool Prominent { get; set; }

    [Parameter] public bool Flat { get; set; }

    [Parameter] public bool Dense { get; set; }

    [Parameter] public string? Src { get; set; }

    [Parameter] public RenderFragment<Dictionary<string, object?>>? ImgContent { get; set; }

    [Parameter] public bool Short { get; set; }

    [Parameter] [MasaApiParameter(48)] public StringNumber? ExtensionHeight { get; set; } = 48;

    [Parameter] public RenderFragment? ExtensionContent { get; set; }

    [Parameter]
    [MasaApiParameter("header")]
    public override string Tag { get; set; } = "header";

    [Parameter] public bool Extended { get; set; }

    protected virtual bool IsCollapsed => Collapse;

    public bool IsExtended => Extended || ExtensionContent != null;

    protected virtual StringNumber ComputedContentHeight
    {
        get
        {
            if (Height != null)
            {
                return Height;
            }

            if (IsProminent && Dense)
            {
                return 96;
            }

            if (IsProminent && Short)
            {
                return 112;
            }

            if (IsProminent)
            {
                return 128;
            }

            if (Dense)
            {
                return 48;
            }

            if (Short) //TODO:breakpoint
            {
                return 56;
            }

            return 64;
        }
    }

    public StringNumber ComputedHeight
    {
        get
        {
            if (!IsExtended)
            {
                return ComputedContentHeight;
            }

            return IsCollapsed
                ? ComputedContentHeight
                : ComputedContentHeight.ToInt32() + (ExtensionHeight?.ToInt32() ?? 0);
        }
    }

    protected virtual bool IsProminent => Prominent;

    private Block _block = new("m-toolbar");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            _block.Modifier(Absolute)
                .And(Bottom)
                .And(Collapse)
                .And("collapsed", IsCollapsed)
                .And(Dense)
                .And("extended", IsExtended)
                .And(Flat)
                .And(Floating)
                .And("prominent", IsProminent)
                .GenerateCssClasses());
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return base.BuildComponentStyle().Concat(new StyleBuilder().AddHeight(ComputedHeight).GenerateCssStyles());
    }

    protected virtual string? GetImageStyle() => null;
}