﻿namespace Masa.Blazor
{
#if NET6_0
    public partial class MSwitch<TValue> : MSelectable<TValue>
#else
    public partial class MSwitch<TValue> : MSelectable<TValue> where TValue : notnull
#endif
    {
        [Parameter] public bool Flat { get; set; }

        [Parameter] public bool Inset { get; set; }

        [Parameter] public string? LeftText { get; set; }

        [Parameter]
        [MasaApiParameter(ReleasedOn = "v1.4.0")]
        public string? LeftIcon { get; set; }

        [Parameter] public string? RightText { get; set; }

        [Parameter]
        [MasaApiParameter(ReleasedOn = "v1.4.0")]
        public string? RightIcon { get; set; }

        [Parameter]
        public string? TrackColor { get; set; }

        public override bool HasColor => IsActive || HasText;
        
        public string? ComputedTrackColor => HasText ? TrackColor ?? ValidationState : null;

        // according to spec, should still show
        // a color when disabled and active
        public override string? ValidationState
        {
            get
            {
                if (HasError && ShouldValidate)
                {
                    return "error";
                }

                if (HasSuccess)
                {
                    return "success";
                }

                if (HasColor)
                {
                    return ComputedColor;
                }

                return null;
            }
        }

        public bool HasText => LeftText != null || LeftIcon != null || RightText != null || RightIcon != null;

        protected override void OnInternalValueChange(TValue val)
        {
            base.OnInternalValueChange(val);

            if (OnChange.HasDelegate)
            {
                OnChange.InvokeAsync(val);
            }
        }

        private static Block _block = new("m-input--switch");
        private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
        private static ModifierBuilder _trackModifierBuilder = _block.Element("track").CreateModifierBuilder();
        private static ModifierBuilder _thumbModifierBuilder = _block.Element("thumb").CreateModifierBuilder();
        

        protected override IEnumerable<string> BuildComponentClass()
        {
            return base.BuildComponentClass().Concat(
                new []
                {
                    _modifierBuilder.Add(Flat)
                        .Add(Inset)
                        .Add("text", HasText)
                        .Build()
                }
            );
        }
    }
}