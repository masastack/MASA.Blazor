namespace Masa.Blazor
{
    public class MAppBarTitle : BLabel
    {
        protected override void SetComponentClass()
        {
            CssProvider.Apply(cssBuilder =>
            {
                cssBuilder.Add("m-toolbar__title");
            });
        }
    }
}
