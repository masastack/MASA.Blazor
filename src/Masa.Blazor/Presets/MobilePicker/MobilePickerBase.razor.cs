namespace Masa.Blazor.Presets.MobilePicker;

public partial class MobilePickerBase<TColumn, TColumnItem, TColumnItemValue, TValue>
{
    [Inject]
    private I18n I18n { get; set; } = null!;

    [Inject]
    private IJSRuntime Js { get; set; } = null!;

    [Parameter]
    public RenderFragment<ActivatorProps>? ActivatorContent { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible == value) return;

            OnVisibleChanged(value);
            _visible = value;
        }
    }

    [Parameter]
    public EventCallback<bool> VisibleChanged { get; set; }

    [Parameter]
    public TValue? Value { get; set; }

    [Parameter]
    public EventCallback<TValue?> ValueChanged { get; set; }

    #region paramters from MobilePickerView

    public virtual List<TColumn> Columns { get; set; } = null!;

    public virtual Func<TColumnItem, string> ItemText { get; set; } = null!;

    public virtual Func<TColumnItem, TColumnItemValue> ItemValue { get; set; } = null!;

    public virtual Func<TColumnItem, List<TColumnItem>>? ItemChildren { get; set; }

    public virtual Func<TColumnItem, bool>? ItemDisabled { get; set; }

    // TODO: change to StringNumber, support px, vh, vw, rem
    [Parameter]
    public int ItemHeight { get; set; } = 44;

    [Parameter]
    public int SwipeDuration { get; set; } = 1000;

    [Parameter]
    public int VisibleItemCount { get; set; } = 6;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; } = new();

    #endregion

    private bool _visible;
    private Func<bool, Task>? _internalValueChanged;

    private List<TColumnItemValue> InternalValue { get; set; } = new();

    protected virtual string? ClassPrefix { get; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _internalValueChanged = ValueChanged.HasDelegate ? HandleVisibleChanged : default;
    }

    private Task OnCancel() => HandleVisibleChanged(false);

    protected virtual bool TryConvertInternalValueToValue(List<TColumnItemValue> internalValue, out TValue? value)
    {
        value = default;

        if (internalValue is not TValue val) return false;

        value = val;
        return true;
    }

    protected virtual bool TryConvertValueToInternalValue(TValue? value, out List<TColumnItemValue> internalValue)
    {
        internalValue = new();

        if (value is not List<TColumnItemValue> val) return false;

        internalValue = val;
        return true;
    }

    private async Task HandleOnConfirm()
    {
        if (TryConvertInternalValueToValue(InternalValue, out var value))
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }
        }

        await OnCancel();
    }

    private async Task HandleVisibleChanged(bool val)
    {
        if (val)
        {
            await Js.InvokeVoidAsync(JsInteropConstants.AddCls, "html", "overflow-y-hidden");
        }
        else
        {
            await Js.InvokeVoidAsync(JsInteropConstants.RemoveCls, "html", "overflow-y-hidden");
        }

        if (VisibleChanged.HasDelegate)
        {
            await VisibleChanged.InvokeAsync(val);
        }
        else
        {
            Visible = val;
        }
    }

    private void OnVisibleChanged(bool visible)
    {
        if (!visible) return;

        if (TryConvertValueToInternalValue(Value, out var internalValue))
        {
            InternalValue = internalValue;
        }
    }

    protected virtual void HandleValueChanged(List<TColumnItemValue> val)
    {
        InternalValue = val;
    }
}
