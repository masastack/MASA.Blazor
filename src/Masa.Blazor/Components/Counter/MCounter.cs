namespace Masa.Blazor
{
    public class MCounter : BCounter
    {


        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-counter")
                        .AddIf("error--text", () => Max != null && (Value.ToInt32() > Max.ToInt32()))
                        .AddTheme(IsDark);
                });
        }
    }
}
