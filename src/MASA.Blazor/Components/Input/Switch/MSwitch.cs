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
    public partial class MSwitch : MInput<bool>
    {
        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Inset { get; set; }

        protected override bool IsDirty => Value;

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            var prefix = "m-input";
            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}--selection-controls")
                        .Add($"{prefix}--switch")
                        .AddIf($"{prefix}--switch--flat", () => Flat)
                        .AddIf($"{prefix}--switch--inset", () => Inset)
                        .AddTextColor(ValidationState, () => IsActive);
                });

            AbstractProvider
                .Apply<IInputBody, MSwitchInputBody>(props =>
                {
                    props[nameof(MSwitchInputBody.ValidationState)] = ValidationState;
                    props[nameof(MSwitchInputBody.IsActive)] = IsActive;
                    props[nameof(MSwitchInputBody.Label)] = Label;
                });
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            IsActive = Value;
            HideDetails = "auto";
        }

        protected override async Task HandleClickAsync(MouseEventArgs args)
        {
            IsActive = !IsActive;

            Value = IsActive;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
        }
    }
}
