namespace Masa.Blazor
{
    public partial class MForm : BForm
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-form");
                });
        }
    }
}
