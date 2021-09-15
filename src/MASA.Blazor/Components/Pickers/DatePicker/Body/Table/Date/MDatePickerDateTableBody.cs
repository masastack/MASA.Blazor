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
    internal partial class MDatePickerDateTableBody : BDatePickerDateTableBody
    {

        protected override void SetComponentClass()
        {
            AbstractProvider
                .Apply<BButton, MDatePickerTableButton>(props =>
                {
                    props[nameof(MButton.Text)] = true;
                    props[nameof(MButton.Fab)] = true;
                    props[nameof(MButton.Default)] = false;
                    props[nameof(MButton.Color)] = Color;
                })
                .Apply<BButton, MDatePickerTableButton>("current-btn", props =>
                {
                    props[nameof(Class)] = "m-date-picker-table__current";
                    props[nameof(MButton.Outlined)] = true;
                    props[nameof(MButton.Fab)] = true;
                    props[nameof(MButton.Default)] = false;
                    props[nameof(MButton.Color)] = Color;
                    props[nameof(MDatePickerTableButton.IsCurrent)] = true;
                });
        }
    }
}
