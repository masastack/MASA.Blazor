namespace Masa.Blazor.Presets;

public class PDateDigitalClockPickerViewBase<TValue> : PDateTimePickerViewBase<TValue>
{
    [Parameter] public OneOf<Func<TimeOnly, bool>, List<TimeOnly>> AllowedTimes { get; set; }

    [Parameter] public bool MultiSection { get; set; }

    [Parameter] [ApiDefaultValue("TimeSpan.FromMinutes(30)")]
    public TimeSpan Step { get; set; } = TimeSpan.FromMinutes(30);

    [Parameter] [ApiDefaultValue(1)]
    public int HourStep { get; set; } = 1;

    [Parameter] [ApiDefaultValue(1)]
    public int MinuteStep { get; set; } = 1;

    [Parameter] [ApiDefaultValue(1)]
    public int SecondStep { get; set; } = 1;
}
