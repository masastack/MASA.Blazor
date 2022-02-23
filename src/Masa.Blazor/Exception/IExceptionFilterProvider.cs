using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masa.Blazor
{
    public interface IExceptionFilterProvider
    {
        IEnumerable<IExceptionFilter> GetExceptionFilters();
    }
}
