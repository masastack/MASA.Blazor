namespace Masa.Blazor.Presets;

public interface IPageStackNavControllerFactory
{
    PageStackNavController Create(string name);

    void Activate(string name);

    void Inactivate(string name);

    string Current { get; }
}