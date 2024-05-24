using Masa.Blazor.Components.Transition;

namespace Masa.Blazor;

/// <summary>
/// The ExpandTransition.
/// </summary>
public class ExpandTransition : Transition
{
    // BUG: Unable to get height/width for the first time.
    // TODO: Try to rewrite ExpandTransition with hooks.
    // https://github.com/vuetifyjs/vuetify/blob/aa68dd2d9c/packages/vuetify/src/components/transitions/expand-transition.ts

    protected virtual string SizeProp => "height";

    private double? Size { get; set; }

    protected override void OnParametersSet()
    {
        Name = "expand-transition";
    }

    public override string GetClass(TransitionState transitionState)
    {
        var transitionClass = base.GetClass(transitionState);

        return string.Join(" ", transitionClass);
    }

    public override string GetStyle(TransitionState transitionState)
    {
        var styles = new List<string?>
        {
            base.GetStyle(transitionState)
        };

        switch (transitionState)
        {
            case TransitionState.Enter:
            case TransitionState.LeaveTo:
                styles.Add("overflow:hidden");
                styles.Add($"{SizeProp}:0px");
                break;
            case TransitionState.EnterTo:
            case TransitionState.Leave:
                styles.Add("overflow:hidden");
                if (Size.HasValue)
                {
                    styles.Add($"{SizeProp}:{Size}px");
                }

                break;
        }

        return string.Join(';', styles);
    }

    public override Task Enter(TransitionElementBase element)
    {
        return UpdateSize(element.Reference);
    }

    public override Task Leave(TransitionElementBase element)
    {
        return UpdateSize(element.Reference);
    }

    private async Task UpdateSize(ElementReference elementReference)
    {
        var elementInfo = await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, elementReference);
        var size = elementInfo.OffsetHeight;
        if (size != 0)
        {
            Size = size;
        }
    }
}
