namespace Masa.Blazor
{
    public partial class MExpansionPanels : BExpansionPanels
    {
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
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-item-group theme--light m-expansion-panels")
                        .AddIf("m-expansion-panels--accordion", () => Accordion)
                        .AddIf("m-expansion-panels--flat", () => Flat)
                        .AddIf("m-expansion-panels--hover", () => Hover)
                        .AddIf("m-expansion-panels--focusable", () => Focusable)
                        .AddIf("m-expansion-panels--inset", () => Inset)
                        .AddIf("m-expansion-panels--popout", () => Popout)
                        .AddIf("m-expansion-panels--tile", () => Tile)
                        .AddTheme(IsDark, IndependentTheme);
                });
        }
    }
}
