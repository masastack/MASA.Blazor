namespace Masa.Blazor;

public class MStepperItems : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        return new[] { "m-stepper__items" };
    }
}