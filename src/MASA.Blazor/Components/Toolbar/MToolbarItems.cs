using BlazorComponent;

namespace MASA.Blazor
{
    public class MToolbarItems : BToolbarItems
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder.Add("m-toolbar__items");
                });

            AbstractProvider
                .Apply(typeof(IImage), typeof(MImage));
        }
    }
}
