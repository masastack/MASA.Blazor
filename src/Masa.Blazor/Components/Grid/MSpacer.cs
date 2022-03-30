namespace Masa.Blazor
{
    public partial class MSpacer : BSpacer
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("spacer");
                });
        }
    }
}
