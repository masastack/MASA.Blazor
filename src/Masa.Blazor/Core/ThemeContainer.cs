namespace Masa.Blazor.Core;

public abstract class ThemeContainer : Container
{
    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    public bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            if (Light)
            {
                return false;
            }

            return CascadingIsDark;
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<bool>>(0);
        builder.AddAttribute(1, "Value", IsDark);
        builder.AddAttribute(2, "Name", "IsDark");
        builder.AddAttribute(3, "ChildContent", (RenderFragment)base.BuildRenderTree);
        builder.CloseComponent();
    }
}