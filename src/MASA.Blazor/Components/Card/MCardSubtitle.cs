using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MCardSubtitle : BCardSubtitle
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .AsProvider<BCardSubtitle>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-card__subtitle");
                });
        }
    }
}