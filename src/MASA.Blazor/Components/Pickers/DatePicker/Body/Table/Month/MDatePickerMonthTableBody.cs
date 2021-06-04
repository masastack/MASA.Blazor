using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MDatePickerMonthTableBody : BDatePickerMonthTableBody, IDatePickerTableBody
    {
        protected override void SetComponentClass()
        {
            AbstractProvider
                .Apply<BButton, MDatePickerTableButton>(props =>
                {
                    props[nameof(MButton.Text)] = true;
                    props[nameof(MButton.Color)] = Color;
                });
        }
    }
}
