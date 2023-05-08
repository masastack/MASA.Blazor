namespace Masa.Blazor
{
    public class MDatePickerDateTable<TValue> : MDatePickerTable<TValue>, IDatePickerDateTable
    {
        [Parameter]
        public int FirstDayOfWeek { get; set; }

        [Parameter]
        public Func<DateOnly, string>? WeekdayFormat { get; set; }

        [Parameter]
        public bool ShowWeek { get; set; }

        [Parameter]
        public bool ShowAdjacentMonths { get; set; }

        [Parameter]
        public EventCallback<int> OnDaySelected { get; set; }

        public override Func<DateOnly, string> Formatter => Format ?? DateFormatters.Day(Locale);

        public Func<DateOnly, string>? WeekdayFormatter => WeekdayFormat ?? DateFormatters.ShortestDayOfWeek(Locale);

        public IEnumerable<string> WeekDays
        {
            get
            {
                var first = FirstDayOfWeek;

                var range = Enumerable.Range(0, 7);
                return WeekdayFormatter != null
                    ? range.Select(i => WeekdayFormatter(DateOnly.Parse($"2017-01-{first + i + 15}"))) // 2017-01-15 is Sunday
                    : range.Select(i => new[] { "S", "M", "T", "W", "T", "F", "S" }[(i + first) % 7]);
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
                })
                .Apply("events", cssBuilder => { cssBuilder.Add("m-date-picker-table__events"); })
                .Apply("event", cssBuilder =>
                {
                    var color = (string?)cssBuilder.Data;
                    cssBuilder.AddBackgroundColor(color);
                }, styleBuilder =>
                {
                    var color =  (string?)styleBuilder.Data;
                    styleBuilder.AddBackgroundColor(color);
                });

            AbstractProvider
                .ApplyDatePickerDateTableDefault();
        }
    }
}
