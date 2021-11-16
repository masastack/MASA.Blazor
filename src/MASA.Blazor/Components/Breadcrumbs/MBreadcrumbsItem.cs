using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public class MBreadcrumbsItem : BBreadcrumbsItem
    {
        [Parameter]
        public string ActiveClass { get; set; } = "m-breadcrumbs__item--disabled";

        /// <summary>
        /// TODO: ripple in breadcrumbs-item
        /// </summary>
        [Parameter]
        public bool Ripple { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(css =>
                {
                    css.Add("m-breadcrumbs__item")
                        .AddIf(ActiveClass, () => Disabled);
                });

            AbstractProvider
                .Apply<BBreadcrumbsDivider, MBreadcrumbsDivider>();
        }
    }
}