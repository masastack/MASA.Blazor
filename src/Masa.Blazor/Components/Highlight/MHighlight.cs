namespace Masa.Blazor;

public partial class MHighlight : BHighlight
{
    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Apply(css =>
            {
                css.Add("m-code-highlight")
                   .AddIf($"language-{Language.ToLower()}", () => Language is not null);
            }).Apply("code", css =>
            {
                css.AddIf($"language-{Language.ToLower()}", () => Language is not null);
            });
    }
}
