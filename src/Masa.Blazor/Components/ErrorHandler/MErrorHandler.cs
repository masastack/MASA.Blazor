using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;

namespace Masa.Blazor
{
    public class MErrorHandler : ErrorBoundaryBase
    {
        [Inject]
        protected ILogger<MErrorHandler> Logger { get; set; }

        [Inject]
        public IPopupService PopupService { get; set; }

        [Parameter]
        public Func<Exception, Task> OnErrorHandleAsync { get; set; }

        [Parameter]
        public bool ShowAlert { get; set; } = true;

        [Parameter]
        public bool ShowDetail { get; set; }

        private Exception _exception;
        public Exception Exception
        {
            get { return _exception ?? CurrentException; }
            set { _exception = value; }
        }

        private bool _isFirstRender = true;
        private bool _isShouldRender = true;

        protected override bool ShouldRender()
        {
            return _isShouldRender && base.ShouldRender();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            _isFirstRender = firstRender;
        }        

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (!_isFirstRender)
            {
                Exception = null;
                _isFirstRender = true;
                _isShouldRender = true;
                Recover();
            }
        }

        protected override async Task OnErrorAsync(Exception exception)
        {
            Logger.LogError(exception, "OnErrorAsync");
            Exception = exception;
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
                        alert.Content = ShowDetail ? $"{Exception.Message}:{Exception.StackTrace}" : Exception.Message;
                    });
                }
            }
            _isShouldRender = false;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Exception is null)
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