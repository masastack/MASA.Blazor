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
    public partial class MRadio<TValue> : BRadio<TValue>
    {
        [CascadingParameter]
        public BInput Input { get; set; }

        protected MRadioGroup<TValue> RadioGroup => Input as MRadioGroup<TValue>;

        protected bool IsFocused { get; set; }

        [Parameter]
        public bool Dark { get; set; }

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
                        .AddTheme(Dark);
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
                        .AddTextColor(ValidationState, () => IsActive);
                });

            SlotProvider
                .Apply<BIcon, MIcon>(props =>
                {
                    props[nameof(MIcon.Color)] = ValidationState;
                    props[nameof(MIcon.IsActive)] = IsActive;
                })
                .Apply<BLabel, MLabel>();

            OnIcon = "mdi-radiobox-marked";
            OffIcon = "mdi-radiobox-blank";
        }
    }
}
