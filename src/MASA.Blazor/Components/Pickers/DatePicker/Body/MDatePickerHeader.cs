using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    internal class MDatePickerHeader : BDatePickerHeader, IThemeable
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public string Color { get; set; } = "accent";

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return Themeable != null && Themeable.IsDark;
            }
        } 

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-date-picker-header")
                        .AddIf("m-date-picker-header--disabled", () => Disabled)
                        .AddTheme(IsDark);
                })
                .Apply("value", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-date-picker-header__value")
                        .AddIf("m-date-picker-header__value--disabled", () => Disabled);
                })
                .Apply("header", cssBuilder =>
                {
                    cssBuilder
                        .AddTextColor(Color);
                });

            AbstractProvider
                .Apply<BButton, MButton>(props =>
                {
                    props[nameof(MButton.Dark)] = Dark;
                    props[nameof(MButton.Icon)] = true;
                })
                .Apply<BIcon, MIcon>();
        }
    }
}
