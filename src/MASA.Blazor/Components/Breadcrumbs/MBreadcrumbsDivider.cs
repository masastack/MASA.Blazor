using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    internal class MBreadcrumbsDivider : BBreadcrumbsDivider
    {
        [Parameter]
        public override string Tag { get; set; }= "li";

        protected override void SetComponentClass()
        {
            CssProvider.Apply(nameof(BBreadcrumbsDivider),css => { css.Add("m-breadcrumbs__divider"); });
        }
    }
}