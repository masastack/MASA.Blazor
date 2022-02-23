using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Masa.Blazor
{
    public partial class MExpansionPanel : BExpansionPanel
    {
        protected override void SetComponentClass()
        {
            ActiveClass ??= "m-item--active";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-expansion-panel")
                        .AddIf($"m-expansion-panel--active {ComputedActiveClass}", () => InternalIsActive)
                        .AddIf("m-expansion-panel--next-active", () => NextActive)
                        .AddIf("m-expansion-panel--disabled", () => IsDisabled);
                })
                .Apply("divider", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-breadcrumbs__divider");
                });

            AbstractProvider
                .Apply<BBreadcrumbsItem, MBreadcrumbsItem>(attrs => { attrs[nameof(MBreadcrumbsItem.Class)] = "m-breadcrumbs__item"; });
        }
    }
}