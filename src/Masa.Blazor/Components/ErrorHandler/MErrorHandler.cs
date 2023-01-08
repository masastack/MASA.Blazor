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
        public Func<Exception, Task<bool>> OnErrorHandleAsync { get; set; }

        [Parameter]
        public bool ShowAlert { get; set; } = true;

        [Parameter]
        public bool ShowDetail { get; set; }

        private bool _thrownInLifecycles;

        protected override void OnParametersSet()
        {
            if (CurrentException is not null)
            {
                _thrownInLifecycles = false;
                Recover();
            }
        }

        private bool CheckIfThrownInLifecycles(Exception exception)
        {
            if (exception.TargetSite?.Name is nameof(SetParametersAsync)
                    or nameof(OnInitialized) or nameof(OnInitializedAsync)
                    or nameof(OnParametersSet) or nameof(OnParametersSetAsync)
                    or nameof(OnAfterRender) or nameof(OnAfterRenderAsync)
                || (exception.StackTrace is not null
                    && (exception.StackTrace.Contains("RunInitAndSetParametersAsync()")
                        || exception.StackTrace.Contains("SupplyCombinedParameters(ParameterView directAndCascadingParameters)")
                        || exception.StackTrace.Contains("OnAfterRenderAsync(Boolean firstRender)")
                        || exception.StackTrace.Contains("OnAfterRender(Boolean firstRender)"))))
            {
                return true;
            }

            if (exception.InnerException is not null)
            {
                return CheckIfThrownInLifecycles(exception.InnerException);
            }

            return false;
        }

        protected override async Task OnErrorAsync(Exception exception)
        {
            Logger?.LogError(exception, "OnErrorAsync");

            if (CheckIfThrownInLifecycles(exception))
            {
                _thrownInLifecycles = true;
            }

            var _handled = false;
            if (OnErrorHandleAsync != null)
            {
                _handled = await OnErrorHandleAsync(exception);
                StateHasChanged();
            }

            if (!_handled)
            {
                if (ShowAlert)
                {
                    await PopupService.ToastAsync(alert =>
                    {
                        alert.Type = AlertTypes.Error;
                        alert.Title = "Something wrong!";
                        alert.Content = ShowDetail ? $"{exception.Message}:{exception.StackTrace}" : exception.Message;
                    });
                }
                else
                {
                    throw exception;
                }
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<IErrorHandler>>(0);
            builder.AddAttribute(1, nameof(CascadingValue<IErrorHandler>.Value), this);
            builder.AddAttribute(2, nameof(CascadingValue<IErrorHandler>.IsFixed), true);

            var content = ChildContent;
            var showChildContent = true;
            if (CurrentException is not null)
            {
                if (ErrorContent is not null)
                {
                    content = ErrorContent.Invoke(CurrentException);
                }
                else if (_thrownInLifecycles || (OnErrorHandleAsync == null && !ShowAlert))
                {
                    showChildContent = false;
                    content = cb =>
                    {
                        cb.OpenElement(0, "div");
                        cb.AddAttribute(1, "class", "blazor-error-boundary");
                        cb.CloseElement();
                    };
                }
            }

            if (showChildContent)
                builder.AddAttribute(3, nameof(CascadingValue<IErrorHandler>.ChildContent), content);

            builder.CloseComponent();
        }

        public async Task HandleExceptionAsync(Exception exception)
        {
            await OnErrorAsync(exception);
        }
    }
}
