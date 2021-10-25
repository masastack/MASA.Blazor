using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Timers;

namespace MASA.Blazor
{
    public class MBreadcrumbs : BBreadcrumbs, IMBreadcrumbs
    {
        protected override void SetComponentClass()
        {
            (this as IMBreadcrumbs).SetComponentClass();
        }
    }
}