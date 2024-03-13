using Masa.Blazor.Presets.Cron.Models;

namespace Masa.Blazor.Presets.Cron;

public partial class CronItemBase : ComponentBase
{
    public CronItemBase(PeriodTypes period)
    {
        _period = period;
    }

    [Inject] protected I18n I18n { get; set; } = null!;

    [Parameter] public string? Value { get; set; }

    [Parameter] public EventCallback<string?> ValueChanged { get; set; }

    [Parameter] public EventCallback<CronItemModel> CronValueHasChanged { get; set; }

    private readonly PeriodTypes _period;

    protected CronTypes SelectedCronType;

    protected int? MinUnit;

    protected int? MaxUnit;

    protected CronItemDataModel? CronItemData;

    protected List<int> AllPeriodList = new();

    protected string I18NUnitText = string.Empty;

    protected List<WeekNumbers> WeekNumbers = new();

    protected List<DayOfWeek> DayOfWeeks = new();

    private string? _prevValue;

    protected override void OnInitialized()
    {
        if (_period != PeriodTypes.Week && _period != PeriodTypes.Year)
        {
            I18NUnitText = I18n.T($"$masaBlazor.period.{_period}");
        }

        switch (_period)
        {
            case PeriodTypes.Second:
            case PeriodTypes.Minute:
                MinUnit = 0;
                MaxUnit = 59;
                AllPeriodList = Enumerable.Range(MinUnit.Value, MaxUnit.Value + 1).ToList();
                break;
            case PeriodTypes.Hour:
                MinUnit = 0;
                MaxUnit = 23;
                AllPeriodList = Enumerable.Range(MinUnit.Value, MaxUnit.Value + 1).ToList();
                break;
            case PeriodTypes.Day:
                MinUnit = 1;
                MaxUnit = 31;
                AllPeriodList = Enumerable.Range(MinUnit.Value, MaxUnit.Value).ToList();
                break;
            case PeriodTypes.Month:
                MinUnit = 1;
                MaxUnit = 12;
                AllPeriodList = Enumerable.Range(MinUnit.Value, MaxUnit.Value).ToList();
                break;
            case PeriodTypes.Week:
                MinUnit = 1;
                MaxUnit = 7;
                AllPeriodList = Enumerable.Range(MinUnit.Value, MaxUnit.Value).ToList();
                break;
            case PeriodTypes.Year:
                MinUnit = DateTime.Now.Year;
                break;
        }

        if (_period == PeriodTypes.Week)
        {
            WeekNumbers = Enum.GetValues<WeekNumbers>().ToList();
            DayOfWeeks = Enum.GetValues<DayOfWeek>().ToList();
        }

        CronItemData = new()
        {
            PeriodStart = MinUnit,
            PeriodEnd = MinUnit + 1,
            StartFromPeriod = MinUnit,
            StartEveryPeriod = 1,
            SpecifyPeriods = new List<int>(),
            NearestOfDay = MinUnit,
            LastPeriodOfWeek = MinUnit,
            SelectWeekNumber = Presets.WeekNumbers.First
        };
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (_prevValue != Value)
        {
            _prevValue = Value;

            InitValue();
        }
    }

    protected async Task OnSelectedCronTypeChanged(CronTypes type)
    {
        if (SelectedCronType != type)
        {
            SelectedCronType = type;

            await CalculateCronValue();
        }
    }

    protected async Task OnPeriodStartChanged(int? periodStart)
    {
        if (CronItemData!.PeriodStart != periodStart)
        {
            CronItemData.PeriodStart = periodStart;

            await CalculateCronValue();
        }
    }

    protected async Task OnPeriodEndChanged(int? periodEnd)
    {
        if (CronItemData!.PeriodEnd != periodEnd)
        {
            CronItemData.PeriodEnd = periodEnd;
            await CalculateCronValue();
        }
    }

    protected async Task OnNearestOfDayChanged(int? nearestOfDay)
    {
        if (CronItemData!.NearestOfDay != nearestOfDay)
        {
            CronItemData.NearestOfDay = nearestOfDay;
            await CalculateCronValue();
        }
    }

    protected async Task OnLastPeriodOfWeekChanged(int? lastPeriodOfWeek)
    {
        lastPeriodOfWeek++;

        if (CronItemData!.LastPeriodOfWeek != lastPeriodOfWeek)
        {
            CronItemData.LastPeriodOfWeek = lastPeriodOfWeek;
            await CalculateCronValue();
        }
    }

    protected async Task OnStartFromPeriodChange(int? startFromPeriod)
    {
        if (CronItemData!.StartFromPeriod != startFromPeriod)
        {
            CronItemData.StartFromPeriod = startFromPeriod;
            await CalculateCronValue();
        }
    }

    protected async Task OnStartEveryPeriod(int startEveryPeriod)
    {
        if (CronItemData!.StartEveryPeriod != startEveryPeriod)
        {
            CronItemData.StartEveryPeriod = startEveryPeriod;
            await CalculateCronValue();
        }
    }

