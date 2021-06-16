using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

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

        protected InputContext InputContext { get; set; }

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
                })
                .Merge("details", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-text-field__details");
                });

            AbstractProvider
                .Apply<IInputBody, MInputBody>(properties =>
                {
                    properties[nameof(MInputBody.Label)] = Label;
                    properties[nameof(MInputBody.ShowLabel)] = ShowLabel;
                    properties[nameof(MInputBody.IsActive)] = IsActive || !string.IsNullOrEmpty(Value);
                    properties[nameof(MInputBody.IsFocused)] = IsFocused;
                    properties[nameof(MInputBody.OnBlur)] = EventCallback.Factory.Create<FocusEventArgs>(this, () =>
                    {
                        IsActive = false;
                        IsFocused = false;
                    });
                    properties[nameof(MInputBody.Value)] = Value;
                    properties[nameof(MInputBody.InputContext)] = InputContext;
                    properties[nameof(MInputBody.PlaceHolder)] = IsFocused ? Placeholder : "";
                    properties[nameof(MInputBody.Outlined)] = Outlined;
                    properties[nameof(MInputBody.Type)] = Type;
                    properties[nameof(MInputBody.IsTextField)] = true;
                    properties[nameof(MInputBody.Readonly)] = IsReadonly;
                    properties[nameof(MInputBody.Disabled)] = IsDisabled;
                });
            ValidationState = "error";
        }

        protected override void OnParametersSet()
        {
            if (InputContext == null)
            {
                InputContext = new InputContext();
                InputContext.OnValueChanged += InputContext_OnValueChanged;
            }

            base.OnParametersSet();
        }

        private void InputContext_OnValueChanged(string value)
        {
            Value = value;
            if (ValueChanged.HasDelegate)
            {
                ValueChanged.InvokeAsync(Value);
            }
        }

        protected override async Task HandleClickAsync(MouseEventArgs args)
        {
            IsActive = true;
            IsFocused = true;

            await InputContext.InputRef.FocusAsync();
        }
    }
}
