using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MTextField<TValue> : MInput<TValue>
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
        public string Type { get; set; } = "text";

        [Parameter]
        public RenderFragment PrependInnerContent { get; set; }

        protected bool ShowLabel => HasLabel && !(IsSingle && LabelValue);

        protected InputContext<TValue> InputContext { get; set; }

        protected bool HasLabel => Label != null;

        public bool IsSingle => IsSolo || IsSingleLine || FullWidth || (Filled && !HasLabel);

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
                        .AddTextColor(ValidationState);
                })
                .Merge("details", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-text-field__details");
                })
                .Merge("input-slot", cssBuilder =>
                {
                    cssBuilder
                        .AddBackgroundColor(BackgroundColor);
                });

            AbstractProvider
                .Apply<IInputBody, MInputBody<TValue>>(properties =>
                {
                    properties[nameof(MInputBody<TValue>.HasState)] = HasState;
                    properties[nameof(MInputBody<TValue>.ValidationState)] = ValidationState;
                    properties[nameof(MInputBody<TValue>.Label)] = Label;
                    properties[nameof(MInputBody<TValue>.LabelValue)] = LabelValue;
                    properties[nameof(MInputBody<TValue>.ShowLabel)] = ShowLabel;
                    properties[nameof(MInputBody<TValue>.IsFocused)] = IsFocused;
                    properties[nameof(MInputBody<TValue>.OnBlur)] = EventCallback.Factory.Create<FocusEventArgs>(this, () =>
                    {
                        IsActive = false;
                        IsFocused = false;
                    });
                    properties[nameof(MInputBody<TValue>.Value)] = (NullableValue<TValue>)Value;
                    properties[nameof(MInputBody<TValue>.InputContext)] = InputContext;
                    properties[nameof(MInputBody<TValue>.PlaceHolder)] = PersistentPlaceholder || IsFocused || !HasLabel ? Placeholder : "";
                    properties[nameof(MInputBody<TValue>.Outlined)] = Outlined;
                    properties[nameof(MInputBody<TValue>.Type)] = Type;
                    properties[nameof(MInputBody<TValue>.IsTextField)] = true;
                    properties[nameof(MInputBody<TValue>.Readonly)] = IsReadonly;
                    properties[nameof(MInputBody<TValue>.Disabled)] = IsDisabled;
                    properties[nameof(MInputBody<TValue>.PrependInnerContent)] = PrependInnerContent;
                });
        }

        protected override void OnParametersSet()
        {
            if (InputContext == null)
            {
                InputContext = new InputContext<TValue>();
                InputContext.OnValueChanged += InputContext_OnValueChanged;
            }

            base.OnParametersSet();
        }

        private async Task InputContext_OnValueChanged(NullableValue<TValue> value)
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }
            Value = value;
        }

        protected override async Task HandleClickAsync(MouseEventArgs args)
        {
            IsActive = true;
            IsFocused = true;

            await InputContext.InputRef.FocusAsync();
        }
    }
}
