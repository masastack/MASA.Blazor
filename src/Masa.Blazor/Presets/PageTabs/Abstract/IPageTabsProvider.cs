#nullable enable

namespace Masa.Blazor.Presets;

public interface IPageTabsProvider
{
    Dictionary<string, Func<string?>> PathTitles { get; }

    void UpdateTabTitle(string absolutePath, Func<string?> titleFunc);

    EventHandler<string>? TabTitleChanged { get; set; }

    void RemovePathTitles(params string[] absolutePaths);
}
