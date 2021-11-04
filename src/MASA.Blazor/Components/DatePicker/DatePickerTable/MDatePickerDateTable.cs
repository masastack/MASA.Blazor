using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MDatePickerDateTable<TValue> : MDatePickerTable<TValue>, IDatePickerDateTable, IDatePickerTable
    {
        [Parameter]
        public int FirstDayOfWeek { get; set; }

        [Parameter]
        public Func<DateOnly, string> WeekdayFormat { get; set; }

        [Parameter]
        public bool ShowWeek { get; set; }

        [Parameter]
        public bool ShowAdjacentMonths { get; set; }

        [Parameter]
        public EventCallback<int> OnDaySelected { get; set; }

        public override Func<DateOnly, string> Formatter
        {
            get
            {
                if (Format != null)
                {
                    return Format;
                }

                return value => value.Day.ToString();
            }
        }

        public Func<DateOnly, string> WeekdayFormatter
        {
            get
            {
                if (WeekdayFormat != null)
                {
                    return WeekdayFormat;
                }

                return null;
            }
        }

        public IEnumerable<string> WeekDays
        {
            get
            {
                var first = FirstDayOfWeek;

                var range = Enumerable.Range(0, 7);
                return WeekdayFormatter != null ? range.Select(i => WeekdayFormat(DateOnly.Parse($"2017-01-{first + i + 15}"))) : range.Select(i => new[] { "S", "M", "T", "W", "T", "F", "S" }[(i + first) % 7]);
            }
        }

        public int WeekDaysBeforeFirstDayOfTheMonth
        {
            get
            {
                var firstDayOfTheMonth = new DateOnly(DisplayedYear, DisplayedMonth + 1, 1);
                var weekDay = (int)firstDayOfTheMonth.DayOfWeek;

                return (weekDay - FirstDayOfWeek + 7) % 7;
            }
        }

        public int GetWeekNumber(int dayInMonth)
        {
            return 0;
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-date-picker-table")
                        .Add("m-date-picker-table--date");
                })
                .Apply("date-week", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-date-picker-table--date__week");
                });

            AbstractProvider
                .ApplyDatePickerDateTableDefault();
        }
    }
}
