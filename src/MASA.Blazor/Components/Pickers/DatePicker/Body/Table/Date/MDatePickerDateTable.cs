using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    internal class MDatePickerDateTable : MDatePickerTable, IDatePickerDateTable
    {
        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public EventCallback<int> OnDaySelected { get; set; }

        [Parameter]
        public DateTime? Min { get; set; }

        [Parameter]
        public DateTime? Max { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-date-picker-table")
                        .Add("m-date-picker-table--date");
                });

            AbstractProvider
                .Apply<IDatePickerTableBody, MDatePickerDateTableBody>(props =>
                {
                    props[nameof(MDatePickerDateTableBody.Color)] = Color;
                    props[nameof(MDatePickerDateTableBody.Component)] = this;
                    props[nameof(MDatePickerDateTableBody.OnDaySelected)] = OnDaySelected;
                    props[nameof(MDatePickerDateTableBody.Min)] = Min;
                    props[nameof(MDatePickerDateTableBody.Max)] = Max;
                });
        }
    }
}
