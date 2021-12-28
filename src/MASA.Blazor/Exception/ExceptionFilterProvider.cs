using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class ExceptionFilterProvider : IExceptionFilterProvider
    {
        private IEnumerable<IExceptionFilter> _filters;

        public ExceptionFilterProvider(IEnumerable<IExceptionFilter> filters)
        {
            _filters = filters;
        }

        public IEnumerable<IExceptionFilter> GetExceptionFilters()
        {
            return _filters;
        }
    }
}
