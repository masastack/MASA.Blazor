using Masa.Blazor.Extensions;

namespace Masa.Blazor
{
#if NET6_0
    public partial class MCheckbox<TValue> : MSelectable<TValue>, IThemeable
#else
    public partial class MCheckbox<TValue> : MSelectable<TValue>, IThemeable where TValue : notnull
#endif
    {
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

        private static Block _block = new("m-input--checkbox");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return base.BuildComponentClass().Concat(
                new[]
                {
                    _block.Name,
                    Indeterminate ? "m-input--indeterminate" : ""
                }
            );
        }
    }
}