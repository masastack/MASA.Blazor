namespace Masa.Blazor;

public partial class MDatePicker<TValue> : MasaComponentBase
{
    [Inject] protected I18n I18n { get; set; }

    [Parameter]
    public DatePickerType? ActivePicker
    {
        get => GetValue<DatePickerType?>();
        set => SetValue(value);
    }

    [Parameter] public Func<DateOnly, string>? DayFormat { get; set; }

    [Parameter] public Func<DateOnly, string>? HeaderDateFormat { get; set; }

    [Parameter] public Func<DateOnly, string>? MonthFormat { get; set; }

    [Parameter] public bool Multiple { get; set; }

    [Parameter] public bool Range { get; set; }

    [Parameter] public bool Reactive { get; set; }

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public bool Scrollable { get; set; }

    [Parameter] [MasaApiParameter(true)] public OneOf<DateOnly, bool> ShowCurrent { get; set; } = true;

    [Parameter] public Func<IList<DateOnly>, string>? TitleDateFormat { get; set; }

    [Parameter] public string? HeaderColor { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public StringNumber? Elevation { get; set; }

    [Parameter] public bool Flat { get; set; }

    [Parameter] public bool FullWidth { get; set; }

    [Parameter] public bool Landscape { get; set; }

    [Parameter] [MasaApiParameter(290)] public StringNumber Width { get; set; } = 290;

    [Parameter] public Func<DateOnly, bool>? AllowedDates { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public OneOf<DateOnly[], Func<DateOnly, bool>>? Events { get; set; }

    [Parameter] public OneOf<string, Func<DateOnly, string>, Func<DateOnly, string[]>>? EventColor { get; set; }

    [Parameter] public int FirstDayOfWeek { get; set; }

    [Parameter] public DateOnly? Min { get; set; }

    [Parameter] public DateOnly? Max { get; set; }

    [Parameter] public bool ShowAdjacentMonths { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedOn = "v1.4.0")]
    public bool ShowWeek { get; set; }

    [Parameter]
    public TValue? Value
    {
        get => GetValue<TValue>();
        set => SetValue(value);
    }

    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter] public EventCallback OnInput { get; set; }

    [Parameter] public Func<DateOnly, string>? WeekdayFormat { get; set; }

    [Parameter] public Func<DateOnly, string>? YearFormat { get; set; }

    [Parameter] public string? YearIcon { get; set; }

    [Parameter]
    [MasaApiParameter("$next")]
    public string NextIcon { get; set; } = "$next";

    [Parameter]
    [MasaApiParameter("$prev")]
    public string PrevIcon { get; set; } = "$prev";

    [Parameter] public EventCallback<DateOnly> OnPickerDateUpdate { get; set; }

    [Parameter] public EventCallback<DatePickerType> OnActivePickerUpdate { get; set; }

    [Parameter] public string? Locale { get; set; }

    [Parameter]
    [MasaApiParameter(CalendarWeekRule.FirstDay, "v1.4.0")]
    public CalendarWeekRule CalendarWeekRule { get; set; }

    [Parameter] public bool NoTitle { get; set; }

    [Parameter] public DatePickerType Type { get; set; } = DatePickerType.Date;

    [Parameter] public RenderFragment? ChildContent { get; set; }

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

    protected DatePickerType InternalActivePicker
    {
        get { return GetValue<DatePickerType>(); }
        set { SetValue(value); }
    }

    public CultureInfo CurrentLocale
    {
        get
        {
            var culture = I18n.Culture;

            if (Locale is not null)
            {
                try
                {
                    culture = CultureInfo.CreateSpecificCulture(Locale);
                }
                catch (CultureNotFoundException e)
                {
                    Logger.LogWarning(e, "Locale {Locale} is not found", Locale);
                }
            }

            return culture;
        }
    }

    protected DateOnly TableDate
    {
        get => GetValue<DateOnly>();
        set => SetValue(value);
    }

    protected int TableMonth => TableDate.Month - 1;

    protected int TableYear => TableDate.Year;

    protected DateOnly? MinMonth => Min != null ? new DateOnly(Min.Value.Year, Min.Value.Month, 1) : null;

    protected DateOnly? MaxMonth => Max != null ? new DateOnly(Max.Value.Year, Max.Value.Month, 1) : null;

    protected DateOnly? MinYear => Min != null ? new DateOnly(Min.Value.Year, 1, 1) : null;

    protected DateOnly? MaxYear => Max != null ? new DateOnly(Max.Value.Year, 1, 1) : null;

    public (Func<DateOnly, string> Year, Func<IList<DateOnly>, string> TitleDate) Formatters => (
        YearFormat ?? DateFormatters.Year(CurrentLocale), TitleDateFormat ?? DefaultTitleDateFormatter);

