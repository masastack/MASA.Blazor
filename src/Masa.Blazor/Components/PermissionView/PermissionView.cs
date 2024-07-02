using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Masa.Blazor
{
    public class PermissionView : ComponentBase
    {
        [Inject]
        public IPermissionValidator? Validator { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState>? AuthenticationStateTask { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public RenderFragment? FallbackContent { get; set; }

        [Parameter, EditorRequired]
        public string Code { get; set; } = null!;

        protected ClaimsPrincipal? User { get; set; }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            Code.ThrowIfNull(nameof(PermissionView));
        }

        protected override async Task OnInitializedAsync()
        {
            if (AuthenticationStateTask != null && User == null)
            {
                var state = await AuthenticationStateTask;
                User = state.User;
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (User == null || Validator == null || !Validator.Validate(Code, User))
            {
                builder.AddContent(0, FallbackContent);
            }
            else
            {
                builder.AddContent(0, ChildContent);
            }
        }
    }
}
