namespace Masa.Blazor;

public partial class MHighlight : BHighlight
{
    [Parameter]
    public bool IgnorePreCssOfTheme { get; set; }

    [Parameter]
    public bool IgnoreCodeCssOfTheme { get; set; }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Apply(css =>
            {
                css.Add("m-code-highlight__pre")
                   .AddIf($"language-{Language.ToLower()}", () => Language is not null && !IgnorePreCssOfTheme);
            }).Apply("code", css =>
            {
                css.Add("m-code-highlight__code")
                   .AddIf($"language-{Language.ToLower()}", () => Language is not null && !IgnoreCodeCssOfTheme);
            });
    }
}
