namespace Masa.Blazor;

public partial class MDatePickerHeader : MasaComponentBase
{
    [Inject] public MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public string? Color { get; set; } = "accent";

    [Parameter] public DateOnly? Min { get; set; }

    [Parameter] public DateOnly? Max { get; set; }

    [Parameter] public EventCallback<DateOnly> OnInput { get; set; }

    [Parameter]
    public DateOnly Value
    {
        get => GetValue<DateOnly>();
        set => SetValue(value);
    }

    [Parameter] public EventCallback OnToggle { get; set; }

    [Parameter] public string? PrevIcon { get; set; }

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public string? NextIcon { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public Func<DateOnly, string>? Format { get; set; }

    [Parameter] public DatePickerType ActivePicker { get; set; }

    [Parameter] public CultureInfo Locale { get; set; } = null!;

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

    public bool RTL => MasaBlazor.RTL;

    protected bool IsReversing { get; set; }

    public string Transition => IsReversing == !MasaBlazor.RTL ? "tab-reverse-transition" : "tab-transition";
    
    private async Task HandleOnClickAsync(MouseEventArgs args)
    {
        if (OnToggle.HasDelegate)
        {
            await OnToggle.InvokeAsync();
        }
    }

    public Func<DateOnly, string> Formatter
    {
        get
        {
            if (Format != null)
            {
                return Format;
            }

            return ActivePicker == DatePickerType.Date ? DateFormatters.YearMonth(Locale) : DateFormatters.Year(Locale);
        }
    }

    public DateOnly CalculateChange(int sign)
    {
        if (ActivePicker == DatePickerType.Month)
        {
            var date = Value.AddYears(sign);
            return new DateOnly(date.Year, 1, 1);
        }

        return MonthChange(Value, sign);
    }

    public static DateOnly MonthChange(DateOnly value, int sign)
    {
        var date = value.AddMonths(sign);
        return new DateOnly(date.Year, date.Month, 1);
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher
            .Watch<DateOnly>(nameof(Value), (newVal, oldVal) => { IsReversing = newVal < oldVal; });
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

    private Block _block = new("m-date-picker-header");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier(Disabled).AddTheme(IsDark, IndependentTheme).GenerateCssClasses();
    }
}