using Masa.Blazor.Presets.Cron.Models;

namespace Masa.Blazor.Presets;

public partial class CronItem
{
    [Inject]
    protected I18n I18n { get; set; } = null!;

    [Parameter]
    public PeriodTypes Period { get; set; }

    [Parameter]
    public string Value
    {
        get
        {
            return _value;
        }
        set
        {
            if (value != _value)
            {
                _value = value;

                InitValue();
            }
        }
    }

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter]
    public EventCallback<CronItemModel> CronValueHasChanged { get; set; }

    private CronTypes _selectedCronType;

    private int? _minUnit = null;

    private int? _maxUnit = null;

    private CronItemDataModel _cronItemData;

    private List<int> _allPeriodList = new();

    private string _value;

    private string _i18nUnitText = string.Empty;

    private bool _showNotSpecify;

    private List<WeekNumbers> _weekNumbers = new();

    private List<DayOfWeek> _dayOfWeeks = new();

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (Period != PeriodTypes.Week && Period != PeriodTypes.Year)
            {
                _i18nUnitText = I18n.T($"$masaBlazor.{nameof(Period)}.{Period}");
            }

            switch (Period)
            {
                case PeriodTypes.Second:
                case PeriodTypes.Minute:
                    _minUnit = 0;
                    _maxUnit = 59;
                    _allPeriodList = Enumerable.Range(_minUnit.Value, _maxUnit.Value + 1).ToList();
                    _showNotSpecify = false;
                    break;
                case PeriodTypes.Hour:
                    _minUnit = 0;
                    _maxUnit = 23;
                    _allPeriodList = Enumerable.Range(_minUnit.Value, _maxUnit.Value + 1).ToList();
                    _showNotSpecify = false;
                    break;
                case PeriodTypes.Day:
                    _minUnit = 1;
                    _maxUnit = 31;
                    _allPeriodList = Enumerable.Range(_minUnit.Value, _maxUnit.Value).ToList();
                    _showNotSpecify = true;
                    break;
                case PeriodTypes.Month:
                    _minUnit = 1;
                    _maxUnit = 12;
                    _allPeriodList = Enumerable.Range(_minUnit.Value, _maxUnit.Value).ToList();
                    _showNotSpecify = false;
                    break;
                case PeriodTypes.Week:
                    _minUnit = 1;
                    _maxUnit = 7;
                    _allPeriodList = Enumerable.Range(_minUnit.Value, _maxUnit.Value).ToList();
                    _showNotSpecify = true;
                    break;
                case PeriodTypes.Year:
                    _minUnit = 1;
                    break;
                default:
                    break;
            }

            if (Period == PeriodTypes.Week)
            {
                _weekNumbers = Enum.GetValues<WeekNumbers>().ToList();
                _dayOfWeeks = Enum.GetValues<DayOfWeek>().ToList();
            }
        }
        return base.OnAfterRenderAsync(firstRender);
    }

    private Task OnSelectedCronTypeChanged(CronTypes type)
    {
        if (_selectedCronType != type)
        {
            _selectedCronType = type;

            CalculateCronValue();
        }
        InitCronType();
        return Task.CompletedTask;
    }

    private Task OnPeriodStartChanged(int? periodStart)
    {
        if (_cronItemData.PeriodStart != periodStart)
        {
            _cronItemData.PeriodStart = periodStart;

            CalculateCronValue();
        }
        return Task.CompletedTask;
    }

    private Task OnPeriodEndChanged(int? periodEnd)
    {
        if (_cronItemData.PeriodEnd != periodEnd)
        {
            _cronItemData.PeriodEnd = periodEnd;
            CalculateCronValue();
        }
        return Task.CompletedTask;
    }

    private Task OnNearestOfDayChanged(int? nearestOfDay)
    {
        if (_cronItemData.NearestOfDay != nearestOfDay)
        {
            _cronItemData.NearestOfDay = nearestOfDay;
            CalculateCronValue();
        }
        return Task.CompletedTask;
    }

    private Task OnLastPeriodOfWeekChanged(int? lastPeriodOfWeek)
    {
        if (_cronItemData.LastPeriodOfWeek != lastPeriodOfWeek)
        {
            _cronItemData.LastPeriodOfWeek = lastPeriodOfWeek;
            CalculateCronValue();
        }
        return Task.CompletedTask;
    }

    private Task OnStartFromPeriodChange(int? startFromPeriod)
    {
        if (_cronItemData.StartFromPeriod != startFromPeriod)
        {
            _cronItemData.StartFromPeriod = startFromPeriod;
            CalculateCronValue();
        }
        return Task.CompletedTask;
    }

    private Task OnStartEveryPeriod(int startEveryPeriod)
    {
        if (_cronItemData.StartEveryPeriod != startEveryPeriod)
        {
            _cronItemData.StartEveryPeriod = startEveryPeriod;
            CalculateCronValue();
        }
        return Task.CompletedTask;
    }

    private Task OnSpecifyPeriodChanged(List<int> specifyPeriods)
    {
        if (_cronItemData.SpecifyPeriods != specifyPeriods)
        {
            _cronItemData.SpecifyPeriods = specifyPeriods;
            CalculateCronValue();
        }
        return Task.CompletedTask;
    }

    private Task OnWeekNumberChanged(WeekNumbers weekNumber)
    {
        if (_cronItemData.SelectWeekNumber != weekNumber)
        {
            _cronItemData.SelectWeekNumber = weekNumber;
            CalculateCronValue();
        }

        return Task.CompletedTask;
    }

    private Task OnDayOfWeekChanged(DayOfWeek dayOfWeek)
    {
        if (_cronItemData.SelectDayOfWeek != dayOfWeek)
        {
            _cronItemData.SelectDayOfWeek = dayOfWeek;
            CalculateCronValue();
        }
        return Task.CompletedTask;
    }

    private Task CalculateCronValue()
    {
        switch (_selectedCronType)
        {
            case CronTypes.Range:
                _value = _cronItemData.PeriodStart + "-" + _cronItemData.PeriodEnd;
                break;
            case CronTypes.StartFrom:
                _value = _cronItemData.StartFromPeriod + "/" + _cronItemData.StartEveryPeriod;
                break;
            case CronTypes.Specify:
                if (_cronItemData.SpecifyPeriods.Any())
                {
                    _value = string.Join(",", _cronItemData.SpecifyPeriods);
                }
                else
                {
                    _value = "*";
                }
                break;
            case CronTypes.NotSpecify:
                _value = "?";
                break;
            case CronTypes.WeekStartFrom:
                _value = _cronItemData.SelectDayOfWeek.ToString("d") + "#" + _cronItemData.SelectWeekNumber.ToString("d");
                break;
            case CronTypes.LastOfPeriod:
                if (Period == PeriodTypes.Day)
                {
                    _value = "L";
                }
                else
                {
                    _value = _cronItemData.LastPeriodOfWeek + "L";
                }
                break;
            case CronTypes.NearestDay:
                _value = _cronItemData.NearestOfDay + "W";
                break;
            default:
                _value = "*";
                break;
        }

        if (ValueChanged.HasDelegate)
        {
            ValueChanged.InvokeAsync(_value);
        }

        if (CronValueHasChanged.HasDelegate)
        {
            CronValueHasChanged.InvokeAsync(new CronItemModel() { Period = Period, CronValue = _value });
        }

        return Task.CompletedTask;
    }

    private string GetWeekNumberI18nString(WeekNumbers weekNumber)
    {
        return I18n.T($"$masaBlazor.weekNumber.{weekNumber}");
    }

    private string GetDayOfWeekI18nString(DayOfWeek dayOfWeek)
    {
        return I18n.T($"$masaBlazor.dayOfWeek.{dayOfWeek}");
    }

    private Task InitValue()
    {
        InitCronType();

        if (_value == "*")
        {
            _selectedCronType = CronTypes.Period;
        }
        else if (_value.Contains('-'))
        {
            _selectedCronType = CronTypes.Range;
            var splitArr = _value.Split('-');
            if (splitArr.Length == 2)
            {
                _cronItemData.PeriodStart = int.Parse(splitArr[0]);
                _cronItemData.PeriodEnd = int.Parse(splitArr[1]);
            }
        }
        else if (_value.Contains('/'))
        {
            _selectedCronType = CronTypes.StartFrom;
            var splitArr = _value.Split('/');
            if (splitArr.Length == 2)
            {
                _cronItemData.StartFromPeriod = int.Parse(splitArr[0]);
                _cronItemData.StartEveryPeriod = int.Parse(splitArr[1]);
            }
        }
        else if (_value == "?")
        {
            _selectedCronType = CronTypes.NotSpecify;
        }
        else if (_value.Contains('#'))
        {
            _selectedCronType = CronTypes.WeekStartFrom;
            var splitArr = _value.Split("#");
            if (splitArr.Length == 2)
            {
                _cronItemData.SelectDayOfWeek = Enum.Parse<DayOfWeek>(splitArr[0]);
                _cronItemData.SelectWeekNumber = Enum.Parse<WeekNumbers>(splitArr[1]);
            }
        }
        else if (_value.Contains('L'))
        {
            _selectedCronType = CronTypes.LastOfPeriod;
            if (Period == PeriodTypes.Week)
            {
                _cronItemData.LastPeriodOfWeek = int.Parse(_value[0].ToString());
            }
        }
        else if (_value.Contains('W'))
        {
            _selectedCronType = CronTypes.NearestDay;
            _cronItemData.NearestOfDay = int.Parse(_value[0].ToString());
        }
        else if (string.IsNullOrWhiteSpace(_value))
        {
            _selectedCronType = 0;
        }
        else
        {
            _selectedCronType = CronTypes.Specify;
            _cronItemData.SpecifyPeriods = _value.Split(',').Select(p => int.Parse(p)).ToList();
        }

        return Task.CompletedTask;
    }

    private void InitCronType()
    {
        CronItemDataModel cronItemData = new()
        {
            PeriodStart = _minUnit,
            PeriodEnd = 2,
            StartFromPeriod = _minUnit,
            StartEveryPeriod = 1,
            NearestOfDay = _minUnit,
            LastPeriodOfWeek = _minUnit,
            SelectWeekNumber = WeekNumbers.First
        };
        switch (_selectedCronType)
        {
            case CronTypes.Range:
                cronItemData.PeriodStart = _cronItemData.PeriodStart;
                cronItemData.PeriodEnd = _cronItemData.PeriodEnd;
                break;
            case CronTypes.StartFrom:
                cronItemData.StartFromPeriod = _cronItemData.StartFromPeriod;
                cronItemData.StartEveryPeriod = _cronItemData.StartEveryPeriod;
                break;
            case CronTypes.Specify:
                cronItemData.SpecifyPeriods = _cronItemData.SpecifyPeriods;
                break;
            case CronTypes.WeekStartFrom:
                cronItemData.SelectDayOfWeek = _cronItemData.SelectDayOfWeek;
                cronItemData.SelectWeekNumber = _cronItemData.SelectWeekNumber;
                break;
            case CronTypes.LastOfPeriod:
                cronItemData.LastPeriodOfWeek = _cronItemData.LastPeriodOfWeek;
                break;
            case CronTypes.NearestDay:
                cronItemData.NearestOfDay = _cronItemData.NearestOfDay;
                break;
            default:
                break;
        }
        _cronItemData = cronItemData;
    }
}
