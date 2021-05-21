using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MDatePicker : MPicker, IThemeable, IElevatable, IColorable
    {
        private DateTime _tableDate;
        private DateTime _value;

        [Parameter]
        public string HeaderColor { get; set; }

        [Parameter]
        public override string Color
        {
            get
            {
                return HeaderColor ?? base.Color;
            }
            set
            {
                base.Color = value;
            }
        }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public string YearIcon { get; set; }

        [Parameter]
        public StringNumber MinYear { get; set; }

        [Parameter]
        public StringNumber MaxYear { get; set; }

        [Parameter]
        public StringNumber MinMonth { get; set; }

        [Parameter]
        public StringNumber MaxMonth { get; set; }

        public DateTime TableDate
        {
            get
            {
                if (_tableDate == default)
                {
                    return Value;
                }

                return _tableDate;
            }
            set
            {
                _tableDate = value;
            }
        }

        public StringNumber TableYear => TableDate.Year;

        public StringNumber TableMonth => TableDate.Month;

        [Parameter]
        public DateTime Value
        {
            get
            {
                if (_value == default)
                {
                    _value = DateTime.Now.Date;
                }

                return _value;
            }
            set
            {
                _value = value;
            }
        }

        [Parameter]
        public EventCallback<DateTime> ValueChanged { get; set; }

        [Parameter]
        public string NextIcon { get; set; } = "mdi-chevron-right";

        [Parameter]
        public string PrevIcon { get; set; } = "mdi-chevron-left";

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-picker--date");
                });

            SlotProvider
                .Apply<IPickerTitle, MDatePickerTitle>(props =>
                {
                    props[nameof(MDatePickerTitle.Year)] = (StringNumber)Value.Year;
                    props[nameof(MDatePickerTitle.Date)] = Value.ToString("MM-dd");
                })
                .Apply<IPickerBody, MDatePickerBody>(props =>
                {
                    props[nameof(MDatePickerBody.Component)] = this;
                    props[nameof(MDatePickerBody.DateClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, () =>
                    {
                        if (Type == "date")
                        {
                            Type = "month";
                        }
                        else if (Type == "month")
                        {
                            Type = "year";
                        }
                    });
                    props[nameof(MDatePickerBody.OnDaySelected)] = EventCallback.Factory.Create<int>(this, async day =>
                     {
                         Value = new DateTime(TableDate.Year, TableDate.Month, day);
                         if (ValueChanged.HasDelegate)
                         {
                             await ValueChanged.InvokeAsync(Value);
                         }
                     });
                    props[nameof(MDatePickerBody.OnMonthSelected)] = EventCallback.Factory.Create<int>(this, month =>
                    {
                        TableDate = new DateTime(TableDate.Year, month, TableDate.Day);
                        Type = "date";
                    });
                    props[nameof(MDatePickerBody.OnYearSelected)] = EventCallback.Factory.Create<int>(this, year =>
                    {
                        TableDate = new DateTime(year, TableDate.Month, TableDate.Day);
                        Type = "month";
                    });
                });
        }

        public void AddMonths(int month)
        {
            TableDate = TableDate.AddMonths(month);
        }

        public void AddYears(int year)
        {
            TableDate = TableDate.AddYears(year);
        }
    }
}
