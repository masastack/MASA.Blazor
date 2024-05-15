namespace Masa.Blazor;

public partial class MDatePickerDateTable<TValue> : MDatePickerTable<TValue>
{
    [Parameter] public int FirstDayOfWeek { get; set; }

    [Parameter] public Func<DateOnly, string>? WeekdayFormat { get; set; }

    [Parameter] public bool ShowWeek { get; set; }

    [Parameter] public bool ShowAdjacentMonths { get; set; }

    [Parameter] public EventCallback<int> OnDaySelected { get; set; }

    public override Func<DateOnly, string> Formatter => Format ?? DateFormatters.Day(Locale);

    public Func<DateOnly, string>? WeekdayFormatter => WeekdayFormat ?? DateFormatters.ShortestDayOfWeek(Locale);

    public IEnumerable<string> WeekDays
    {
        get
        {
            var first = FirstDayOfWeek;

            var range = Enumerable.Range(0, 7);
            return WeekdayFormatter != null
                ? range.Select(i =>
                    WeekdayFormatter(DateOnly.Parse($"2017-01-{first + i + 15}"))) // 2017-01-15 is Sunday
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
        var year = DisplayedYear;
        var month = DisplayedMonth + 1;
        var day = dayInMonth;

        var daysInMonth = DateTime.DaysInMonth(year, month);
        if (dayInMonth > daysInMonth)
        {
            if (month == 12)
            {
                year++;
                month = 1;
            }
            else
            {
                month++;
            }

            day = dayInMonth - daysInMonth;
        }

        var date = new DateTime(year, month, day);
        return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule, (DayOfWeek)FirstDayOfWeek);
    }

    private Block _dateBlock = new("m-date-picker-table--date");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(new[]
        {
            _dateBlock.Name
        });
    }
}