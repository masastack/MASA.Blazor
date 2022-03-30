using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using System.Security.Claims;

namespace Masa.Blazor
{
    public class PermissionView : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Parameter]
        public string Code { get; set; }

        [Inject]
        public IPermissionValidator Validator { get; set; }

        protected ClaimsPrincipal User { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (AuthenticationStateTask == null)
            {
                throw new ArgumentNullException(nameof(AuthenticationStateTask));
            }

            if (string.IsNullOrEmpty(Code))
            {
                throw new ArgumentException("Code is required");
            }

            if (Validator == null)
            {
                throw new ArgumentNullException(nameof(Validator));
            }

            if (User == null)
            {
                var state = await AuthenticationStateTask;
                User = state.User;
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var valid = Validator.Validate(Code, User);
            if (valid)
            {
                builder.AddContent(0, ChildContent);
            }
        }
    }
}
