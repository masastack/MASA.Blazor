namespace Masa.Blazor;

public class MAppBarNavIcon : MButton
{
    [Parameter]
    [MasaApiParameter(Ignored = true)]
    public override bool Icon { get; set; } = true;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Icon = true;
        ChildContent ??= DefaultChildContent;
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        return ["m-app-bar__nav-icon", ..base.BuildComponentClass()];
    }

    private RenderFragment DefaultChildContent => builder =>
    {
        if (IconName == null)
        {
            builder.OpenComponent<MIcon>(0);
            builder.AddAttribute(1, "Icon", (Icon)"$menu");
            builder.CloseComponent();
        }
    };
}