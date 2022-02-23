using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Blazor
{
    public interface IPermissionValidator
    {
        bool Validate(string code, ClaimsPrincipal user);
    }
}
