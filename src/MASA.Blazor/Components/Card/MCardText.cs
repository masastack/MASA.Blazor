using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MCardText : BCardText
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-card__text");
                });
        }
    }
}