namespace Masa.Blazor.Components.DatePicker;

public partial class MDatePickerTable<TValue> : ThemeComponentBase
{
    [Inject] public MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public OneOf<DateOnly[], Func<DateOnly, bool>>? Events { get; set; }

    [Parameter] public OneOf<string, Func<DateOnly, string>, Func<DateOnly, string[]>>? EventColor { get; set; }

    [Parameter] public Func<DateOnly, bool>? AllowedDates { get; set; }

    [Parameter] public DateOnly? Min { get; set; }

    [Parameter] public DateOnly? Max { get; set; }

    [Parameter] public bool Range { get; set; }

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public bool Scrollable { get; set; }

    [Parameter] public TValue? Value { get; set; }

    [Parameter] public DateOnly Current { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public Func<DateOnly, string>? Format { get; set; }

    [Parameter] public EventCallback<DateOnly> OnInput { get; set; }

    [Parameter] [MasaApiParameter(ReleasedIn = "v1.6.0")] public EventCallback<DateOnly> OnDateClick { get; set; }

    /// <summary>
    /// The input parameter is the first day of the month
    /// </summary>
    [Parameter] [MasaApiParameter(ReleasedIn = "v1.6.0")] public EventCallback<DateOnly> OnMonthClick { get; set; }

    [Parameter] [MasaApiParameter(ReleasedIn = "v1.6.0")] public CultureInfo Locale { get; set; } = null!;

    [Parameter] public CalendarWeekRule CalendarWeekRule { get; set; }

    [Parameter]
    public DateOnly TableDate
    {
        get => GetValue<DateOnly>();
        set => SetValue(value);
    }

    public int DisplayedMonth => TableDate.Month - 1;

    public int DisplayedYear => TableDate.Year;

    public virtual Func<DateOnly, string>? Formatter { get; }

    protected bool IsReversing { get; set; }

    private async Task HandleOnClickAsync(DateOnly value, bool isMonth)
    {
        if (Disabled)
        {
            return;
        }

        if (IsDateAllowed(value, Min, Max, AllowedDates) && !Readonly)
        {
            await OnInput.InvokeAsync(value);
            await (isMonth ? OnMonthClick : OnDateClick).InvokeAsync(value);
        }
    }

    protected bool IsDateAllowed(DateOnly date, DateOnly? min, DateOnly? max, Func<DateOnly, bool>? allowedFunc)
    {
        return (allowedFunc == null || allowedFunc(date)) && (min == null || date >= min) &&
               (max == null || date <= max);
    }

    protected virtual bool IsSelected(DateOnly value)
    {
        if (Value is DateOnly date)
        {
            return date == value;
        }
        else if (Value is IList<DateOnly> dates)
        {
            if (Range && dates.Count == 2)
            {
                return dates.Min() <= value && value <= dates.Max();
            }

            return dates.Contains(value);
        }

        return false;
    }

    protected virtual bool IsCurrent(DateOnly value)
    {
        return value == Current;
    }

    protected string ComputedTransition
        => IsReversing == !MasaBlazor.RTL ? "tab-reverse-transition" : "tab-transition";

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher
            .Watch<DateOnly>(nameof(TableDate), (newVal, oldVal) => { IsReversing = newVal < oldVal; });
    }

    protected static Block Block = new("m-date-picker-table");
    private ModifierBuilder _modifierBuilder = Block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add(Disabled).AddTheme(ComputedTheme).Build();
    }
}