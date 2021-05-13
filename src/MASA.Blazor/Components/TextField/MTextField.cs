using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    public partial class MTextField : MInput<string>
    {
        [Parameter]
        public bool FullWidth { get; set; }

        [Parameter]
        public bool Prefix { get; set; }

        [Parameter]
        public bool IsSingleLine { get; set; }

        [Parameter]
        public bool IsSolo { get; set; }

        [Parameter]
        public bool SoloInverted { get; set; }

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Filled { get; set; }

        [Parameter]
        public bool IsBooted { get; set; } = true;

        public bool Enclosed => Filled || IsSolo || Outlined;

        [Parameter]
        public bool Outlined { get; set; }
        [Parameter]
        public bool Reverse { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public bool Rounded { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public string Type { get; set; }

        [Parameter]
        public EventCallback<ValidatorArgs> Validator { get; set; }

        public bool ShowLabel => (!string.IsNullOrEmpty(Label) && !IsSolo) || (string.IsNullOrEmpty(Value) && IsSolo && !IsFocused);

        protected bool Active { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            var prefix = "m-text-field";
            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--full-width", () => FullWidth)
                        .AddIf($"{prefix}--prefix", () => Prefix)
                        .AddIf($"{prefix}--single-line", () => IsSingleLine)
                        .AddIf($"{prefix}--solo", () => IsSolo)
                        .AddIf($"{prefix}--solo-inverted", () => SoloInverted)
                        .AddIf($"{prefix}--solo-flat", () => Flat)
                        .AddIf($"{prefix}--filled", () => Filled)
                        .AddIf($"{prefix}--is-booted", () => IsBooted)
                        .AddIf($"{prefix}--enclosed", () => Enclosed)
                        .AddIf($"{prefix}--reverse", () => Reverse)
                        .AddIf($"{prefix}--outlined", () => Outlined)
                        .AddIf($"{prefix}--placeholder", () => !string.IsNullOrEmpty(Placeholder))
                        .AddIf($"{prefix}--rounded", () => Rounded)
                        .AddIf($"{prefix}--shaped", () => Shaped)
                        .AddIf("primary--text", () => Active);
                });

            SlotProvider
                .Apply<BInputSlot, MInputSlot>(properties =>
                {
                    properties[nameof(MInputSlot.Label)] = Label;
                    properties[nameof(MInputSlot.ShowLabel)] = ShowLabel;
                    properties[nameof(MInputSlot.Active)] = Active || !string.IsNullOrEmpty(Value);
                    properties[nameof(MInputSlot.HandleBlur)] = EventCallback.Factory.Create(this, async () =>
                     {
                         Active = false;
                         IsFocused = false;

                         if (Validator.HasDelegate)
                         {
                             Messages = new List<string>();
                             await Validator.InvokeAsync(new ValidatorArgs(Value, Messages));
                         }
                     });
                    properties[nameof(MInputSlot.Value)] = Value;
                    properties[nameof(MInputSlot.ValueChanged)] = EventCallback.Factory.Create<string>(this, async value =>
                     {
                         Value = value;

                         if (ValueChanged.HasDelegate)
                         {
                             await ValueChanged.InvokeAsync(value);
                         }
                     });
                    properties[nameof(MInputSlot.PlaceHolder)] = IsFocused ? Placeholder : "";
                    properties[nameof(MInputSlot.Outlined)] = Outlined;
                    properties[nameof(MInputSlot.Type)] = Type;
                    properties[nameof(MInputSlot.IsTextField)] = true;
                });
            ValidationState = "error";
        }

        protected override Task HandleClick(MouseEventArgs args)
        {
            Active = true;
            IsFocused = true;

            return Task.CompletedTask;
        }
    }
}
