using BlazorComponent.Web;
using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor
{
#if NET6_0
    public partial class MCheckbox<TValue> : MSelectable<TValue>, IThemeable, ICheckbox<TValue>
#else
    public partial class MCheckbox<TValue> : MSelectable<TValue>, IThemeable, ICheckbox<TValue> where TValue : notnull
#endif
    {
        [Inject]
        public Document Document { get; set; } = null!;

        [Parameter]
        public bool Indeterminate { get; set; }

        [Parameter]
        public string IndeterminateIcon { get; set; } = "$checkboxIndeterminate";

        [Parameter]
        public string OnIcon { get; set; } = "$checkboxOn";

        [Parameter]
        public string OffIcon { get; set; } = "$checkboxOff";

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

        protected override void OnInternalValueChange(TValue val)
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
                .Merge(typeof(BInputDefaultSlot<,>), typeof(BCheckboxDefaultSlot<TValue>))
                .Apply(typeof(BCheckboxCheckbox<,>), typeof(BCheckboxCheckbox<MCheckbox<TValue>, TValue>))
                .Apply(typeof(BSelectableInput<,>), typeof(BSelectableInput<MCheckbox<TValue>, TValue>))
                .Apply(typeof(BRippleableRipple<>), typeof(BRippleableRipple<MCheckbox<TValue>>))
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
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                //It's used to prevent ripple directive,and we may remove this 
                var inputSlot = Document.GetElementByReference(InputSlotElement);
                await inputSlot!.AddEventListenerAsync("mousedown", EventCallback.Empty, stopPropagation: true);
                await inputSlot.AddEventListenerAsync("mouseup", EventCallback.Empty, stopPropagation: true);
            }
        }
    }
}
