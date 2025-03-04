namespace Masa.Blazor.Mixins.Activatable;

public class MActivatable : MActivatableBase, IActivatable
{
    [Parameter]
    public RenderFragment<ActivatorProps>? ActivatorContent { get; set; }

    /// <summary>
    /// a flag to sync the active state to js
    /// </summary>
    private bool _syncActiveStateToJS = false;

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

    protected virtual bool ShouldUpdateActiveInJS => ActivatorContent is not null;

    RenderFragment? IActivatable.ComputedActivatorContent => ComputedActivatorContent;

    bool IActivatable.IsActive => IsActive;

    protected override void RunDirectly(bool val)
    {
        if (!IsBooted && val)
        {
            // the js-interop is not ready,
            // so we need to sync the active state to js
            // at the time of first render
            _syncActiveStateToJS = true;
            _ = SetActive(true);
            return;
        }

        if (ShouldUpdateActiveInJS)
        {
            UpdateActiveInJS(val);
        }
        else
        {
            _ = SetActive(val);
        }
    }

    protected override Task OnActiveUpdatedAsync(bool firstActive, bool active)
    {
        if (firstActive && _syncActiveStateToJS)
        {
            _syncActiveStateToJS = false;
            UpdateActiveInJS(active);
        }

        return Task.CompletedTask;
    }
}