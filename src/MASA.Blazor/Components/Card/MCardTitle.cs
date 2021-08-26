using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MCardTitle : BCardTitle
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-card__title");
                });
        }
    }
}