namespace Masa.Blazor.Presets.PageStack.NavController;

public interface IPageStackNavControllerFactory
{
    PageStackNavController Create(string name);
}