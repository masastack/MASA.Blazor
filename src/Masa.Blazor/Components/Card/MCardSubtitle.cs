namespace Masa.Blazor
{
    public partial class MCardSubtitle : BCardSubtitle
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-card__subtitle");
                });
        }
    }
}