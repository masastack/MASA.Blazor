namespace Masa.Blazor
{
    public class ScrollYTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "scroll-y-transition";
        }
    }
}
