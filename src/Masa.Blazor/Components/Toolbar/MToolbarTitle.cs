namespace Masa.Blazor
{
    public class MToolbarTitle : BToolbarTitle
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder.Add("m-toolbar__title");
                });
        }
    }
}
