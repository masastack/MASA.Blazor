﻿namespace Masa.Blazor.Components.DatePicker;

public partial class MDatePickerMonthTable<TValue> : MDatePickerTable<TValue>
{
    public override Func<DateOnly, string> Formatter
    {
        get
        {
            if (Format != null)
            {
                return Format;
            }

            return DateFormatters.AbbreviatedMonth(Locale);
        }
    }

    protected override bool IsSelected(DateOnly value)
    {
        if (Value is DateOnly date)
        {
            return date.Year == value.Year && date.Month == value.Month;
        }

        if (Value is IList<DateOnly> dates)
        {
            return dates.Any(d => d.Year == value.Year && d.Month == value.Month);
        }

        return false;
    }

    protected override bool IsCurrent(DateOnly value)
    {
        return Current.Year == value.Year && Current.Month == value.Month;
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(new[]
        {
            "m-date-picker-table--month"
        });
    }
}