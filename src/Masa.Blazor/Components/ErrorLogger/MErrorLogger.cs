using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;

namespace Masa.Blazor
{
    public class MErrorLogger : ErrorBoundaryBase, IErrorLogger
    {
        [Inject]
        public IPopupService PopupService { get; set; }

        [Parameter]
        public Func<Exception, Task>? OnErrorHandleAsync { get; set; }

        [Parameter]
        public bool IsShow { get; set; } = true;

        [Parameter]
        public bool IsShowDetail { get; set; } = false;

        protected Exception? Exception { get; set; }

        protected override async Task OnErrorAsync(Exception exception)
        {
            Exception = exception;
            if (OnErrorHandleAsync != null)
            {
                await OnErrorHandleAsync(exception);
            }
            else
            {
                if (IsShow)
                {
                    await PopupService.AlertAsync(alert =>
                    {
                        alert.Content = IsShowDetail ? $"{Exception.Message}:{Exception.StackTrace}" : Exception.Message;
                        alert.Top = true;
                    });
                }
            }
        }

        public async Task HandlerExceptionAsync(Exception exception)
        {

            await OnErrorAsync(exception);

            //if (OnErrorHandleAsync is null && IsShowDetail)
            {

                StateHasChanged();
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, ChildContent);
        }
    }
}
