using BlazorComponent;

namespace MASA.Blazor
{
    internal class MBreadcrumbsDivider : BBreadcrumbsDivider
    {
        public override string Tag => "li";

        protected override void SetComponentClass()
        {
            CssProvider.Apply(nameof(BBreadcrumbsDivider),css => { css.Add("m-breadcrumbs__divider"); });
        }
    }
}