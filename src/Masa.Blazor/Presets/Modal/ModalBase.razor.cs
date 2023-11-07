namespace Masa.Blazor.Presets;

public partial class ModalBase
{
    [Inject]
    public I18n I18n { get; set; } = null!;

    [Inject]
    protected IJSRuntime JsRuntime { get; set; } = null!;

    [Parameter]
    public RenderFragment<ActivatorProps>? ActivatorContent { get; set; }

    [Parameter]
    public string? ActionsClass { get; set; }

    [Parameter]
    public string? ActionsStyle { get; set; }

    /// <summary>
    /// Automatically scroll to the top when <see cref="Value"/> is true.
    /// </summary>
    [Parameter]
    public bool AutoScrollToTop { get; set; }

    [Parameter]
    public string? BodyClass { get; set; }

    [Parameter]
    public string? BodyStyle { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? ContentClass { get; set; }

    [Parameter]
    public string? ContentStyle { get; set; }

    [Parameter, MassApiParameter(100)]
    public int DebounceInterval { get; set; } = 100;

    [Parameter]
    public StringNumber? Elevation { get; set; }

    [Parameter]
    public object? FormModel { get; set; }

    [Parameter]
    public string? HeaderClass { get; set; }

    [Parameter]
    public string? HeaderStyle { get; set; }

    [Parameter]
    public StringNumber? Height { get; set; }

    [Parameter]
    public bool HideCancelAction { get; set; }

    [Parameter]
    public bool HideTitleDivider { get; set; }

    [Parameter]
    public bool HideActionsDivider { get; set; }

    [Parameter]
    public StringNumber? MaxWidth { get; set; }

    [Parameter]
    public bool Persistent { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Parameter, MassApiParameter("dialog-transition")]
    public string? Transition { get; set; } = "dialog-transition";

    [Parameter]
    public bool Value { get; set; }

    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

    [Parameter]
    public StringNumber? Width { get; set; }

    #region save,cancel,delete

    [Parameter]
    public Action<ModalButtonProps>? SaveProps { get; set; }

    [Parameter]
    public Action<ModalButtonProps>? CancelProps { get; set; }

    [Parameter]
    public Action<ModalButtonProps>? DeleteProps { get; set; }

    [Parameter]
    public string? SaveText { get; set; }

    [Parameter]
    public string? CancelText { get; set; }

    [Parameter]
    public string? DeleteText { get; set; }

    [Parameter]
    public EventCallback<ModalActionEventArgs> OnSave { get; set; }

    [Parameter]
    public EventCallback<ModalActionEventArgs> OnCancel { get; set; }

    [Parameter]
    public EventCallback<ModalActionEventArgs> OnDelete { get; set; }

    [Parameter]
    public RenderFragment<(Func<MouseEventArgs, Task> Click, bool Loading)>? SaveContent { get; set; }

    [Parameter]
    public RenderFragment<(Func<MouseEventArgs, Task> Click, bool Loading)>? DeleteContent { get; set; }

    [Parameter]
    public RenderFragment<(Func<MouseEventArgs, Task> Click, bool Loading)>? CancelContent { get; set; }

    [Parameter]
    public RenderFragment? TitleContent { get; set; }

    [Parameter]
    public RenderFragment<Func<MouseEventArgs, Task>>? CloseContent { get; set; }

    #endregion

    private bool _saveLoading;
    private Func<MouseEventArgs, Task>? _debounceHandleOnSave;

    private MCardText? BodyRef { get; set; }

    private bool Loading => _saveLoading; // may add the _deleteLoading in the future

    protected bool HasActions => OnDelete.HasDelegate || OnSave.HasDelegate;

    protected MForm? Form { get; set; }

    protected MDialog? Dialog { get; set; }

    protected ModalButtonProps? ComputedSaveButtonProps { get; set; }

    protected ModalButtonProps? ComputedCancelButtonProps { get; set; }

    protected ModalButtonProps? ComputedDeleteButtonProps { get; set; }

    protected virtual string? StaticClass { get; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        AutoScrollToTop = true;

        SaveText = I18n.T("$masaBlazor.save");
        CancelText = I18n.T("$masaBlazor.cancel");
        DeleteText = I18n.T("$masaBlazor.delete");

        await base.SetParametersAsync(parameters);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ComputedSaveButtonProps = GetDefaultSaveButtonProps();
        ComputedCancelButtonProps = GetDefaultCancelButtonProps();
        ComputedDeleteButtonProps = GetDefaultDeleteButtonProps();

        SaveProps?.Invoke(ComputedSaveButtonProps);
        CancelProps?.Invoke(ComputedCancelButtonProps);
        DeleteProps?.Invoke(ComputedDeleteButtonProps);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _debounceHandleOnSave = DebounceEvent<MouseEventArgs>(
            async (_) =>
            {
                var args = new ModalActionEventArgs(Form?.FormContext);

                _saveLoading = true;

                try
                {
                    await OnSave.InvokeAsync(args);

                    if (args.IsCanceled) return;

                    Form?.Reset();
                }
                finally
                {
                    _saveLoading = false;
                }
            },
            TimeSpan.FromMilliseconds(DebounceInterval));
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            Dialog!.AfterShowContent = async _ => { await ScrollToTop(); };
        }
    }

    protected virtual async Task HandleOnSave(MouseEventArgs args)
    {
        if (Form != null)
        {
            if (Form.Validate())
            {
                await _debounceHandleOnSave!(args);
            }
        }
        else
        {
            await _debounceHandleOnSave!(args);
        }
    }

    protected virtual async Task HandleOnCancel(MouseEventArgs _)
    {
        Form?.Reset();

        if (OnCancel.HasDelegate)
        {
            var args = new ModalActionEventArgs();

            await OnCancel.InvokeAsync(args);
        }
        else
        {
            await InternalValueChanged(false);
        }
    }

    protected virtual async Task HandleOnDelete(MouseEventArgs _)
    {
        if (OnDelete.HasDelegate)
        {
            var args = new ModalActionEventArgs();

            await OnDelete.InvokeAsync(args);
        }
    }

    private async Task InternalValueChanged(bool value)
    {
        Value = value;

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(value);
        }
    }

    protected virtual ModalButtonProps GetDefaultSaveButtonProps() => new()
    {
        Color = "primary",
        Text = true,
    };

    protected virtual ModalButtonProps GetDefaultCancelButtonProps() => new()
    {
        Text = true
    };

    protected virtual ModalButtonProps GetDefaultDeleteButtonProps() => new()
    {
        Color = "error",
        Text = true,
    };

    private async Task ScrollToTop()
    {
        if (AutoScrollToTop && BodyRef?.Ref != null)
        {
            await JsRuntime.ScrollTo(BodyRef.Ref, 0);
        }
    }

    #region form

    private Func<T, Task> DebounceEvent<T>(Func<T, Task> action, TimeSpan interval)
    {
        return Debounce<T>(async arg =>
        {
            await InvokeAsync(async () =>
            {
                try
                {
                    await action(arg);
                }
                finally
                {
                    StateHasChanged();
                }
            });
        }, interval);
    }

    private Func<T, Task> Debounce<T>(Func<T, Task> action, TimeSpan interval)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        var last = 0;
        return async arg =>
        {
            var current = Interlocked.Increment(ref last);

            await Task.Delay(interval);

            if (current == last) await action(arg);
        };
    }

    #endregion
}
