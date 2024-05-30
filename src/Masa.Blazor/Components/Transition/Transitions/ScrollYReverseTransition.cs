namespace Masa.Blazor
{
    public class ScrollYReverseTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "scroll-y-reverse-transition";
        }
    }
}
