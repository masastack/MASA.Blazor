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

        private bool _thrownInLifecycles;

        private Exception _exception;

        public Exception Exception
        {
            get => _exception ?? CurrentException;
            set => _exception = value;
        }

        protected override void OnParametersSet()
        {
            if (_thrownInLifecycles)
            {
                return;
            }

            Recover();
        }

        public new void Recover()
        {
            _thrownInLifecycles = false;
            Exception = null;

            base.Recover();
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
            Logger.LogError(exception, "OnErrorAsync");

            if (CheckIfThrownInLifecycles(exception))
            {
                _thrownInLifecycles = true;
            }

            Exception = exception;

            if (OnErrorHandleAsync != null)
            {
                await OnErrorHandleAsync(exception);
            }
            else
            {
                if (ShowAlert)
                {
                    await PopupService.ToastAsync(alert =>
                    {
                        // alert.Top = true;
                        alert.Type = AlertTypes.Error;
                        alert.Title = "Something wrong!";
                        alert.Content = ShowDetail ? $"{Exception.Message}:{Exception.StackTrace}" : Exception.Message;
                    });
                }
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<IErrorHandler>>(0);
            builder.AddAttribute(1, nameof(CascadingValue<IErrorHandler>.Value), this);
            builder.AddAttribute(2, nameof(CascadingValue<IErrorHandler>.IsFixed), true);

            var content = ChildContent;

            if (_thrownInLifecycles || (OnErrorHandleAsync == null && !ShowAlert && CurrentException != null))
            {
                if (ErrorContent != null)
                {
                    content = ErrorContent.Invoke(CurrentException);
                }
                else
                {
                    content = cb =>
                    {
                        cb.OpenElement(0, "div");
                        cb.AddAttribute(1, "class", "blazor-error-boundary");
                        cb.CloseElement();
                    };
                }
            }

            builder.AddAttribute(3, nameof(CascadingValue<IErrorHandler>.ChildContent), content);
            builder.CloseComponent();
        }

        public async Task HandleExceptionAsync(Exception exception)
        {
            await OnErrorAsync(exception);
        }
    }
}
