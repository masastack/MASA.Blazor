using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MDatePickerMonthTable<TValue> : MDatePickerTable<TValue>, IDatePickerMonthTable, IDatePickerTable
    {
        public override Func<DateOnly, string> Formatter
        {
            get
            {
                if (Format != null)
                {
                    return Format;
                }

                return value => DatePickerFormatter.Month(value.Month)[..3];
            }
        }

        protected override bool IsSelected(DateOnly value)
        {
            if (Value is DateOnly date)
            {
                return date.Year == value.Year && date.Month == value.Month;
            }
            else if (Value is IList<DateOnly> dates)
            {
                return dates.Any(date => date.Year == value.Year && date.Month == value.Month);
            }

            return false;
        }

        protected override bool IsCurrent(DateOnly value)
        {
            return Current.Year == value.Year && Current.Month == value.Month;
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-date-picker-table")
                        .Add("m-date-picker-table--month");
                });

            AbstractProvider
                .ApplyDatePickerMonthTableDefault();
        }
    }
}
