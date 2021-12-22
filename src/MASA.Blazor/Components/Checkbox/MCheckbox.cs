using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorComponent;
using BlazorComponent.Web;
using MASA.Blazor.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    public partial class MCheckbox : MInput<bool>, IThemeable, ICheckbox
    {
        [Parameter]
        public bool Indeterminate { get; set; }

        [Parameter]
        public string IndeterminateIcon { get; set; } = "mdi-minus-box";

        [Parameter]
        public string OnIcon { get; set; } = "mdi-checkbox-marked";

        [Parameter]
        public string OffIcon { get; set; } = "mdi-checkbox-blank-outline";

        public override bool HasColor => InternalValue;

        public string ComputedIcon
        {
            get
            {
                if (Indeterminate)
                {
                    return IndeterminateIcon;
                }

                if (IsActive)
                {
                    return OnIcon;
                }

                return OffIcon;
            }
        }

        public Dictionary<string, object> InputAttrs => new();

        private bool? _isActive;

        [Parameter]
        public bool IsActive
        {
            get => _isActive ?? Value;
            set
            {
                _isActive = value;
                InternalValue = value;
            }
        }

        [Parameter]
        public bool? Ripple { get; set; }

        [Parameter]
        public EventCallback<bool> OnChange { get; set; }

        [Inject]
        public Document Document { get; set; }

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

        public override string ValidationState
        {
            get
            {
                if (IsDisabled && !Indeterminate) return "";
                if (HasError && ShouldValidate) return "error";
                if (HasSuccess) return "success";
                if (HasColor) return ComputedColor;
                return "";
            }
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--selection-controls")
                        .Add("m-input--checkbox")
                        .AddIf("m-input--indeterminate", () => Indeterminate);
                })
                .Apply("checkbox-input", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--selection-controls__input");
                })
                .Apply("ripple", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--selection-controls__ripple")
                        .AddTextColor(ValidationState);
                });

            AbstractProvider
                .Merge(typeof(BInputDefaultSlot<,>), typeof(BCheckboxDefaultSlot<MCheckbox>))
                .Apply(typeof(BCheckboxCheckbox), typeof(BCheckboxCheckbox))
                .Apply(typeof(BSelectableInput<>), typeof(BSelectableInput<MCheckbox>))
                .Apply(typeof(BRippleableRipple<>), typeof(BRippleableRipple<MCheckbox>))
                .Apply(typeof(BIcon), typeof(MIcon), attrs =>
                {
                    attrs[nameof(MIcon.Dense)] = Dense;
                    attrs[nameof(MIcon.Dark)] = Dark;
                    attrs[nameof(MIcon.Light)] = Light;
                    attrs[nameof(MIcon.Color)] = ValidationState;
                });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //It's used to prevent ripple directive,and we may remove this 
                var inputSlot = Document.GetElementByReference(InputSlotElement);
                await inputSlot.AddEventListenerAsync("mousedown", EventCallback.Empty, stopPropagation: true);
                await inputSlot.AddEventListenerAsync("mouseup", EventCallback.Empty, stopPropagation: true);
            }
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