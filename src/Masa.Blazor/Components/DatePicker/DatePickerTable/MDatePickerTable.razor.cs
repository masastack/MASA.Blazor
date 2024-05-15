namespace Masa.Blazor;

public partial class MDatePickerTable<TValue> : MasaComponentBase
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

    [Parameter] public CultureInfo Locale { get; set; } = null!;

    [Parameter] public CalendarWeekRule CalendarWeekRule { get; set; }

    [Parameter]
    public DateOnly TableDate
    {
        get => GetValue<DateOnly>();
        set => SetValue(value);
    }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

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

    public int DisplayedMonth => TableDate.Month - 1;

    public int DisplayedYear => TableDate.Year;

    public virtual Func<DateOnly, string>? Formatter { get; }

    protected bool IsReversing { get; set; }

    private async Task HandleOnClickAsync(DateOnly value)
    {
        if (Disabled)
        {
            return;
        }

        if (IsDateAllowed(value, Min, Max, AllowedDates) && !Readonly)
        {
            if (OnInput.HasDelegate)
            {
                await OnInput.InvokeAsync(value);
            }
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

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif

    protected Block Block = new("m-date-picker-table");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return Block.Modifier(Disabled).AddTheme(IsDark, IndependentTheme).GenerateCssClasses();
    }
}