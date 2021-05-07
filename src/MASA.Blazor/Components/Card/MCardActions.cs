using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MCardActions : BCardActions
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider.AsProvider<BCardActions>().Apply(css => { css.Add("m-card__actions"); });
        }
    }
}