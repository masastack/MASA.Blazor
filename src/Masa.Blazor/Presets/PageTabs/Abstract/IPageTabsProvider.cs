#nullable enable

namespace Masa.Blazor.Presets;

public interface IPageTabsProvider
{
    Dictionary<string, string?> PathTitles { get; }

    void UpdateTabTitle(string absolutePath, string? title);

    EventHandler<string>? TabTitleChanged { get; set; }

    void RemovePathTitles(params string[] absolutePaths);
}
