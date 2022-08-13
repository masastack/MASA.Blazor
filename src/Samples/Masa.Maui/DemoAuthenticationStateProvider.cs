using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Masa.Maui
{
    /// <summary>
    /// Identity status provider
    /// </summary>
    public class DemoAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.CompletedTask;
            return new AuthenticationState(new ClaimsPrincipal(new List<ClaimsIdentity>
            {
                new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,"test")
                })
            }));
        }
    }
}