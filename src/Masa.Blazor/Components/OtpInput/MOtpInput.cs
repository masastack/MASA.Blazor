
namespace Masa.Blazor
{
    public class MOtpInput: BOtpInput, IThemeable
    {
        [Parameter]
        public string Color { get; set; }

        protected override void SetComponentClass()
        {
            var defaultClass = "m-otp-input";
            var prefix = "m-input";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(defaultClass)
                        .AddTheme(IsDark)
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
                        .AddTheme(IsDark)
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
