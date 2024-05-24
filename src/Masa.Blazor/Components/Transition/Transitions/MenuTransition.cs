namespace Masa.Blazor
{
    public class MenuTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "menu-transition";
        }
    }
}
