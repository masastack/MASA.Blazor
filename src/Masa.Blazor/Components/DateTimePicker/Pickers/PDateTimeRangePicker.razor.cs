namespace Masa.Blazor.Presets;

public partial class PDateTimeRangePicker : PDateTimeRangePickerView
{
    [Parameter] public RenderFragment<ActivatorProps>? ActivatorContent { get; set; }

    private PDateTimeRangePickerView _pickerView = null!;

    private DateTime? _start;
    private DateTime? _end;

    private PDefaultDateTimePickerActivator? _defaultActivator;
    private string? _display;
    private bool _menu;

    private RenderFragment<ActivatorProps> ComputedActivatorContent => ActivatorContent ?? DefaultActivator;

    private string Class => "m-date-time-picker";

    protected override void OnInitialized()
    {
        base.OnInitialized();

        FormatDisplay();
    }

    private void OnMenuChanged(bool val)
    {
        _menu = val;

        if (_menu)
        {
            _start = Start;
            _end = End;
        }
        else
        {
            _pickerView.ResetPickerView(DateOnly.FromDateTime(End ?? DateTime.Now));
        }
    }

    private async Task OnOk()
    {
        await UpdateValue();
        _menu = false;
    }

    private async Task DisplayChanged(string? val)
    {
        if (string.IsNullOrEmpty(val))
        {
            _start = null;
            _end = null;
            await UpdateValue();
        }
        else if (val.Contains("~"))
        {
            var arr = val.Split("~");
            if (DateTime.TryParse(arr[0].Trim(), out var startVal) &&
                DateTime.TryParse(arr[1].Trim(), out var endVal) && startVal <= endVal)
            {
                _start = startVal;
                _end = endVal;
                await UpdateValue();
            }
        }
    }

    private async Task UpdateValue()
    {
        await StartChanged.InvokeAsync(_start);
        await EndChanged.InvokeAsync(_end);
        FormatDisplay();
    }

    private void FormatDisplay()
    {
        if (Start.HasValue && End.HasValue)
        {
            _display = $"{Start?.ToString(_defaultActivator?.Format)} ~ {End?.ToString(_defaultActivator?.Format)}";
        }
        else
        {
            _display = string.Empty;
        }
    }
}