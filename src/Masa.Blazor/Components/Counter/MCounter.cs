namespace Masa.Blazor
{
    public class MCounter : BCounter
    {
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;
        
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            CascadingIsDark = MasaBlazor.IsSsr && MasaBlazor.Theme.Dark;
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
