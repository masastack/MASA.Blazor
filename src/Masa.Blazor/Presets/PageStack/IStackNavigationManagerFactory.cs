namespace Masa.Blazor;

public interface IStackNavigationManagerFactory
{
    StackNavigationManager Create(string name);
}