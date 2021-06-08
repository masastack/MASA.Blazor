using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MBreadcrumbs<TItem> : BBreadcrumbs<TItem>
    {

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-breadcrumbs theme--light");
                })
                .Apply("divider", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-breadcrumbs__divider");
                });

            AbstractProvider
                .Apply<BBreadcrumbsItem, MBreadcrumbsItem>(props =>
                {
                    props[nameof(MIcon.Class)] = "m-breadcrumbs__item";
                });
        }
    }
}
