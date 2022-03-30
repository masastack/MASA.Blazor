namespace Masa.Blazor
{
    public partial class MCardText : BCardText
    {
        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-card__text")
                       .AddTheme(CascadingIsDark);
                });
        }
    }
}