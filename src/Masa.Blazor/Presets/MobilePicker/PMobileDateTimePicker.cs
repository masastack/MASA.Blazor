namespace Masa.Blazor.Presets;

public class PMobileDateTimePicker : PMobilePicker<DateTimeColumn, DateTimeColumn, int>
{
    [Parameter]
    public DateTime Max { get; set; }

    [Parameter]
    public DateTime Min { get; set; }

    public override List<DateTimeColumn> Columns { get; set; } = new();
    public override Func<DateTimeColumn, string> ItemText { get; set; }
    public override Func<DateTimeColumn, int> ItemValue { get; set; }
    public override Func<DateTimeColumn, List<DateTimeColumn>> ItemChildren { get; set; }

    private DateTime _prevMax;
    private DateTime _prevMin;

    // TODO: UtcNow?
    private DateTime Now { get; init; }

    public PMobileDateTimePicker()
    {
        Now = DateTime.Now;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        ItemText = c => c.Text;
        ItemValue = c => c.Value;
        ItemChildren = c => c.Children;
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Min == default) Min = Now.AddYears(-10);
        if (Max == default) Max = Now.AddYears(11);

        var needChange = false;

        if (_prevMax != Max)
        {
            _prevMax = Max;
            needChange = true;
        }

        if (_prevMin != Min)
        {
            _prevMin = Min;
            needChange = true;
        }

        if (needChange)
        {
            GenItems();
        }
    }

    private void GenItems()
    {
        List<DateTimeColumn> yearList = new();

        var years = Enumerable.Range(Min.Year, Max.Year - Min.Year).ToArray();

        for (int i = 0; i < years.Length; i++)
        {
            var year = years[i];
            List<DateTimeColumn> months;

            if (i == 0)
            {
                months = GetMonths(year, Min.Month, 12);
            }
            else if (i == years.Length - 1)
            {
                months = GetMonths(year, 1, Max.Month);
            }
            else
            {
                months = GetMonths(year);
            }

            yearList.Add(new DateTimeColumn(year, months));
        }

        Columns = yearList;
    }

    private List<DateTimeColumn> GetMonths(int year, int from = 1, int to = 12)
    {
        List<DateTimeColumn> monthList = new();

        var count = to - from + 1;
        var months = Enumerable.Range(from, count).ToArray();

        for (int i = 0; i < months.Length; i++)
        {
            var month = months[i];
            List<DateTimeColumn> days;

            if (i == 0)
            {
                days = GetDays(year, month, Min.Day);
            }
            else if (i == months.Length - 1)
            {
                days = GetDays(year, month, 1, Max.Day);
            }
            else
            {
                days = GetDays(year, month);
            }

            monthList.Add(new DateTimeColumn(month, days));
        }

        return monthList;
    }

    private List<DateTimeColumn> GetDays(int year, int month, int from = 1, int to = 0)
    {
        if (to == 0)
        {
            to = DateTime.DaysInMonth(year, month);
        }

        var count = to - from + 1;
        return Enumerable.Range(from, count).Select(d =>  new DateTimeColumn(d)).ToList();
    }
}

public class DateTimeColumn
{
    public int Value { get; }

    public string Text => Value.ToString("00");

    public List<DateTimeColumn> Children { get; } = new();

    public DateTimeColumn(int value)
    {
        Value = value;
    }

    public DateTimeColumn(int value, List<DateTimeColumn> children) : this(value)
    {
        Children = children;
    }
}
