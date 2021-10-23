using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMBreadcrumbs: IBreadcrumbs,IMThemeable
    {
        public void SetComponentClass()
        {
            CssProvider
               .Apply(cssBuilder =>
               {
                   cssBuilder.Add(() => MBreadcrumbsClasses());

               });
            ////子主键BCardProgress的样式
            //CssProvider.Apply("progress", cssBuilder =>
            //{
            //    cssBuilder.Add("v-card__progress");

            //}, styleBuilder =>
            //{

            //});

            AbstractProvider.Apply<BBreadcrumbsDivider, MBreadcrumbsDivider>();
               // .Apply<BBreadcrumbsItem, MBreadcrumbsItem>();
        }

        public string MBreadcrumbsClasses()
        {
            var composite = new List<string>();
            if (Large) composite.Add("m-breadcrumbs--large");
            composite.Add(ThemeClasses());
            return String.Join(" ", composite);
        }
    }
}
