using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace MASA.Blazor
{
    public partial class MCalendarWeekly : BCalendarWeekly, ICalendarWeekly
    {
        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        public bool IsDark => Dark ?
            true :
            (Light ? false : Themeable != null && Themeable.IsDark);

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-weekly")
                        .AddTheme(IsDark);
                })
                .Apply("head", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-weekly__head");
                })
                .Apply("headNumber", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-weekly__head-weeknumber");
                })
                .Apply("headDay", cssBuilder =>
                {
                    var timestamp = cssBuilder.Data as CalendarTimestamp;
                    var outside = IsOutside(timestamp);
                    cssBuilder
                        .Add("m-calendar-weekly__head-weekday")
                        .AddIf("m-present", () => timestamp?.Present ?? false)
                        .AddIf("m-past", () => timestamp?.Past ?? false)
                        .AddIf("m-future", () => timestamp?.Future ?? false)
                        .AddIf("m-outside", () => outside);
                }, styleBuilder =>
                {
                    var timestamp = styleBuilder.Data as CalendarTimestamp;
                    styleBuilder
                        .AddTextColor((timestamp?.Present ?? false) ? Color : string.Empty);
                })
                .Apply("week", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-weekly__week");
                })
                .Apply("weeknumber", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-weekly__weeknumber");
                })
                .Apply("day", cssBuilder =>
                {
                    var timestamp = cssBuilder.Data as CalendarTimestamp;
                    var outside = IsOutside(timestamp);
                    cssBuilder
                        .Add("m-calendar-weekly__day")
                        .AddIf("m-present", () => timestamp?.Present ?? false)
                        .AddIf("m-past", () => timestamp?.Past ?? false)
                        .AddIf("m-future", () => timestamp?.Future ?? false)
                        .AddIf("m-outside", () => outside);
                })
                .Apply("dayLabel", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-weekly__day-label");
                });

            AbstractProvider
                .ApplyCalendarWeeklyDefault()
                .Apply<BButton, MButton>(props =>
                {
                    props.TryGetValue("ItemIndex", out var itemIndexStr);
                    var itemIndex = Convert.ToInt32(itemIndexStr);
                    var day = Days?[itemIndex];

                    props[nameof(MButton.Color)] = (day?.Present ?? false) ? Color : "transparent";
                    props[nameof(MButton.Fab)] = true;
                    props[nameof(MButton.Depressed)] = true;
                    props[nameof(MButton.Small)] = true;
                    //TODO On
                });
        }

        public List<CalendarTimestamp> TodayWeek()
        {
            var today = Today;
            var start = GetStartOfWeek(Today);
            var end = GetEndOfWeek(Today);

            return CalendarTimestampUtils.CreateDayList(start, end, today,
                WeekdaySkips(), ParsedWeekdays().Count, ParsedWeekdays().Count);
        }

        public bool IsOutside(CalendarTimestamp day)
        {
            var dayIdentifier = CalendarTimestampUtils.GetDayIdentifier(day);

            return dayIdentifier < CalendarTimestampUtils.GetDayIdentifier(ParsedStart()) ||
                dayIdentifier > CalendarTimestampUtils.GetDayIdentifier(ParsedEnd());
        }

        public int GetWeekNumber(CalendarTimestamp determineDay) =>
            CalendarDateTimeUtils.WeekNumber(determineDay.Year, determineDay.Month - 1,
                determineDay.Day, ParsedWeekdays()[0], LocaleFirstDayOfYear.ToInt32());

        public List<CalendarTimestamp> Day => Days;

        public new int WeekDays => ParsedWeekdays().Count;

        public string MonthFormatter(CalendarTimestamp tms, bool @short)
        {
            if (MonthFormat != null)
                return MonthFormat(tms, @short);

            var longOptions = new CalendarFormatterOptions { TimeZone = "UTC", Day = "long" };
            var shortOptions = new CalendarFormatterOptions { TimeZone = "UTC", Day = "short" };

            var nativeLocaleFormatter = CalendarTimestampUtils.CreateNativeLocaleFormatter(
                CurrentLocale, @short ? shortOptions : longOptions);

            return nativeLocaleFormatter(tms, @short);
        }
    }
}
