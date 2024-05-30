namespace Masa.Blazor
{
    public class VScrollXReverseTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "scroll-x-reverse-transition";
        }
    }
}
