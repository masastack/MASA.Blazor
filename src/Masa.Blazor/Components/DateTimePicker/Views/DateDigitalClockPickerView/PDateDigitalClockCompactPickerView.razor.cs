namespace Masa.Blazor.Presets;

public partial class PDateDigitalClockCompactPickerView<TValue> : PDateDigitalClockPickerViewBase<TValue>
{
    [Inject] private I18n I18n { get; set; } = null!;

    [Parameter] public string? Transition { get; set; }

    private const string DATE = "date";
    private const string TIME = "time";

    private StringNumber _tabValue = DATE;

    private MDatePicker<DateOnly?>? _datePicker;
    private MTimePicker? _timePicker;

    private TimePickerType _timeActivePicker = TimePickerType.Hour;

    private string? DateTitle
    {
        get
        {
            if (_datePicker is not null && InternalDate.HasValue)
            {
                return DateFormatters.MonthDay(_datePicker.CurrentLocale)(InternalDate.Value);
            }

            return null;
        }
    }

    private string? YearTitle
    {
        get
        {
            if (_datePicker is not null && InternalDate.HasValue)
            {
                return DateFormatters.Year(_datePicker.CurrentLocale)(InternalDate.Value);
            }

            return null;
        }
    }

    private string? SelectingTime
    {
        get
        {
            switch (_timeActivePicker)
            {
                case TimePickerType.Hour:
                    return I18n.T("$masaBlazor.period.hour");
                case TimePickerType.Minute:
                    return I18n.T("$masaBlazor.period.minute");
                case TimePickerType.Second:
                    return I18n.T("$masaBlazor.period.second");
                default:
                    return null;
            }
        }
    }

    private int SupportedMaxTimePickerType
    {
        get
        {
            if (UseSeconds)
            {
                return 3;
            }

            return 2;
        }
    }

    private void OnPrevClick()
    {
        _timeActivePicker = (TimePickerType)(((int)_timeActivePicker) - 1);
    }

    private void OnNextClick()
    {
        _timeActivePicker = (TimePickerType)(((int)_timeActivePicker) + 1);
    }
}
