using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masa.Blazor
{
    public interface IExceptionFilter
    {
        void OnException(ExceptionContext context);
    }
}
