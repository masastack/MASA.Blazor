namespace Masa.Blazor;

public class MMarkdownIt : BMarkdownIt
{
    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider.Apply(css => { css.Add("m-markdown-it"); });
    }
}
