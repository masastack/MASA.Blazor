using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MExpansionPanelContent : BExpansionPanelContent
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-expansion-panel-content mt-2");
                }, styleBuilder =>
                {
                    styleBuilder.AddIf("display:none", () => !ExpansionPanel.Expanded);
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-expansion-panel-content__wrap");
                });
        }
    }
}
