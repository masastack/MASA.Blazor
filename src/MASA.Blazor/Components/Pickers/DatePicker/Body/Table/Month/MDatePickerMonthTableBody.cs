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
            SlotProvider
                .Apply<BButton, MButton>(props =>
                {
                    props[nameof(MButton.Text)] = true;
                    props[nameof(MButton.Color)] = "accent";
                });
        }
    }
}
