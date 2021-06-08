using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
   public class MBreadcrumbsItem: BBreadcrumbsItem
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(Disabled ? "m-breadcrumbs__item m-breadcrumbs__item--disabled" : "m-breadcrumbs__item");
                })
                .Apply("divider", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-breadcrumbs__divider");
                });
        }
    }
}
