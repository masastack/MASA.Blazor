namespace Masa.Blazor.Presets;

public interface IPageTabsProvider
{
    Dictionary<string, string> PathTitles { get; }

    void UpdateTabTitle(string absolutePath, string title);
}