namespace Masa.Blazor.Presets.Cron;

public partial class HourCronItem : SecondCronItem
{
    public HourCronItem()
    {
        Period = PeriodTypes.Hour;
    }
}