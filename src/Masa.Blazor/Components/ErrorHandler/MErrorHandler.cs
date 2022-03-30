using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;

namespace Masa.Blazor
{
    public class MErrorHandler : ErrorBoundaryBase
    {
        [Inject]
        protected ILogger<MErrorHandler> Logger { get; set; }

        [Parameter]
        public Func<Exception, Task> OnErrorHandleAsync { get; set; }

        [Inject]
        public IPopupService PopupService { get; set; }

        [Parameter]
        public bool ShowAlert { get; set; } = true;

        [Parameter]
        public bool ShowDetail { get; set; } = false;

        protected override void OnParametersSet()
        {
            Recover();
        }

        protected override async Task OnErrorAsync(Exception exception)
        {
            Logger.LogError(exception, "OnErrorAsync");
            if (OnErrorHandleAsync != null)
            {
                await OnErrorHandleAsync(exception);
            }
            else
            {
                if (ShowAlert)
                {
                    await PopupService.AlertAsync(alert =>
                    {
                        alert.Top = true;
                        alert.Type = AlertTypes.Error;
                        alert.Content = ShowDetail ? $"{exception.Message}:{exception.StackTrace}" : exception.Message;
                    });
                }
            }
        }

        public async Task HandlerExceptionAsync(Exception exception)
        {
            await OnErrorAsync(exception);
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (CurrentException is null || ShowAlert || OnErrorHandleAsync is not null)
            {
                builder.AddContent(0, ChildContent);
            }
            else if (OnErrorHandleAsync == null && !ShowAlert && CurrentException != null)
            {
                if (ErrorContent != null)
                {
                    builder.AddContent(1, ErrorContent!(CurrentException));
                    return;
                }

                builder.OpenElement(2, "div");
                builder.AddAttribute(3, "class", "blazor-error-boundary");
                builder.CloseElement();
            }
        }
    }
}
