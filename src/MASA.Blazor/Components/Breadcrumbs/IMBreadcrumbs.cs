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

            AbstractProvider.Apply<BBreadcrumbsDivider, MBreadcrumbsDivider>()
                            .Apply<BBreadcrumbsItem, MBreadcrumbsItem>();
        }

        public string MBreadcrumbsClasses()
        {
            var composite = new List<string>();
            composite.Add("m-breadcrumbs");
            if (Large) composite.Add("m-breadcrumbs--large");
            composite.Add(ThemeClasses());
            return String.Join(" ", composite);
        }
    }
}
