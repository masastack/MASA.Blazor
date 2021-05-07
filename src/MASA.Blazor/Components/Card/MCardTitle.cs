using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MCardTitle : BCardTitle
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider.AsProvider<BCardTitle>().Apply(css => { css.Add("m-card__title"); });
        }
    }
}