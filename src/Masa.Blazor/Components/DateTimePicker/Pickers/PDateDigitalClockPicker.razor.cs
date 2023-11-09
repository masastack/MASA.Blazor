namespace Masa.Blazor.Presets;

public partial class PDateDigitalClockPicker<TValue>: PDateTimePickerBase<TValue>
{
    [Parameter] public OneOf<Func<TimeOnly, bool>, List<TimeOnly>> AllowedTimes { get; set; }

    [Parameter] public bool MultiSection { get; set; }

    [Parameter] [MassApiParameter("TimeSpan.FromMinutes(30)")]
    public TimeSpan Step { get; set; } = TimeSpan.FromMinutes(30);

    [Parameter] [MassApiParameter(1)]
    public int HourStep { get; set; } = 1;

    [Parameter] [MassApiParameter(1)]
    public int MinuteStep { get; set; } = 1;

    [Parameter] [MassApiParameter(1)]
    public int SecondStep { get; set; } = 1;
}
