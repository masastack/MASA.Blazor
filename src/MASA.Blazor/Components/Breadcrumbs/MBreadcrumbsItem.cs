using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public class MBreadcrumbsItem : BBreadcrumbsItem,IMBreadcrumbsItem
    {
        [Parameter]
        public override string ActiveClass { get; set; } = "m-breadcrumbs__item--disabled";

        protected override void SetComponentClass() => (this as IMBreadcrumbsItem).SetComponentClass();
    }
}