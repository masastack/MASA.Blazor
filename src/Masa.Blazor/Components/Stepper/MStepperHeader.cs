namespace Masa.Blazor;

public class MStepperHeader : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        return new[] { "m-stepper__header" };
    }
}