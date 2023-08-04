namespace Masa.Blazor.Presets;

public partial class PDateTimePicker<TValue>
{
    [Parameter]
    public RenderFragment<ActivatorProps>? ActivatorContent { get; set; }

    [Parameter]
    public bool Compact { get; set; }

    [Parameter]
    public bool Dialog { get; set; }

    [Parameter]
    public string? TabItemTransition { get; set; }

    private bool _menu;

    private TValue? _internalDateTime;

    private void OnMenuChanged(bool val)
    {
        _menu = val;

        if (_menu)
        {
            _internalDateTime = Value;
        }
    }

    private async Task OnConfirm()
    {
        _menu = false;
        await ValueChanged.InvokeAsync(_internalDateTime);
    }
}
