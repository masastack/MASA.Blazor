namespace Masa.Blazor
{
#if NET6_0
    public partial class MSwitch<TValue> : MSelectable<TValue>, ISwitch<TValue>
#else
    public partial class MSwitch<TValue> : MSelectable<TValue>, ISwitch<TValue> where TValue : notnull
#endif
    {
        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Inset { get; set; }

        [Parameter]
        public string? LeftText { get; set; }

        [Parameter]
        public string? RightText { get; set; }

        [Parameter]
        public string? TrackColor { get; set; }

        // according to spec, should still show
        // a color when disabled and active
        public override string ValidationState
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

                return "";
            }
        }

        public bool HasText => LeftText != null || RightText != null;

        public new string? TextColor => HasText ? ComputedColor : (IsLoading ? null : ValidationState);

        protected override void OnInternalValueChange(TValue val)
        {
            base.OnInternalValueChange(val);

            if (OnChange.HasDelegate)
            {
                OnChange.InvokeAsync(val);
            }
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            var prefix = "m-input";
            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}--selection-controls")
                        .Add($"{prefix}--switch")
                        .AddIf($"{prefix}--switch--flat", () => Flat)
                        .AddIf($"{prefix}--switch--inset", () => Inset)
                        .AddIf($"{prefix}--switch--text", () => HasText);
                })
                .Apply("switch", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--selection-controls__input");
                })
                .Apply("ripple", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--selection-controls__ripple")
                        .AddTextColor(ValidationState);
                })
                .Apply("track", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--switch__track")
                        .AddTextColor(TrackColor ?? TextColor)
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddTextColor(TrackColor ?? TextColor);
                })
                .Apply("thumb", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--switch__thumb")
                        .AddTextColor(TextColor)
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddTextColor(TrackColor ?? TextColor);
                })
                .Apply("left", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--switch__left");
                })
                .Apply("right", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--switch__right");
                });

            AbstractProvider
                .Merge(typeof(BInputDefaultSlot<,>), typeof(BSwitchDefaultSlot<TValue>))
                .Apply(typeof(BSwitchSwitch<,>), typeof(BSwitchSwitch<MSwitch<TValue>, TValue>))
                .Apply(typeof(BSelectableInput<,>), typeof(BSelectableInput<MSwitch<TValue>, TValue>))
                .Apply(typeof(BRippleableRipple<>), typeof(BRippleableRipple<MSwitch<TValue>>))
                .Apply<BProgressCircular, MProgressCircular>(attrs =>
                {
                    if (!IsLoading) return;

                    string? color = null;

                    Loading.Match(
                        s => color = s,
                        b => b ? color = Color ?? "primary" : null
                    );

                    if (color != null && string.IsNullOrWhiteSpace(color))
                    {
                        color = Color ?? "primary";
                    }

                    attrs[nameof(MProgressCircular.Color)] = color;
                    attrs[nameof(MProgressCircular.Indeterminate)] = true;
                    attrs[nameof(MProgressCircular.Size)] = (StringNumber)16;
                    attrs[nameof(MProgressCircular.Width)] = (StringNumber)2;
                })
                .Apply(typeof(BSwitchProgress<,>), typeof(BSwitchProgress<MSwitch<TValue>, TValue>));
        }
    }
}
