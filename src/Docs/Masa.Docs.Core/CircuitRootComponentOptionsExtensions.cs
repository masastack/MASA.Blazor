namespace Masa.Docs.Core;

public static class CircuitRootComponentOptionsExtensions
{
    public static void RegisterCustomElementsOfMasaDocs(this IJSComponentConfiguration options)
    {
        options.RegisterCustomElement<Components.AppHeading>("app-heading");
        options.RegisterCustomElement<Components.AppLink>("app-link");
        options.RegisterCustomElement<Components.AppAlert>("app-alerts");
    }
}
