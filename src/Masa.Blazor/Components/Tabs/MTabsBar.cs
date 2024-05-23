using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public class MTabsBar : MSlideGroup
{
    [CascadingParameter] public MTabs? Tabs { get; set; }

    [Parameter] public string? BackgroundColor { get; set; }

    [Parameter] public string? Color { get; set; }

    protected string ComputedColor
    {
        get
        {
            if (Color != null) return Color;
            return IsDark ? "white" : "primary";
        }
    }

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

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

    private Block _block = new("m-tabs-bar");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            _block.Modifier(IsMobile)
                .AddTextColor(ComputedColor)
                .AddBackgroundColor(BackgroundColor)
                .GenerateCssClasses()
        );
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return base.BuildComponentStyle().Concat(
            StyleBuilder.Create()
                .AddTextColor(ComputedColor)
                .AddBackgroundColor(BackgroundColor)
                .GenerateCssStyles()
        );
    }

    protected override IEnumerable<string> BuildContentClass()
    {
        return base.BuildContentClass().Concat(_block.Element("content").GenerateCssClasses());
    }

    public override void Unregister(IGroupable item)
    {
        base.Unregister(item);

        Tabs?.CallSliderAfterRender();
    }
}