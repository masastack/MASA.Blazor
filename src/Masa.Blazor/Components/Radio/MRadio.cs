namespace Masa.Blazor
{
    public class MRadio<TValue> : BRadio<TValue>
    {
        [Parameter]
        public string? Color { get; set; }

        protected bool IsFocused { get; set; }

        private string? ComputedColor
        {
            get
            {
                if (IsDisabled || !IsActive)
                {
                    return null;
                }

                if (!string.IsNullOrWhiteSpace(Color))
                {
                    return Color;
                }

                if (IsDark)
                {
                    return "white";
                }

                return "primary";
            }
        }

        private string? ValidationState => RadioGroup?.ValidationState ?? ComputedColor;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            RadioGroup?.AddRadio(this);
        }

        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif

        protected override void SetComponentClass()
        {
            var prefix = "m-radio";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--is-disabled", () => Disabled || InputIsDisabled)
                        .AddIf($"{prefix}--is-focused", () => IsFocused)
                        .AddTheme(IsDark, IndependentTheme);
                })
                .Apply("radio", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--selection-controls__input");
                })
                .Apply("ripple", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--selection-controls__ripple")
                        .AddTextColor(Color, () => IsActive);
                });

            AbstractProvider
                .Apply<BIcon, MIcon>(attrs =>
                {
                    attrs[nameof(MIcon.Dense)] = RadioGroup?.Dense is true;
                    attrs[nameof(MIcon.Color)] = ValidationState;
                })
                .Apply<BLabel, MLabel>(attrs =>
                {
                    attrs[nameof(MLabel.Attributes)] = new Dictionary<string, object>()
                    {
                        { "__internal_preventDefault_onclick", true }
                    };
                });
        }
    }
}
