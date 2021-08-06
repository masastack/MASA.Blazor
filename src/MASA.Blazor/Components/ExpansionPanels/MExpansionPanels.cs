using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MExpansionPanels : BExpansionPanels
    {
        public MExpansionPanels() : base(GroupType.ExpansionPanels)
        {
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-item-group theme--light m-expansion-panels")
                        .AddIf("m-expansion-panels--accordion", () => Accordion)
                        .AddIf("m-expansion-panels--focusable", () => Focusable)
                        .AddIf("m-expansion-panels--flat", () => Flat)
                        .AddIf("m-expansion-panels--tile", () => Tile);
                });
        }
    }
}
