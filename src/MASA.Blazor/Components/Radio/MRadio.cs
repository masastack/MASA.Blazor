using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Blazor
{
    public partial class MRadio<TValue> : BRadio<TValue>
    {
        [CascadingParameter]
        public MRadioGroup<TValue> RadioGroup { get; set; }

        protected bool IsFocused { get; set; }

        [Parameter]
        public string Color { get; set; } = "primary";
        

        protected string ValidationState => RadioGroup?.ValidationState ?? "primary";

        protected override void OnInitialized()
        {
            RadioGroup?.AddRadio(this);
        }

        protected override void SetComponentClass()
        {
            var prefix = "m-radio";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--is-disabled", () => IsDisabled || (RadioGroup != null && RadioGroup.IsDisabled))
                        .AddIf($"{prefix}--is-focused", () => IsFocused)
                        .AddTheme(IsDark);
                })
                .Apply("radio", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--selection-controls__input");
                })
                .Apply("ripple", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--selection-controls__ripple")
                        .AddTextColor(Color, () => IsActive);
                });

            AbstractProvider
                .Apply<BIcon, MIcon>(attrs =>
                {
                    attrs[nameof(MIcon.Color)] = Color;
                    attrs[nameof(MIcon.IsActive)] = IsActive;
                })
                .Apply<BLabel, MLabel>();

            OnIcon = "mdi-radiobox-marked";
            OffIcon = "mdi-radiobox-blank";
        }
    }
}
