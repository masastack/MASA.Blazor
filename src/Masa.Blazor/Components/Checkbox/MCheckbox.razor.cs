namespace Masa.Blazor
{
#if NET6_0
    public partial class MCheckbox<TValue> : MSelectable<TValue>, IThemeable
#else
    public partial class MCheckbox<TValue> : MSelectable<TValue>, IThemeable where TValue : notnull
#endif
    {
        [Inject] public Document Document { get; set; } = null!;

        [Parameter] public bool Indeterminate { get; set; }

        [Parameter] public string IndeterminateIcon { get; set; } = "$checkboxIndeterminate";

        [Parameter] public string OnIcon { get; set; } = "$checkboxOn";

        [Parameter] public string OffIcon { get; set; } = "$checkboxOff";

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

        public override string? ValidationState
        {
            get
            {
                if (IsDisabled && !Indeterminate) return null;
                if (HasError && ShouldValidate) return "error";
                if (HasSuccess) return "success";
                if (HasColor) return ComputedColor;
                return null;
            }
        }

        private Block _block = new("m-input--checkbox");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return base.BuildComponentClass().Concat(
                _block.AddClass("m-input--indeterminate", Indeterminate)
                    .GenerateCssClasses()
            );
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