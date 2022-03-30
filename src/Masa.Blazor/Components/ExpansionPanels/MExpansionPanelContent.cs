namespace Masa.Blazor
{
    public partial class MExpansionPanelContent : BExpansionPanelContent
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-expansion-panel-content");
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-expansion-panel-content__wrap");
                });
        }
    }
}
