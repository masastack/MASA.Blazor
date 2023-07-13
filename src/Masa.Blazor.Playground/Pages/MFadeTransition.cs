namespace Masa.Blazor.Playground.Pages;

public class MFadeTransition : MTransition
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Name ??= "fade-transition";
    }
}
