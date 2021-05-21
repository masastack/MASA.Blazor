using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MDatePickerTittleButton:BDatePickerTitleButton
    {
        [Parameter]
        public bool Active { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-picker__title__btn")
                        .AddIf("m-picker__title__btn--active", () => Active)
                        .AddIf("m-picker__title__btn--readonly", () => Readonly);
                });
        }
    }
}
