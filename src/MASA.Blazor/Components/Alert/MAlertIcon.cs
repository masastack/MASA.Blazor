using BlazorComponent;

namespace MASA.Blazor
{
    public class MAlertIcon : MIcon
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();
            
            CssProvider.Merge(css => { css.Add("m-alert__icon"); });
        }
    }
}