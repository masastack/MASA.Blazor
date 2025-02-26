namespace Masa.Blazor.Presets.Cron;

public partial class MonthCronItem : SecondCronItem
{
    public MonthCronItem()
    {
        Period = PeriodTypes.Month;
    }
}