using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    internal partial class MDatePickerTable : BDatePickerTable, IThemeable
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                       .AddIf("m-date-picker-table--disabled", () => Disabled)
                       .AddTheme(Dark);
                });
        }
    }
}
