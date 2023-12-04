namespace Masa.Blazor
{
    public partial class MMessages : BMessages, IThemeable
    {
        [Parameter]
        public string? Color { get; set; }

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
            var prefix = "m-messages";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddTheme(IsDark, IndependentTheme)
                        .AddTextColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddTextColor(Color);
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__wrapper");
                })
                .Apply("message", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__message");
                });

            AbstractProvider
                .Apply(typeof(BMessagesChildren<>), typeof(BMessagesChildren<MMessages>))
                .Apply(typeof(BMessagesMessage<>), typeof(BMessagesMessage<MMessages>));
        }
    }
}
