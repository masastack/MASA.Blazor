using OneOf;

namespace Masa.Blazor
{
    public partial class MDatePicker<TValue> : BDatePicker, IThemeable, IDatePicker
    {
        [Parameter]
        public DatePickerType? ActivePicker
        {
            get
            {
                return GetValue<DatePickerType?>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public Func<DateOnly, bool> DayFormat { get; set; }

        [Parameter]
        public Func<DateOnly, string> HeaderDateFormat { get; set; }

        [Parameter]
        public Func<DateOnly, string> MonthFormat { get; set; }

        [Parameter]
        public bool Multiple { get; set; }

        [Parameter]
        public DateOnly PickerDate { get; set; }

        [Parameter]
        public bool Range { get; set; }

        [Parameter]
        public bool Reactive { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool Scrollable { get; set; }

        [Parameter]
        public OneOf<DateOnly, bool> ShowCurrent { get; set; } = true;

        [Parameter]
        public Func<IList<DateOnly>, string> TitleDateFormat { get; set; }

        [Parameter]
        public string HeaderColor { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public StringNumber Elevation { get; set; }

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool FullWidth { get; set; }

        [Parameter]
        public bool Landscape { get; set; }

        [Parameter]
        public StringNumber Width { get; set; } = 290;

        [Parameter]
        public Func<DateOnly, bool> AllowedDates { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public int FirstDayOfWeek { get; set; }

        [Parameter]
        public DateOnly? Min { get; set; }

        [Parameter]
        public DateOnly? Max { get; set; }

        [Parameter]
        public bool ShowAdjacentMonths { get; set; }

        [Parameter]
        public bool ShowWeek { get; set; }

        [Parameter]
        public TValue Value
        {
            get
            {
                return GetValue<TValue>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        [Parameter]
        public EventCallback OnInput { get; set; }

        [Parameter]
        public Func<DateOnly, string> WeekdayFormat { get; set; }

        [Parameter]
        public Func<DateOnly, string> YearFormat { get; set; }

        [Parameter]
        public string YearIcon { get; set; }

        [Parameter]
        public string NextIcon { get; set; } = "mdi-chevron-right";

        [Parameter]
        public string PrevIcon { get; set; } = "mdi-chevron-left";

        [Parameter]
        public EventCallback<DateOnly> OnPickerDateUpdate { get; set; }

        [Parameter]
        public EventCallback<DatePickerType> OnActivePickerUpdate { get; set; }

        [Parameter]
        public string Locale { get; set; } = "en-US";

        protected DateOnly TableDate
        {
            get
            {
                return GetValue<DateOnly>();
            }
            set
            {
                SetValue(value);
            }
        }

        protected int TableMonth => TableDate.Month - 1;

        protected int TableYear => TableDate.Year;

        protected DateOnly? MinMonth => Min != null ? new DateOnly(Min.Value.Year, Min.Value.Month, 1) : null;

        protected DateOnly? MaxMonth => Max != null ? new DateOnly(Max.Value.Year, Max.Value.Month, 1) : null;

        protected DateOnly? MinYear => Min != null ? new DateOnly(Min.Value.Year, 1, 1) : null;

        protected DateOnly? MaxYear => Max != null ? new DateOnly(Max.Value.Year, 1, 1) : null;

        public (Func<DateOnly, string> Year, Func<IList<DateOnly>, string> TitleDate) Formatters => (YearFormat ?? DateFormatters.Year(Locale), TitleDateFormat ?? DefaultTitleDateFormatter);

        public Func<IList<DateOnly>, string> DefaultTitleDateFormatter
        {
            get
            {
                return values => IsMultiple && values.Count > 1 ? $"{values.Count} selected" : values.Count > 0 ? (Type == DatePickerType.Date ? $"{values[0].DayOfWeek.ToString()[..3]}, {(Landscape ? "<br>" : "")}{DateFormatters.Month(values[0].Month)[..3]} {values[0].Day}" : $"{DateFormatters.Month(values[0].Month)}"
                ) : "&nbsp;";
            }
        }

        protected DateOnly Current
        {
            get
            {
                if (ShowCurrent.IsT1 && ShowCurrent.AsT1 == true)
                {
                    return DateOnly.FromDateTime(DateTime.Now);
                }

                return ShowCurrent.IsT0 ? ShowCurrent.AsT0 : default;
            }
        }

        protected IList<DateOnly> MultipleValue
        {
            get
            {
                return WrapInArray(Value);
            }
        }

        protected bool IsMultiple
        {
            get
            {
                return Multiple || Range;
            }
        }

        protected DateOnly LastValue
        {
            get
            {
                return IsMultiple ? MultipleValue.LastOrDefault() : (Value is DateOnly date ? date : default);
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Watcher
                .Watch<DatePickerType?>(nameof(ActivePicker), val =>
                {
                    InternalActivePicker = val.Value;
                })
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
                    TableDate = multipleValue.Count > 0 ? multipleValue[multipleValue.Count - 1] : (ShowCurrent.IsT0 ? ShowCurrent.AsT0 : DateOnly.FromDateTime(DateTime.Now));
                });

            InternalActivePicker = ActivePicker ?? Type;

            //Init TableDate
            var multipleValue = WrapInArray(Value);
            TableDate = multipleValue.Count > 0 ? multipleValue[multipleValue.Count - 1] : (ShowCurrent.IsT0 ? ShowCurrent.AsT0 : DateOnly.FromDateTime(DateTime.Now));
        }

        private IList<DateOnly> WrapInArray(TValue value)
        {
            if (value is DateOnly date)
            {
                return new List<DateOnly>()
                {
                    date
                };
            }
            else if (value is IList<DateOnly> dates)
            {
                return dates;
            }
            else
            {
                return new List<DateOnly>();
            }
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-picker--date");
                });

            AbstractProvider
                .ApplyDatePickerDefault()
                .Apply(typeof(BPicker), typeof(MPicker), attrs =>
                {
                    attrs[nameof(MPicker.Color)] = HeaderColor ?? Color;
                    attrs[nameof(MPicker.Dark)] = Dark;
                    attrs[nameof(MPicker.Elevation)] = Elevation;
                    attrs[nameof(MPicker.Flat)] = Flat;
                    attrs[nameof(MPicker.FullWidth)] = FullWidth;
                    attrs[nameof(MPicker.Landscape)] = Landscape;
                    attrs[nameof(MPicker.Light)] = Light;
                    attrs[nameof(MPicker.Width)] = Width;
                    attrs[nameof(MPicker.NoTitle)] = NoTitle;
                })
                .Apply(typeof(BDatePickerTitle), typeof(MDatePickerTitle), attrs =>
                {
                    attrs[nameof(MDatePickerTitle.Date)] = Formatters.TitleDate(MultipleValue);
                    attrs[nameof(MDatePickerTitle.Disabled)] = Disabled;
                    attrs[nameof(MDatePickerTitle.Readonly)] = Readonly;
                    attrs[nameof(MDatePickerTitle.SelectingYear)] = InternalActivePicker == DatePickerType.Year;
                    attrs[nameof(MDatePickerTitle.Year)] = Formatters.Year(new DateOnly(TableDate.Year, 1, 1));
                    attrs[nameof(MDatePickerTitle.YearIcon)] = YearIcon;
                    attrs[nameof(MDatePickerTitle.Value)] = MultipleValue.FirstOrDefault();
                    attrs[nameof(MDatePickerTitle.OnSelectingYearUpdate)] = CreateEventCallback<bool>(value =>
                    {
                        InternalActivePicker = value ? DatePickerType.Year : Type;
                    });
                })
                .Apply(typeof(BDatePickerYears), typeof(MDatePickerYears), attrs =>
                {
                    attrs[nameof(MDatePickerYears.Color)] = Color;
                    attrs[nameof(MDatePickerYears.Format)] = YearFormat;
                    attrs[nameof(MDatePickerYears.Min)] = MinYear;
                    attrs[nameof(MDatePickerYears.Max)] = MaxYear;
                    attrs[nameof(MDatePickerYears.Value)] = TableYear;
                    attrs[nameof(MDatePickerYears.Locale)] = Locale;
                    attrs[nameof(MDatePickerYears.OnInput)] = CreateEventCallback<int>(year =>
                    {
                        TableDate = new DateOnly(year, TableDate.Month, TableDate.Day);
                        InternalActivePicker = DatePickerType.Month;
                    });
                })
                .Apply(typeof(BDatePickerHeader), typeof(MDatePickerHeader), attrs =>
                {
                    attrs[nameof(MDatePickerHeader.NextIcon)] = NextIcon;
                    attrs[nameof(MDatePickerHeader.Color)] = Color;
                    attrs[nameof(MDatePickerHeader.Dark)] = Dark;
                    attrs[nameof(MDatePickerHeader.Disabled)] = Disabled;
                    attrs[nameof(MDatePickerHeader.Format)] = HeaderDateFormat;
                    attrs[nameof(MDatePickerHeader.Light)] = Light;
                    attrs[nameof(MDatePickerHeader.Min)] = InternalActivePicker == DatePickerType.Date ? MinMonth : MinYear;
                    attrs[nameof(MDatePickerHeader.Max)] = InternalActivePicker == DatePickerType.Date ? MaxMonth : MaxYear;
                    attrs[nameof(MDatePickerHeader.PrevIcon)] = PrevIcon;
                    attrs[nameof(MDatePickerHeader.Readonly)] = Readonly;
                    attrs[nameof(MDatePickerHeader.Locale)] = Locale;
                    attrs[nameof(MDatePickerHeader.ActivePicker)] = InternalActivePicker;
                    attrs[nameof(MDatePickerHeader.Value)] = Type == DatePickerType.Date ? new DateOnly(TableYear, TableMonth + 1, 1) : new DateOnly(TableYear, 1, 1);
                    attrs[nameof(MDatePickerHeader.OnInput)] = CreateEventCallback<DateOnly>(value =>
                    {
                        TableDate = value;
                    });
                    attrs[nameof(MDatePickerHeader.OnToggle)] = EventCallback.Factory.Create(this, () =>
                    {
                        InternalActivePicker = InternalActivePicker == DatePickerType.Date ? DatePickerType.Month : DatePickerType.Year;
                    });
                })
                .Apply<BDatePickerTable, MDatePickerDateTable<TValue>>("date-table", attrs =>
                {
                    attrs[nameof(MDatePickerDateTable<TValue>.AllowedDates)] = AllowedDates;
                    attrs[nameof(MDatePickerDateTable<TValue>.Color)] = Color;
                    attrs[nameof(MDatePickerDateTable<TValue>.Current)] = Current;
                    attrs[nameof(MDatePickerDateTable<TValue>.Dark)] = Dark;
                    attrs[nameof(MDatePickerDateTable<TValue>.Disabled)] = Disabled;
                    attrs[nameof(MDatePickerDateTable<TValue>.FirstDayOfWeek)] = FirstDayOfWeek;
                    attrs[nameof(MDatePickerDateTable<TValue>.Format)] = DayFormat;
                    attrs[nameof(MDatePickerDateTable<TValue>.Light)] = Light;
                    attrs[nameof(MDatePickerDateTable<TValue>.Min)] = Min;
                    attrs[nameof(MDatePickerDateTable<TValue>.Max)] = Max;
                    attrs[nameof(MDatePickerDateTable<TValue>.Range)] = Range;
                    attrs[nameof(MDatePickerDateTable<TValue>.Locale)] = Locale;
                    attrs[nameof(MDatePickerDateTable<TValue>.Readonly)] = Readonly;
                    attrs[nameof(MDatePickerDateTable<TValue>.Scrollable)] = Scrollable;
                    attrs[nameof(MDatePickerDateTable<TValue>.ShowAdjacentMonths)] = ShowAdjacentMonths;
                    attrs[nameof(MDatePickerDateTable<TValue>.ShowWeek)] = ShowWeek;
                    attrs[nameof(MDatePickerDateTable<TValue>.TableDate)] = new DateOnly(TableYear, TableMonth + 1, 1);
                    attrs[nameof(MDatePickerDateTable<TValue>.Value)] = IsMultiple ? MultipleValue : Value;
                    attrs[nameof(MDatePickerDateTable<TValue>.WeekdayFormat)] = WeekdayFormat;
                    attrs[nameof(MDatePickerDateTable<TValue>.OnInput)] = EventCallback.Factory.Create<DateOnly>(this, OnDateClickAsync);
                })
                .Apply<BDatePickerTable, MDatePickerMonthTable<TValue>>("month-table", attrs =>
                {
                    attrs[nameof(MDatePickerMonthTable<TValue>.AllowedDates)] = Type == DatePickerType.Month ? AllowedDates : null;
                    attrs[nameof(MDatePickerMonthTable<TValue>.Color)] = Color;
                    attrs[nameof(MDatePickerDateTable<TValue>.Current)] = Current;
                    attrs[nameof(MDatePickerMonthTable<TValue>.Dark)] = Dark;
                    attrs[nameof(MDatePickerMonthTable<TValue>.Disabled)] = Disabled;
                    attrs[nameof(MDatePickerDateTable<TValue>.Format)] = MonthFormat;
                    attrs[nameof(MDatePickerMonthTable<TValue>.Light)] = Light;
                    attrs[nameof(MDatePickerMonthTable<TValue>.Min)] = MinMonth;
                    attrs[nameof(MDatePickerMonthTable<TValue>.Max)] = MaxMonth;
                    attrs[nameof(MDatePickerMonthTable<TValue>.Locale)] = Locale;
                    attrs[nameof(MDatePickerDateTable<TValue>.Range)] = Range;
                    attrs[nameof(MDatePickerDateTable<TValue>.Readonly)] = Readonly && Type == DatePickerType.Month;
                    attrs[nameof(MDatePickerDateTable<TValue>.Scrollable)] = Scrollable;
                    attrs[nameof(MDatePickerMonthTable<TValue>.Value)] = IsMultiple ? MultipleValue : Value;
                    attrs[nameof(MDatePickerMonthTable<TValue>.TableDate)] = new DateOnly(TableYear, 1, 1);
                    attrs[nameof(MDatePickerMonthTable<TValue>.OnInput)] = EventCallback.Factory.Create<DateOnly>(this, OnMonthClickAsync);
                });
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
}
