namespace Masa.Blazor.Presets;

public partial class PCron
{
    [Inject] private I18n I18n { get; set; } = null!;

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public StringNumber? MinHeight { get; set; }

    [Parameter] public bool Outlined { get; set; }

    [Parameter] public bool NoTransition { get; set; }

    [Parameter] public string? Value { get; set; }

    [Parameter] public EventCallback<string?> ValueChanged { get; set; }

    [Parameter] public EventCallback<string> OnChange { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")]
    public bool CascadingIsDark { get; set; }

    public bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            if (Light)
            {
                return false;
            }

            return CascadingIsDark;
        }
    }

    private string? _prevValue;

    private bool _hasError;

    private string _errorMessage = string.Empty;

    private StringNumber? _selectedPeriod;

    private readonly List<CronItemModel> _cronItems = new();

    private Block Block => new("m-cron");

    protected override void OnInitialized()
    {
        InitCronItems();

        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (_prevValue != Value)
        {
            _prevValue = Value;

            OnValueChanged();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender && string.IsNullOrWhiteSpace(Value))
        {
            await CalculateCronValueAsync();
        }
    }

    private void OnValueChanged()
    {
        if (string.IsNullOrWhiteSpace(Value) || !CronExpression.IsValidExpression(Value))
        {
            return;
        }

        var valueArr = Value.Split(" ");
        var cronItemsCount = _cronItems.Count;
        for (var i = 0; i < valueArr.Length; i++)
        {
            if (cronItemsCount < i + 1)
            {
                return;
            }

            _cronItems[i].CronValue = valueArr[i];
        }
    }

    private void InitCronItems()
    {
        if (_cronItems.Any()) return;

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
        var value = string.Join(" ", _cronItems.OrderBy(p => p.Period).Select(p => p.CronValue));

        try
        {
            CronExpression.ValidateExpression(value);
            _hasError = false;
        }
        catch (Exception ex)
        {
            _hasError = true;
            _errorMessage = I18n.T(ex.Message);
        }

        if (_hasError)
        {
            return;
        }

        if (ValueChanged.HasDelegate)
        {
            _prevValue = value;
            await ValueChanged.InvokeAsync(value);
        }
        else
        {
            Value = value;
        }

        await OnChange.InvokeAsync(value);
    }
}
