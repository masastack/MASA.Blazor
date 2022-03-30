namespace Masa.Blazor
{
    public partial class MTabsItems : MWindow, ITabsItems
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-tabs-items");
                });
        }
    }
}
