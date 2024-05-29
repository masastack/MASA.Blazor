namespace Masa.Blazor
{
    public class CarouselTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "carousel-transition";
        }
    }
}
