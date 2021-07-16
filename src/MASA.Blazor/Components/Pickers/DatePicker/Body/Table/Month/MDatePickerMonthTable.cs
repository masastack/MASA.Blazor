using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    internal partial class MDatePickerMonthTable : MDatePickerTable, IDatePickerMonthTable
    {
        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public EventCallback<int> OnMonthSelected { get; set; }

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
                .Apply<IDatePickerTableBody, MDatePickerMonthTableBody>(props =>
                {
                    props[nameof(MDatePickerMonthTableBody.Color)] = Color;
                    props[nameof(MDatePickerMonthTableBody.Component)] = this;
                    props[nameof(MDatePickerMonthTableBody.OnMonthSelected)] = OnMonthSelected;
                });
        }
    }
}
