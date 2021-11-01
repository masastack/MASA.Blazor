using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMBreadcrumbs: IBreadcrumbs,IThemeable
    {
        bool Large { get; }
    }
}
