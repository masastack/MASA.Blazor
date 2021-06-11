using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MExpansionPanel : BExpansionPanel
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-expansion-panel");
                })
                .Apply("divider", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-breadcrumbs__divider");
                });

            AbstractProvider
                .Apply<BBreadcrumbsItem, MBreadcrumbsItem>(props =>
                {
                    props[nameof(MBreadcrumbsItem.Class)] = "m-breadcrumbs__item";
                });
        }
    }
}
