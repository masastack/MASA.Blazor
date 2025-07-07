using Masa.Blazor.Mixins;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public class MTabsBar : MSlideGroup
{
    [CascadingParameter] public MTabs? Tabs { get; set; }

    [Parameter] public string? BackgroundColor { get; set; }

    [Parameter] public string? Color { get; set; }

    private static Block _block = new("m-tabs-bar");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(new[]{
            _modifierBuilder.Add(IsMobile)
                .AddBackgroundColor(BackgroundColor)
                .Build()
        });
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return base.BuildComponentStyle().Concat(
            StyleBuilder.Create()
                .AddBackgroundColor(BackgroundColor)
                .GenerateCssStyles()
        );
    }

    protected override IEnumerable<string> BuildContentClass()
    {
        return base.BuildContentClass().Concat(new[] { _block.Element("content").Name });
    }

    protected override void RefreshItemsState()
    {
        base.RefreshItemsState();

        Tabs?.CallSlider();
    }

    public override void Unregister(IGroupable item)
    {
        base.Unregister(item);

        Tabs?.CallSliderAfterRender();
    }
}