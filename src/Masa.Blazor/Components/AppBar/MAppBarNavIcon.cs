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

        Class ??= "";
        Class = "m-app-bar__nav-icon " + Class;

        ChildContent ??= DefaultChildContent;
    }

    private RenderFragment DefaultChildContent => builder =>
    {
        builder.OpenComponent<MIcon>(0);
        builder.AddAttribute(1, "Icon", (Icon)"$menu");
        builder.CloseComponent();
    };
}