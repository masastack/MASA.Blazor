namespace Masa.Blazor.Mixins.Activatable;

public class MActivatable : MActivatableBase, IActivatable
{
    [Parameter]
    public RenderFragment<ActivatorProps>? ActivatorContent { get; set; }

    protected RenderFragment? ComputedActivatorContent
    {
        get
        {
            if (ActivatorContent == null)
            {
                return null;
            }

            var props = new ActivatorProps(ActivatorAttributes);
            return ActivatorContent(props);
        }
    }

    RenderFragment? IActivatable.ComputedActivatorContent => ComputedActivatorContent;

    bool IActivatable.IsActive => IsActive;

    protected override void RunDirectly(bool val)
    {
        if (ActivatorContent is null)
        {
            _ = SetActive(val);
        }
        else
        {
            base.RunDirectly(val);
        }
    }
}
