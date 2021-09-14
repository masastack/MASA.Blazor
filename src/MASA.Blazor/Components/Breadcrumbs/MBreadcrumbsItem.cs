using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                .Apply("link", css =>
                {
                    css.Add("m-breadcrumbs__item")
                        .AddIf(ActiveClass, () => Disabled);
                })
                .Apply("plain", css =>
                {
                    css.Add("m-breadcrumbs__item")
                        .AddIf(ActiveClass, () => Disabled);
                });

            AbstractProvider
                .Apply(typeof(BBreadcrumbsLinkItem<>), typeof(BBreadcrumbsLinkItem<MBreadcrumbsItem>))
                .Apply(typeof(BBreadcrumbsPlainItem<>), typeof(BBreadcrumbsPlainItem<MBreadcrumbsItem>))
                .Apply<BBreadcrumbsDivider, MBreadcrumbsDivider>();
        }
    }
}