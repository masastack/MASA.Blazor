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

        protected override void SetComponentClass()
        {
            SlotProvider
                .Apply<BInputSlot, MSwitchInputSlot>(props =>
                {
                    props[nameof(MSwitchInputSlot.ValidationState)] = ValidationState;
                    props[nameof(MSwitchInputSlot.IsActive)] = IsActive ?? false;
                    props[nameof(MSwitchInputSlot.Label)] = Label;
                });

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
                        .AddIf($"{ValidationState}--text", () => IsActive ?? false);
                });

            IsActive = false;
        }

        protected override async Task HandleClick(MouseEventArgs args)
        {
            IsActive = !IsActive;

            Value = IsActive ?? false;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
        }
    }
}
