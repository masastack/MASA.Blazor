using BlazorComponent;

namespace MASA.Blazor
{
    public class MAlertDismissButton : MButton
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider.Merge(css => { css.Add("m-alert__dismissible"); });
        }
    }
}