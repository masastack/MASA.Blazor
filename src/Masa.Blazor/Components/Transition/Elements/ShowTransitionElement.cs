using Masa.Blazor.Components.Transition;

namespace Masa.Blazor;

public class ShowTransitionElement : ToggleableTransitionElement
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ConditionType = ConditionType.Show;
    }
}