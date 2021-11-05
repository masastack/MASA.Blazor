using BlazorComponent;
using BlazorComponent.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASA.Blazor
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
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

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
        public Func<DateOnly, string> WeekdayFormat { get; set; }

        [Parameter]
        public Func<int, string> YearFormat { get; set; }

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

        public (Func<int, string> Year, Func<IList<DateOnly>, string> TitleDate) Formatters => (YearFormat, TitleDateFormat ?? DefaultTitleDateFormatter);

        public Func<IList<DateOnly>, string> DefaultTitleDateFormatter
        {
            get
            {
                return values => IsMultiple && values.Count > 1 ? $"{values.Count} selected" : values.Count > 0 ? (Type == DatePickerType.Date ? $"{values[0].DayOfWeek.ToString()[..3]}, {(Landscape ? "<br>" : "")}{DatePickerFormatter.Month(values[0].Month)[..3]} {values[0].Day}" : $"{DatePickerFormatter.Month(values[0].Month)}"
                ) : "-";
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
                });

            InternalActivePicker = ActivePicker ?? Type;
            var multipleValue = WrapInArray(Value);
            var date = multipleValue.Count > 0 ? multipleValue[multipleValue.Count - 1] : (ShowCurrent.IsT0 ? ShowCurrent.AsT0 : DateOnly.FromDateTime(DateTime.Now.AddMonths(1)));
            TableDate = date;
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
                .Apply(typeof(BPicker), typeof(MPicker), props =>
                {
                    props[nameof(MPicker.Color)] = HeaderColor ?? Color;
                    props[nameof(MPicker.Dark)] = Dark;
                    props[nameof(MPicker.Elevation)] = Elevation;
                    props[nameof(MPicker.Flat)] = Flat;
                    props[nameof(MPicker.FullWidth)] = FullWidth;
                    props[nameof(MPicker.Landscape)] = Landscape;
                    props[nameof(MPicker.Light)] = Light;
                    props[nameof(MPicker.Width)] = Width;
                    props[nameof(MPicker.NoTitle)] = NoTitle;
                })
                .Apply(typeof(BDatePickerTitle), typeof(MDatePickerTitle), props =>
                {
                    props[nameof(MDatePickerTitle.Date)] = Formatters.TitleDate(MultipleValue);
                    props[nameof(MDatePickerTitle.Disabled)] = Disabled;
                    props[nameof(MDatePickerTitle.Readonly)] = Readonly;
                    props[nameof(MDatePickerTitle.SelectingYear)] = InternalActivePicker == DatePickerType.Year;
                    props[nameof(MDatePickerTitle.Year)] = TableDate.Year;
                    props[nameof(MDatePickerTitle.YearIcon)] = YearIcon;
                    props[nameof(MDatePickerTitle.Value)] = MultipleValue.FirstOrDefault();
                    props[nameof(MDatePickerTitle.OnSelectingYearUpdate)] = CreateEventCallback<bool>(value =>
                    {
                        InternalActivePicker = value ? DatePickerType.Year : Type;
                    });
                })
                .Apply(typeof(BDatePickerYears), typeof(MDatePickerYears), props =>
                {
                    props[nameof(MDatePickerYears.Color)] = Color;
                    props[nameof(MDatePickerYears.Format)] = YearFormat;
                    props[nameof(MDatePickerYears.Min)] = MinYear;
                    props[nameof(MDatePickerYears.Max)] = MaxYear;
                    props[nameof(MDatePickerYears.Value)] = TableYear;
                    props[nameof(MDatePickerYears.OnInput)] = CreateEventCallback<int>(year =>
                    {
                        TableDate = new DateOnly(year, TableDate.Month, TableDate.Day);
                        InternalActivePicker = DatePickerType.Month;
                    });
                })
                .Apply(typeof(BDatePickerHeader), typeof(MDatePickerHeader), props =>
                {
                    props[nameof(MDatePickerHeader.NextIcon)] = NextIcon;
                    props[nameof(MDatePickerHeader.Color)] = Color;
                    props[nameof(MDatePickerHeader.Dark)] = Dark;
                    props[nameof(MDatePickerHeader.Disabled)] = Disabled;
                    props[nameof(MDatePickerHeader.Format)] = HeaderDateFormat;
                    props[nameof(MDatePickerHeader.Light)] = Light;
                    props[nameof(MDatePickerHeader.Min)] = InternalActivePicker == DatePickerType.Date ? MinMonth : MinYear;
                    props[nameof(MDatePickerHeader.Max)] = InternalActivePicker == DatePickerType.Date ? MaxMonth : MaxYear;
                    props[nameof(MDatePickerHeader.PrevIcon)] = PrevIcon;
                    props[nameof(MDatePickerHeader.Readonly)] = Readonly;
                    props[nameof(MDatePickerHeader.ActivePicker)] = InternalActivePicker;
                    props[nameof(MDatePickerHeader.Value)] = new DateOnly(TableYear, TableMonth + 1, 1);
                    props[nameof(MDatePickerHeader.OnInput)] = CreateEventCallback<DateOnly>(value =>
                   {
                       TableDate = value;
                   });
                    props[nameof(MDatePickerHeader.OnToggle)] = EventCallback.Factory.Create(this, () =>
                    {
                        InternalActivePicker = InternalActivePicker == DatePickerType.Date ? DatePickerType.Month : DatePickerType.Year;
                    });
                })
                .Apply<BDatePickerTable, MDatePickerDateTable<TValue>>("date-table", props =>
                {
                    props[nameof(MDatePickerDateTable<TValue>.AllowedDates)] = AllowedDates;
                    props[nameof(MDatePickerDateTable<TValue>.Color)] = Color;
                    props[nameof(MDatePickerDateTable<TValue>.Current)] = Current;
                    props[nameof(MDatePickerDateTable<TValue>.Dark)] = Dark;
                    props[nameof(MDatePickerDateTable<TValue>.Disabled)] = Disabled;
                    props[nameof(MDatePickerDateTable<TValue>.FirstDayOfWeek)] = FirstDayOfWeek;
                    props[nameof(MDatePickerDateTable<TValue>.Format)] = DayFormat;
                    props[nameof(MDatePickerDateTable<TValue>.Light)] = Light;
                    props[nameof(MDatePickerDateTable<TValue>.Min)] = Min;
                    props[nameof(MDatePickerDateTable<TValue>.Max)] = Max;
                    props[nameof(MDatePickerDateTable<TValue>.Range)] = Range;
                    props[nameof(MDatePickerDateTable<TValue>.Readonly)] = Readonly;
                    props[nameof(MDatePickerDateTable<TValue>.Scrollable)] = Scrollable;
                    props[nameof(MDatePickerDateTable<TValue>.ShowAdjacentMonths)] = ShowAdjacentMonths;
                    props[nameof(MDatePickerDateTable<TValue>.ShowWeek)] = ShowWeek;
                    props[nameof(MDatePickerDateTable<TValue>.TableDate)] = new DateOnly(TableYear, TableMonth + 1, 1);
                    props[nameof(MDatePickerDateTable<TValue>.Value)] = IsMultiple ? MultipleValue : Value;
                    props[nameof(MDatePickerDateTable<TValue>.WeekdayFormat)] = WeekdayFormat;
                    props[nameof(MDatePickerDateTable<TValue>.OnInput)] = EventCallback.Factory.Create<DateOnly>(this, OnDateClickAsync);
                })
                .Apply<BDatePickerTable, MDatePickerMonthTable<TValue>>("month-table", props =>
                {
                    props[nameof(MDatePickerMonthTable<TValue>.AllowedDates)] = Type == DatePickerType.Month ? AllowedDates : null;
                    props[nameof(MDatePickerMonthTable<TValue>.Color)] = Color;
                    props[nameof(MDatePickerDateTable<TValue>.Current)] = Current;
                    props[nameof(MDatePickerMonthTable<TValue>.Dark)] = Dark;
                    props[nameof(MDatePickerMonthTable<TValue>.Disabled)] = Disabled;
                    props[nameof(MDatePickerDateTable<TValue>.Format)] = MonthFormat;
                    props[nameof(MDatePickerMonthTable<TValue>.Light)] = Light;
                    props[nameof(MDatePickerMonthTable<TValue>.Min)] = MinMonth;
                    props[nameof(MDatePickerMonthTable<TValue>.Max)] = MaxMonth;
                    props[nameof(MDatePickerDateTable<TValue>.Range)] = Range;
                    props[nameof(MDatePickerDateTable<TValue>.Readonly)] = Readonly && Type == DatePickerType.Month;
                    props[nameof(MDatePickerDateTable<TValue>.Scrollable)] = Scrollable;
                    props[nameof(MDatePickerMonthTable<TValue>.Value)] = IsMultiple ? MultipleValue : Value;
                    props[nameof(MDatePickerMonthTable<TValue>.TableDate)] = new DateOnly(TableYear, 1, 1);
                    props[nameof(MDatePickerMonthTable<TValue>.OnInput)] = EventCallback.Factory.Create<DateOnly>(this, OnMonthClickAsync);
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
        }
    }
}