    protected async Task OnSpecifyPeriodChanged(List<int> specifyPeriods)
    {
        if (CronItemData!.SpecifyPeriods != specifyPeriods)
        {
            CronItemData.SpecifyPeriods = specifyPeriods;
            await CalculateCronValue();
        }
    }

    protected async Task OnWeekNumberChanged(WeekNumbers weekNumber)
    {
        if (CronItemData!.SelectWeekNumber != weekNumber)
        {
            CronItemData.SelectWeekNumber = weekNumber;
            await CalculateCronValue();
        }
    }

    protected async Task OnDayOfWeekChanged(DayOfWeek dayOfWeek)
    {
        if (CronItemData!.SelectDayOfWeek != dayOfWeek)
        {
            CronItemData.SelectDayOfWeek = dayOfWeek;
            await CalculateCronValue();
        }
    }

    private async Task CalculateCronValue()
    {
        if (CronItemData is null)
        {
            return;
        }
        
        string val;
        switch (SelectedCronType)
        {
            case CronTypes.Range:
                val = CronItemData.PeriodStart + "-" + CronItemData.PeriodEnd;
                break;
            case CronTypes.StartFrom:
                val = CronItemData.StartFromPeriod + "/" + CronItemData.StartEveryPeriod;
                break;
            case CronTypes.Specify:
                if (CronItemData.SpecifyPeriods.Any())
                {
                    val = string.Join(",", CronItemData.SpecifyPeriods);
                }
                else
                {
                    val = "*";
                }

                break;
            case CronTypes.NotSpecify:
                val = "?";
                break;
            case CronTypes.WeekStartFrom:
                val = (1 + (int)CronItemData.SelectDayOfWeek)  + "#" + CronItemData.SelectWeekNumber.ToString("d");
                break;
            case CronTypes.LastOfPeriod:
                if (_period == PeriodTypes.Day)
                {
                    val = "L";
                }
                else
                {
                    val = CronItemData.LastPeriodOfWeek + "L";
                }

                break;
            case CronTypes.NearestDay:
                val = CronItemData.NearestOfDay + "W";
                break;
            default:
                val = "*";
                break;
        }

        if (ValueChanged.HasDelegate)
        {
            _prevValue = val;
            await ValueChanged.InvokeAsync(val);
        }
        else
        {
            Value = val;
        }

        if (CronValueHasChanged.HasDelegate)
        {
            await CronValueHasChanged.InvokeAsync(new CronItemModel() { Period = _period, CronValue = val });
        }
    }

    protected string GetWeekNumberI18nString(WeekNumbers weekNumber)
    {
        return I18n.T($"$masaBlazor.weekNumber.{weekNumber}");
    }

    protected string GetDayOfWeekI18nString(DayOfWeek dayOfWeek)
    {
        return I18n.T($"$masaBlazor.dayOfWeek.{dayOfWeek}");
    }

    private void InitValue()
    {
        if (Value is null || CronItemData is null)
        {
            return;
        }
        
        if (Value == "*")
        {
            SelectedCronType = CronTypes.Period;
        }
        else if (Value.Contains('-'))
        {
            SelectedCronType = CronTypes.Range;
            var splitArr = Value.Split('-');
            if (splitArr.Length == 2)
            {
                CronItemData.PeriodStart = int.Parse(splitArr[0]);
                CronItemData.PeriodEnd = int.Parse(splitArr[1]);
            }
        }
        else if (Value.Contains('/'))
        {
            SelectedCronType = CronTypes.StartFrom;
            var splitArr = Value.Split('/');
            if (splitArr.Length == 2)
            {
                CronItemData.StartFromPeriod = int.Parse(splitArr[0]);
                CronItemData.StartEveryPeriod = int.Parse(splitArr[1]);
            }
        }
        else if (Value == "?")
        {
            SelectedCronType = CronTypes.NotSpecify;
        }
        else if (Value.Contains('#'))
        {
            SelectedCronType = CronTypes.WeekStartFrom;
            var splitArr = Value.Split("#");
            if (splitArr.Length == 2)
            {
                CronItemData.SelectDayOfWeek = Enum.Parse<DayOfWeek>(splitArr[0]);
                CronItemData.SelectWeekNumber = Enum.Parse<WeekNumbers>(splitArr[1]);
            }
        }
        else if (Value.Contains('L'))
        {
            SelectedCronType = CronTypes.LastOfPeriod;
            if (_period == PeriodTypes.Week)
            {
                CronItemData.LastPeriodOfWeek = int.Parse(Value[0].ToString());
            }
        }
        else if (Value.Contains('W'))
        {
            SelectedCronType = CronTypes.NearestDay;
            CronItemData.NearestOfDay = int.Parse(Value[0].ToString());
        }
        else if (string.IsNullOrWhiteSpace(Value))
        {
            SelectedCronType = 0;
        }
        else
        {
            SelectedCronType = CronTypes.Specify;
            CronItemData.SpecifyPeriods = Value.Split(',').Select(p => int.Parse(p)).ToList();
        }
    }
}
