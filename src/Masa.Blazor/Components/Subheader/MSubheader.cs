namespace Masa.Blazor
{
    public partial class MSubheader : BSubheader, IThemeable
    {
        [Parameter]
        public bool Inset { get; set; }


        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-subheader")
                        .AddIf("m-subheader--inset", () => Inset)
                        .AddTheme(IsDark);
                });
        }
    }
}
