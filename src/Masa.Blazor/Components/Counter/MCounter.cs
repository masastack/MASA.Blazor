namespace Masa.Blazor
{
    public class MCounter : BCounter
    {
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;
        
        private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

#if NET8_0_OR_GREATER
        if (MasaBlazor.IsSsr && !IndependentTheme)
        {
            CascadingIsDark = MasaBlazor.Theme.Dark;
        }
#endif
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-counter")
                        .AddIf("error--text", () => Max != null && (Value.ToInt32() > Max.ToInt32()))
                        .AddTheme(IsDark, IndependentTheme);
                });
        }
    }
}
