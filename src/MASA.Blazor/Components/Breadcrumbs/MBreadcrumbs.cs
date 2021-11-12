using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Timers;

namespace MASA.Blazor
{
    public class MBreadcrumbs<TItem> : BBreadcrumbs<TItem>, IThemeable where TItem: BreadcrumbItem
    {
        [Parameter]
        public bool Large { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

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