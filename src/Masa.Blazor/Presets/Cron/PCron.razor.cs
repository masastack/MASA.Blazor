namespace Masa.Blazor.Presets;

public partial class PCron
{
    [Inject]
    I18n I18n { get; set; }

    [Parameter]
    public string Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;
                OnValueChanged();
            }
        }
    }

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }
    
    private string _value;

    private bool _hasError;

    private string _errorMessage = string.Empty;

    private StringNumber _selectedPeriod;

    private readonly List<CronItemModel> _cronItems = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await CalculateCronValueAsync();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    protected override Task OnInitializedAsync()
    {
        InitCronItems();
        return base.OnInitializedAsync();
    }

    private void InitCronItems()
    {
        if (_cronItems.Count == 0)
        {
            foreach (var item in Enum.GetValues<PeriodTypes>())
            {
                var cronDefaultValue = "*";

                if (item == PeriodTypes.Week)
                {
                    cronDefaultValue = "?";
                }
                else if (item == PeriodTypes.Year)
                {
                    cronDefaultValue = string.Empty;
                }

                _cronItems.Add(new CronItemModel()
                {
                    Period = item,
                    CronValue = cronDefaultValue,
                    DefaultValue = cronDefaultValue
                });
            }
        }
    }

    protected override Task OnParametersSetAsync()
    {
        OnValueChanged();
        return base.OnParametersSetAsync();
    }

    private void OnValueChanged()
    {
        InitCronItems();
        if (CronExpression.IsValidExpression(_value))
        {
            var valueArr = _value.Split(" ");
            var cronItemsCount = _cronItems.Count;
            for (int i = 0; i < valueArr.Length; i++)
            {
                if (cronItemsCount < i + 1)
                {
                    return;
                }
                _cronItems[i].CronValue = valueArr[i];
            }
        }
    }

    public void SelectTab()
    {
        _selectedPeriod = _cronItems.FirstOrDefault(x => x.DefaultValue != x.CronValue)?.Period.ToString();
    }

    private async Task CronValueChangedAsync(CronItemModel cronItem)
    {
        if (cronItem != null)
        {
            if (cronItem.Period == PeriodTypes.Week)
            {
                var dayItem = _cronItems.FirstOrDefault(p => p.Period == PeriodTypes.Day);
                if (dayItem != null)
                    dayItem.CronValue = cronItem.CronValue == "?" ? "*" : "?";
            }
            else if (cronItem.Period == PeriodTypes.Day)
            {
                var dayItem = _cronItems.FirstOrDefault(p => p.Period == PeriodTypes.Week);
                if (dayItem != null)
                    dayItem.CronValue = cronItem.CronValue == "?" ? "*" : "?";
            }
            ChangeTimeItemDefaultValue(cronItem);
        }

        await CalculateCronValueAsync();
    }

    private void ChangeTimeItemDefaultValue(CronItemModel cronItem)
    {
        if (cronItem.Period != PeriodTypes.Second && cronItem.Period != PeriodTypes.Minute && cronItem.Period != PeriodTypes.Hour)
        {
            SetCronValueToZeroWhenDefault(PeriodTypes.Second);
            SetCronValueToZeroWhenDefault(PeriodTypes.Minute);
            SetCronValueToZeroWhenDefault(PeriodTypes.Hour);

            if (cronItem.Period == PeriodTypes.Month)
            {
                SetCronValueToZeroWhenDefault(PeriodTypes.Day, "1");
                SetCronValueToZeroWhenDefault(PeriodTypes.Week, "?");
            }
            else if (cronItem.Period == PeriodTypes.Year)
            {
                SetCronValueToZeroWhenDefault(PeriodTypes.Day, "1");
                SetCronValueToZeroWhenDefault(PeriodTypes.Week, "?");
                SetCronValueToZeroWhenDefault(PeriodTypes.Month, "1");
            }
        }
    }

    private void SetCronValueToZeroWhenDefault(PeriodTypes period, string defaultValue = "0")
    {
        var item = _cronItems.FirstOrDefault(p => p.Period == period);
        if (item != null && (item.CronValue == "*" || item.CronValue == "?"))
        {
            item.CronValue = defaultValue;
            item.DefaultValue = defaultValue;
        }
    }

    private async Task CalculateCronValueAsync()
    {
        Value = string.Join(" ", _cronItems.OrderBy(p => p.Period).Select(p => p.CronValue));

        try
        {
            CronExpression.ValidateExpression(Value);
            _hasError = false;
        }
        catch (Exception ex)
        {
            _hasError = true;
            _errorMessage = I18n.T(ex.Message);
        }

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(_value);
        }
    }
}
