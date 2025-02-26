namespace Masa.Blazor.Presets.Cron;

public partial class MinuteCronItem : SecondCronItem
{
    public MinuteCronItem()
    {
        Period = PeriodTypes.Minute;
    }
}