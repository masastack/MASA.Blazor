using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Timers;

namespace MASA.Blazor
{
    public class MBreadcrumbs : BBreadcrumbs, IBreadcrumbs, IThemeable
    {
        [Parameter]
        public bool Large { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
               .Apply(cssBuilder =>
               {
                   cssBuilder.Add("m-breadcrumbs")
                             .AddIf("m-breadcrumbs--large", () => Large == true)
                             .AddTheme(this);
               });

            AbstractProvider.Apply<BBreadcrumbsDivider, MBreadcrumbsDivider>()
                            .Apply<BBreadcrumbsItem, MBreadcrumbsItem>();
        }
    }
}