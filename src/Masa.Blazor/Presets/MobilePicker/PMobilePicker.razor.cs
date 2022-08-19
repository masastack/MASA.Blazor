namespace Masa.Blazor.Presets;

public partial class PMobilePicker<TColumn, TColumnItem, TColumnItemValue>
{
    [Inject]
    private I18n I18n { get; set; } = null!;

    [Parameter]
    public RenderFragment<ActivatorProps> ActivatorContent { get; set; }

    [Parameter] public string Title { get; set; }

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

    private bool _visible;

    private List<TColumnItemValue> InternalValue { get; set; } = new();

    private Task OnCancel() => HandleVisibleChanged(false);

    private async Task OnConfirm()
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(InternalValue);
        }

        await OnCancel();
    }

    private async Task HandleVisibleChanged(bool val)
    {
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
        if (visible)
        {
            InternalValue = Value ?? new();
        }
    }

    private void HandleValueChanged(List<TColumnItemValue> val)
    {
        InternalValue = val;
    }
}
