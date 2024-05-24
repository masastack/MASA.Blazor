namespace Masa.Blazor
{
    public class FabTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "fab-transition";
        }
    }
}
