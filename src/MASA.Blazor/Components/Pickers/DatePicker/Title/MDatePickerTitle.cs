using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    internal partial class MDatePickerTitle : BDatePickerTitle, IPickerTitle
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool SelectingYear { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-date-picker-title")
                        .AddIf("m-date-picker-title--disabled", () => Disabled);
                });

            AbstractProvider
                .Apply<BDatePickerTitleButton, MDatePickerTittleButton>("year-btn",props =>
                {
                    props[nameof(Class)] = "m-date-picker-title__year";
                    props[nameof(MDatePickerTittleButton.Active)] = SelectingYear;
                })
                .Apply<BDatePickerTitleButton, MDatePickerTittleButton>("title-date", props =>
                {
                    props[nameof(Class)] = "m-date-picker-title__date";
                    props[nameof(MDatePickerTittleButton.Active)] = !SelectingYear;
                })
                .Apply<BIcon, MIcon>(props =>
                {
                    props[nameof(MIcon.Dark)] = true;
                });
        }
    }
}
