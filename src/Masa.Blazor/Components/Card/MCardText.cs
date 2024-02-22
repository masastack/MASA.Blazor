namespace Masa.Blazor;

public class MCardText : Container
{
    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    private Block _block = new("m-card");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            _block.Element("text").AddTheme(CascadingIsDark, isIndependent: false).GenerateCssClasses()
        );
    }
}