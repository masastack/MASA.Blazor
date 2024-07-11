using Masa.Blazor.Components.ErrorHandler;

namespace Masa.Blazor
{
    // TODO: refactor summary
    // 1. The OnHandle 
    //    - should rename to OnErrorAsync
    //    - change the type to Func<Exception, Task>?
    //    - No longer override the default popup behavior
    // 2. Remove PopupType and OnAfterHandle parameters
    // 3. Add a new parameter `DisableDefaultPopup` to disable the default popup behavior

    public class MErrorHandler : ErrorBoundaryBase, IErrorHandler
    {
        [Inject] protected ILogger<MErrorHandler> Logger { get; set; } = null!;

        [Inject] private IErrorBoundaryLogger ErrorBoundaryLogger { get; set; } = null!;

        [Inject] public IPopupService PopupService { get; set; } = null!;

        /// <summary>
        /// The event that will be invoked when an exception is thrown.
        /// It's recommended to pop up a dialog to show the exception.
        /// If you set this, the default popup will be disabled.
        /// </summary>
        // TODO: the type should be Func<Exception, Task>? and rename the parameter to OnErrorAsync
        [Parameter]
        public EventCallback<Exception> OnHandle { get; set; }

        /// <summary>
        /// The event that will be invoked after <see cref="OnHandle"/>.
        /// It's recommended to log the exception here.
        /// </summary>
        [Parameter]
        public EventCallback<Exception> OnAfterHandle { get; set; }

        [Parameter]
        [MasaApiParameter(ErrorPopupType.Snackbar)]
        public ErrorPopupType PopupType { get; set; } = ErrorPopupType.Snackbar;

        /// <summary>
        /// Determine whether to show the stack trace of exception.
        /// </summary>
        [Parameter]
        public bool ShowDetail { get; set; }

        /// <summary>
        /// Disable the default popup or custom <see cref="OnHandle"/> event
        /// when the error content is going to render.
        /// It's useful when you don't want to show the error content and popup at the same time.
        /// </summary>
        [Parameter]
        public bool DisablePopupIfErrorContentRender { get; set; }

        private static RenderFragment _defaultErrorContent = builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "blazor-error-boundary");
            builder.CloseElement();
        };

        private bool _shouldRender = true;
        private bool _thrownInLifecycles;
        private Exception? _scopedCurrentException;

        // A flag to determine whether the exception has been rendered.
        // If the exception has been rendered, we should not render it again.
        private bool _exceptionRendered;

        private bool ShouldRenderErrorContent
            => _thrownInLifecycles || (OnHandle.HasDelegate == false && PopupType == ErrorPopupType.None);

        protected override void OnParametersSet()
        {
            if (_scopedCurrentException is null || _exceptionRendered == false) return;

            _thrownInLifecycles = false;

            if (CurrentException is null)
            {
                _scopedCurrentException = null;
            }

            Recover();
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
                        || exception.StackTrace.Contains("OnAfterRender(Boolean firstRender)")
                        || exception.StackTrace.Contains("<OnAfterRenderAsync>")
                        || exception.StackTrace.Contains("<OnAfterRender>"))))
            {
                return true;
            }

            return exception.InnerException is not null && CheckIfThrownInLifecycles(exception.InnerException);
        }

        protected override async Task OnErrorAsync(Exception exception)
        {
            Logger.LogError(exception.Message, exception);
            await ErrorBoundaryLogger.LogErrorAsync(exception);

            _exceptionRendered = false;
            _scopedCurrentException = exception;

            if (CheckIfThrownInLifecycles(exception))
            {
                _thrownInLifecycles = true;
            }

            _shouldRender = false;

            if (!(DisablePopupIfErrorContentRender && ShouldRenderErrorContent))
            {
                if (OnHandle.HasDelegate)
                {
                    await OnHandle.InvokeAsync(exception);
                }
                else
                {
                    if (PopupType == ErrorPopupType.Snackbar)
                    {
                        await PopupService.EnqueueSnackbarAsync(exception, ShowDetail);
                    }
                }
            }

            if (OnAfterHandle.HasDelegate)
            {
                _ = OnAfterHandle.InvokeAsync(exception);
            }

            _shouldRender = true;
        }

        protected override bool ShouldRender() => _shouldRender;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (_scopedCurrentException is not null)
            {
                _exceptionRendered = true;
            }

            builder.OpenComponent<CascadingValue<IErrorHandler>>(0);
            builder.AddAttribute(1, nameof(CascadingValue<IErrorHandler>.Value), this);
            builder.AddAttribute(2, nameof(CascadingValue<IErrorHandler>.IsFixed), true);

            RenderFragment? content;
            if (_scopedCurrentException is not null && ShouldRenderErrorContent)
            {
                content = ErrorContent?.Invoke(_scopedCurrentException) ?? _defaultErrorContent;
            }
            else
            {
                content = ChildContent;
            }

            builder.AddAttribute(3, nameof(CascadingValue<IErrorHandler>.ChildContent), content);
            builder.CloseComponent();
        }

        public async Task HandleExceptionAsync(Exception exception)
        {
            await OnErrorAsync(exception);
            StateHasChanged();
        }
    }
}