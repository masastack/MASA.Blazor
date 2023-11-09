using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor
{
    public class MErrorHandler : ErrorBoundaryBase, IErrorHandler
    {
        [Inject]
        protected ILogger<MErrorHandler> Logger { get; set; } = null!;

        [Inject]
        public IPopupService PopupService { get; set; } = null!;

        /// <summary>
        /// The event that will be invoked when an exception is thrown.
        /// It's recommended to pop up a dialog to show the exception.
        /// If you set this, the default popup will be disabled.
        /// </summary>
        [Parameter]
        public EventCallback<Exception> OnHandle { get; set; }

        /// <summary>
        /// The event that will be invoked after <see cref="OnHandle"/>.
        /// </summary>
        [Parameter]
        public EventCallback<Exception> OnAfterHandle { get; set; }

        [Parameter]
        [MassApiParameter(ErrorPopupType.Snackbar)]
        public ErrorPopupType PopupType { get; set; } = ErrorPopupType.Snackbar;

        /// <summary>
        /// Determine whether to show the stack trace of exception.
        /// </summary>
        [Parameter]
        public bool ShowDetail { get; set; }

        /// <summary>
        /// Disable the default popup or custom <see cref="OnHandle"/> event when the error content is going to render.
        /// It's useful when you don't want to show the error content and popup at the same time.
        /// </summary>
        [Parameter]
        public bool DisablePopupIfErrorContentRender { get; set; }

        private bool _shouldRender = true;
        private bool _thrownInLifecycles;

        private bool ShouldRenderErrorContent
            => _thrownInLifecycles || (OnHandle.HasDelegate == false && PopupType == ErrorPopupType.None);

        protected new Exception? CurrentException { get; private set; }

        protected override void OnParametersSet()
        {
            if (CurrentException is null) return;

            _thrownInLifecycles = false;
            Recover();
        }

        public new void Recover()
        {
            if (CurrentException is not null)
            {
                CurrentException = null;
                StateHasChanged();
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
            Logger?.LogError(exception, "OnErrorAsync");

            CurrentException = exception;

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
                await OnAfterHandle.InvokeAsync(exception);
            }

            _shouldRender = true;

            StateHasChanged();
        }

        protected override bool ShouldRender() => _shouldRender;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<IErrorHandler>>(0);
            builder.AddAttribute(1, nameof(CascadingValue<IErrorHandler>.Value), this);
            builder.AddAttribute(2, nameof(CascadingValue<IErrorHandler>.IsFixed), true);

            var content = ChildContent;
            if (CurrentException is not null && ShouldRenderErrorContent)
            {
                if (ErrorContent is null)
                {
                    content = cb =>
                    {
                        cb.OpenElement(0, "div");
                        cb.AddAttribute(1, "class", "blazor-error-boundary");
                        cb.CloseElement();
                    };
                }
                else
                {
                    content = ErrorContent.Invoke(CurrentException);
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
