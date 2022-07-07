using BlazorComponent.Web;
using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor
{
    public partial class MCheckbox : MSelectable, IThemeable, ICheckbox
    {
        [Parameter]
        public bool Indeterminate { get; set; }

        [Parameter]
        public string IndeterminateIcon { get; set; } = "mdi-minus-box";

        [Parameter]
        public string OnIcon { get; set; } = "mdi-checkbox-marked";

        [Parameter]
        public string OffIcon { get; set; } = "mdi-checkbox-blank-outline";

        public override int DebounceMilliseconds { get; set; }

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

        [Inject]
        public Document Document { get; set; }

        protected override void OnInternalValueChange(bool val)
        {
            base.OnInternalValueChange(val);

            if (OnChange.HasDelegate)
            {
                OnChange.InvokeAsync(val);
            }
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
    }
}