using Masa.Blazor.Mixins;
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

    private static Block _block = new("m-tabs-bar");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(new[]{
            _modifierBuilder.Add(IsMobile)
                .AddTextColor(ComputedColor)
                .AddBackgroundColor(BackgroundColor)
                .Build()
        });
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
        return base.BuildContentClass().Concat(new[] { _block.Element("content").Name });
    }

    public override void Unregister(IGroupable item)
    {
        base.Unregister(item);

        Tabs?.CallSliderAfterRender();
    }
}