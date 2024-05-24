namespace Masa.Blazor
{
    public class CarouselReverseTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "carousel-reverse-transition";
        }
    }
}