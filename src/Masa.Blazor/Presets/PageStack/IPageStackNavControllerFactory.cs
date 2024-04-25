namespace Masa.Blazor;

public interface IPageStackNavControllerFactory
{
    PageStackNavController Create(string name);
}