
namespace Masa.Blazor
{
    public class MOtpInput: BOtpInput, IThemeable
    {
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter]
        public string? Color { get; set; }

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
            var defaultClass = "m-otp-input";
            var prefix = "m-input";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(defaultClass)
                        .AddTheme(IsDark, IndependentTheme)
                        .AddTextColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddTextColor(Color);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddTheme(IsDark, IndependentTheme)
                        .Add("m-text-field")
                        .Add("m-text-field--is-booted")
                        .AddIf("m-otp-input--plain", ()=> Plain)
                        .AddIf("m-text-field--outlined", ()=> !Plain);
                })
                .Apply("control", cssBuilder =>
                {
                    cssBuilder.Add($"{prefix}__control");
                })
                .Apply("slot", cssBuilder =>
                {
                    cssBuilder.Add($"{prefix}__slot");
                })
                .Apply("fieldSlot", cssBuilder =>
                {
                    cssBuilder
                        .Add($"m-text-field__slot");
                });

            AbstractProvider.Apply(typeof(BOtpInputSlot<>), typeof(BOtpInputSlot<MOtpInput>));
        }

    }
}
