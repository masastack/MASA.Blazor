namespace Masa.Blazor
{
    public class ScrollXTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "scroll-x-transition";
        }
    }
}
