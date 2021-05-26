using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MDatePickerDateTable : MDatePickerTable, IDatePickerDateTable
    {
        [Parameter]
        public EventCallback<int> OnDaySelected { get; set; }

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
                    props[nameof(MDatePickerDateTableBody.Component)] = this;
                    props[nameof(MDatePickerDateTableBody.OnDaySelected)] = OnDaySelected;
                });
        }
    }
}