    public Func<IList<DateOnly>, string> DefaultTitleDateFormatter
    {
        get
        {
            return values =>
            {
                if (IsMultiple && values.Count > 1)
                {
                    return string.Format(I18n.T("$masaBlazor.datePicker.itemsSelected"), values.Count);
                }

                if (values.Count > 0)
                {
                    var date = values[0];

                    if (Type == DatePickerType.Date)
                    {
                        var str = DateFormatters.AbbreviatedDayOfWeek(CurrentLocale)(date) + ", " +
                                  DateFormatters.MonthDay(CurrentLocale)(date);
                        if (Landscape)
                        {
                            return str.Replace(", ", "<br>");
                        }

                        return str;
                    }
                    else
                    {
                        return DateFormatters.Month(CurrentLocale)(date);
                    }
                }

                return "&nbsp;";
            };
        }
    }

    protected DateOnly Current
    {
        get
        {
            if (ShowCurrent.IsT1 && ShowCurrent.AsT1)
            {
                return DateOnly.FromDateTime(DateTime.Now);
            }

            return ShowCurrent.IsT0 ? ShowCurrent.AsT0 : default;
        }
    }

    protected IList<DateOnly> MultipleValue => WrapInArray(Value);

    protected bool IsMultiple => Multiple || Range;

    protected DateOnly LastValue =>
        IsMultiple ? MultipleValue.LastOrDefault() : (Value is DateOnly date ? date : default);

    protected override void OnInitialized()
    {
        base.OnInitialized();

        InternalActivePicker = ActivePicker ?? Type;

        //Init TableDate
        var multipleValue = WrapInArray(Value);
        TableDate = multipleValue.Count > 0
            ? multipleValue[multipleValue.Count - 1]
            : (ShowCurrent.IsT0 ? ShowCurrent.AsT0 : DateOnly.FromDateTime(DateTime.Now));
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher
            .Watch<DatePickerType?>(nameof(ActivePicker), val => { InternalActivePicker = val ?? Type; })
            .Watch<DateOnly>(nameof(TableDate), val =>
            {
                if (OnPickerDateUpdate.HasDelegate)
                {
                    OnPickerDateUpdate.InvokeAsync(val);
                }
            })
            .Watch<DatePickerType>(nameof(InternalActivePicker), val =>
            {
                if (OnActivePickerUpdate.HasDelegate)
                {
                    OnActivePickerUpdate.InvokeAsync(val);
                }
            })
            .Watch<TValue>(nameof(Value), val =>
            {
                var multipleValue = WrapInArray(val);
                TableDate = multipleValue.Count > 0
                    ? multipleValue[^1]
                    : (ShowCurrent.IsT0 ? ShowCurrent.AsT0 : DateOnly.FromDateTime(DateTime.Now));
            });
    }

    private IList<DateOnly> WrapInArray(TValue? value)
    {
        if (value is DateOnly date && date != DateOnly.MinValue && date != DateOnly.MaxValue)
        {
            return new List<DateOnly>
            {
                date
            };
        }

        if (value is DateTime dateTime && dateTime != DateTime.MinValue && dateTime != DateTime.MaxValue)
        {
            return new List<DateOnly>
            {
                DateOnly.FromDateTime(dateTime)
            };
        }

        if (value is IList<DateOnly> dates && dates.All(d => d != DateOnly.MinValue && d != DateOnly.MaxValue))
        {
            return dates;
        }

        if (value is IList<DateTime> dateTimes && dateTimes.All(d => d != DateTime.MinValue && d != DateTime.MaxValue))
        {
            return dateTimes.Select(DateOnly.FromDateTime).ToList();
        }

        return new List<DateOnly>();
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-picker--date";
    }

    private async Task OnMonthClickAsync(DateOnly value)
    {
        if (Type == DatePickerType.Date)
        {
            TableDate = value;
            InternalActivePicker = DatePickerType.Date;
        }
        else
        {
            if (IsMultiple)
            {
                var values = MultipleValue;

                if (!values.Contains(value))
                {
                    values.Add(value);
                }
                else
                {
                    values.Remove(value);
                }

                Value = (TValue)values;
            }
            else
            {
                Value = value is TValue val ? val : default;
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            //REVIEW:  
            if (OnInput.HasDelegate)
            {
                await OnInput.InvokeAsync();
            }
        }
    }

    private async Task OnDateClickAsync(DateOnly value)
    {
        if (IsMultiple)
        {
            var values = MultipleValue;

            if (Range)
            {
                if (values.Count == 2)
                {
                    values = new List<DateOnly>() { value };
                }
                else
                {
                    values.Add(value);
                }

                values = values.OrderBy(val => val).ToList();
            }
            else
            {
                if (!values.Contains(value))
                {
                    values.Add(value);
                }
                else
                {
                    values.Remove(value);
                }
            }

            Value = (TValue)values;
        }
        else
        {
            Value = value is TValue val ? val : default;
        }

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }

        //REVIEW:  
        if (OnInput.HasDelegate)
        {
            await OnInput.InvokeAsync();
        }
    }
}