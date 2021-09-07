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
    //TODO:该组件需要进一步完善
    public partial class MSwitch : MInput<bool>, ISwitch
    {
        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Inset { get; set; }

        [Parameter]
        public EventCallback<bool> OnChange { get; set; }

        public override bool IsDirty => Value;

        public Dictionary<string, object> InputAttrs { get; set; } = new();

        public bool IsActive { get; set; }

        public bool? Ripple { get; set; }

        public override bool HasColor => Value;

        public Task HandleOnBlur(FocusEventArgs args)
        {
            return Task.CompletedTask;
        }

        public Task HandleOnChange(ChangeEventArgs args)
        {
            return Task.CompletedTask;
        }

        public Task HandleOnFocus(FocusEventArgs args)
        {
            return Task.CompletedTask;
        }

        public Task HandleOnKeyDown(KeyboardEventArgs args)
        {
            return Task.CompletedTask;
        }

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
                        .AddIf($"{prefix}--switch--inset", () => Inset);
                })
                .Apply("switch", cssBuilder =>
                 {
                     cssBuilder
                         .Add("m-input--selection-controls__input");
                 })
                .Apply("ripple", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--selection-controls__ripple")
                        .AddTextColor(ValidationState);
                })
                .Apply("track", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--switch__track")
                        .AddTextColor(ValidationState)
                        .AddTheme(IsDark);
                })
                .Apply("thumb", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--switch__thumb")
                        .AddTextColor(ValidationState)
                        .AddTheme(IsDark);
                });

            AbstractProvider
                .Merge(typeof(BInputDefaultSlot<,>), typeof(BSwitchDefaultSlot))
                .Apply(typeof(BSwitchSwitch<>), typeof(BSwitchSwitch<MSwitch>))
                .Apply(typeof(BSelectableInput<>), typeof(BSelectableInput<MSwitch>))
                .Apply(typeof(BRippleableRipple<>), typeof(BRippleableRipple<MSwitch>))
                .Apply(typeof(BSwitchProgress<>), typeof(BSwitchProgress<MSwitch>));
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            HideDetails = "auto";
        }

        public override async Task HandleOnClickAsync(MouseEventArgs args)
        {
            Value = !Value;
            if (OnChange.HasDelegate)
            {
                await OnChange.InvokeAsync(Value);
            }
            else
            {
                if (ValueChanged.HasDelegate)
                {
                    await ValueChanged.InvokeAsync(Value);
                }
            }
        }
    }
}
