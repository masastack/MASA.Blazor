using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;

namespace Masa.Blazor
{
    public class MErrorHandler : ErrorBoundaryBase, IErrorHandler
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

        private bool _showRender = true;
        private bool _isThrown;

        protected override bool ShouldRender()
        {
            return _showRender && base.ShouldRender();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (Exception != null)
            {
                Exception = null;
                Recover();
            }
            _showRender = false;
        }

        private bool CheckIfThrownInLifecycles(Exception exception)
        {
            if (exception.TargetSite?.Name is nameof(SetParametersAsync)
                or nameof(OnInitialized) or nameof(OnInitializedAsync)
                or nameof(OnParametersSet) or nameof(SetParametersAsync)
                or nameof(OnAfterRender) or nameof(OnAfterRenderAsync))
            {
                return true;
            }

            return exception.InnerException is not null && CheckIfThrownInLifecycles(exception.InnerException);
        }

        protected override async Task OnErrorAsync(Exception exception)
        {
            if (exception.InnerException != null) exception = exception.InnerException;
            _exception = exception;
            var ttt = CheckIfThrownInLifecycles(exception);
            //if (!ttt)
            //{
            //    //_showRender = false;
            //}
            //else
            //{
            //    _isThrown=true;
            //    StateHasChanged();
            //}
            Logger?.LogError(exception, "OnErrorAsync");
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

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (!_isThrown && (CurrentException is null || CurrentException is not null && _showRender))
            {
                builder.AddContent(0, ChildContent);
            }
            else if (_isThrown || CurrentException is not null)
            {
                if (!ShowAlert && OnErrorHandleAsync is null)
                {
                    if (ErrorContent != null)
                    {
                        builder.AddContent(1, ErrorContent!(CurrentException));
                        return;
                    }
                    else
                    {
                        builder.OpenElement(0, "div");
                        builder.AddAttribute(0, "class", "blazor-error-boundary");
                        builder.CloseElement();
                    }
                }
                else
                {
                    builder.OpenElement(0, "div");
                    builder.AddContent(0, CurrentException.Message);
                    //builder.AddAttribute(0, "class", "blazor-error-boundary");
                    builder.CloseElement();
                }
            }
        }

        public async Task HandleExceptionAsync(Exception exception)
        {
            await OnErrorAsync(exception);
            //_showRender = true;

            if (!ShowAlert && OnErrorHandleAsync is null)
            {
                Exception = exception;
                StateHasChanged();
            }
        }
    }
}