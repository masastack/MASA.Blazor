namespace Masa.Blazor
{
    public class ScaleTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "scale-transition";
        }
    }
}
