using System.Security.Claims;

namespace Masa.Blazor
{
    public interface IPermissionValidator
    {
        bool Validate(string code, ClaimsPrincipal user);
    }
}
