using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.Extensions.Logging;

namespace Masa.Blazor
{
    public class MErrorHandler : ErrorBoundaryBase, IErrorHandler
    {
        [Inject]
        public IPopupService PopupService { get; set; }

        [Inject]
        protected ILogger<MErrorHandler> Logger { get; set; }

        [Parameter]
        public Func<Exception, Task>? OnErrorHandleAsync { get; set; }

        [Parameter]
        public bool Show { get; set; } = true;

        [Parameter]
        public bool ShowDetail { get; set; } = false;

        protected Exception? Exception { get; set; }

        protected override void OnParametersSet()
        {
            Exception = null;
            Recover();
        }

        protected override async Task OnErrorAsync(Exception exception)
        {
            Exception = exception;
            Logger.LogError(exception, "OnErrorAsync");
            if (OnErrorHandleAsync != null)
            {
                await OnErrorHandleAsync(exception);
            }
            else
            {
                if (Show)
                {
                    await PopupService.AlertAsync(alert =>
                    {
                        alert.Content = ShowDetail ? $"{Exception.Message}:{Exception.StackTrace}" : Exception.Message;
                        alert.Top = true;
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
            if (Show || OnErrorHandleAsync != null || CurrentException == null)
            {
                builder.AddContent(0, ChildContent);
            }
            if (OnErrorHandleAsync == null && !Show && CurrentException != null)
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
