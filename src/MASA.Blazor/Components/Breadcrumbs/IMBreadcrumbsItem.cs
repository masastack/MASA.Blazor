using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMBreadcrumbsItem: IBreadcrumbsItem, IMRoutable
    {
        public void SetComponentClass()
        {
            ElementProps = GenerateRouteLink(BreadcrumbsItemClass());
        }

        public string BreadcrumbsItemClass()
        {
            var composite = new List<string>();
            composite.Add("m-breadcrumbs__item");
            if(Disabled) composite.Add(ActiveClass);           
            return String.Join(" ", composite);
        }
    }
}
