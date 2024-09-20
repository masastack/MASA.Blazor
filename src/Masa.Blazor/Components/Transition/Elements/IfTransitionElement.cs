using Masa.Blazor.Components.Transition;

namespace Masa.Blazor;

public class IfTransitionElement : ToggleableTransitionElement
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        ConditionType = ConditionType.If;
    }
}