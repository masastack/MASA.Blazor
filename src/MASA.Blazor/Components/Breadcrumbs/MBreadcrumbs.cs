using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Timers;

namespace MASA.Blazor
{
    public class MBreadcrumbs : BBreadcrumbs, IMBreadcrumbs
    {

        //[CascadingParameter]
        //public IThemeable Themeable { get; set; }

        //protected bool IsDark
        //{
        //    get
        //    {
        //        if (Dark)
        //        {
        //            return true;
        //        }

        //        if (Light)
        //        {
        //            return false;
        //        }

        //        return Themeable != null && Themeable.IsDark;
        //    }
        //}

        protected override void SetComponentClass()
        {
            (this as IMBreadcrumbs).SetComponentClass();
            //CssProvider
            //    .Apply(cssBuilder =>
            //    {
            //        cssBuilder
            //            .Add("m-breadcrumbs")
            //            .AddIf("m-breadcrumbs--large", () => Large)
            //            .AddTheme(IsDark);
            //    });

            //AbstractProvider
            //    .Apply(typeof(BBreadcrumbsItemGroup<>), typeof(BBreadcrumbsItemGroup<MBreadcrumbs>))
            //    .Apply<BBreadcrumbsDivider, MBreadcrumbsDivider>()
            //    .Apply<BBreadcrumbsItem, MBreadcrumbsItem>();
        }
    }
}