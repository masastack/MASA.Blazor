using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        protected bool Enclosed => Filled || IsSolo || Outlined;

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

        protected bool ShowLabel => (!string.IsNullOrEmpty(Label) && !IsSolo) || (string.IsNullOrEmpty(Value) && IsSolo && !IsFocused);

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
                        .AddIf("primary--text", () => IsActive);
                });

            AbstractProvider
                .Apply<IInputBody, MInputBody>(properties =>
                {
                    properties[nameof(MInputBody.Label)] = Label;
                    properties[nameof(MInputBody.ShowLabel)] = ShowLabel;
                    properties[nameof(MInputBody.IsActive)] = IsActive || !string.IsNullOrEmpty(Value);
                    properties[nameof(MInputBody.OnBlur)] = EventCallback.Factory.Create<FocusEventArgs>(this, () =>
                    {
                        IsActive = false;
                        IsFocused = false;
                    });
                    properties[nameof(MInputBody.Value)] = Value;
                    properties[nameof(MInputBody.ValueChanged)] = ValueChanged;
                    properties[nameof(MInputBody.PlaceHolder)] = IsFocused ? Placeholder : "";
                    properties[nameof(MInputBody.Outlined)] = Outlined;
                    properties[nameof(MInputBody.Type)] = Type;
                    properties[nameof(MInputBody.IsTextField)] = true;
                });
            ValidationState = "error";
        }

        protected override Task HandleClick(MouseEventArgs args)
        {
            IsActive = true;
            IsFocused = true;

            return Task.CompletedTask;
        }
    }
}
