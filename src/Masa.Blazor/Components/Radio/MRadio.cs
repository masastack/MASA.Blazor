namespace Masa.Blazor
{
    public class MRadio<TValue> : BRadio<TValue>
    {
        [Parameter]
        public string? Color { get; set; } = "primary";

        protected bool IsFocused { get; set; }

        protected string ValidationState => RadioGroup?.ValidationState ?? "primary";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            RadioGroup?.AddRadio(this);
        }

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
                        .AddTheme(IsDark);
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
                    attrs[nameof(MIcon.Color)] = Color;
                    attrs[nameof(MIcon.IsActive)] = IsActive;
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
