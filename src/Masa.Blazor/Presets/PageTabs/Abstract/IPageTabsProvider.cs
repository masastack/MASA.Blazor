namespace Masa.Blazor.Presets;

public interface IPageTabsProvider
{
    /// <summary>
    /// Update tab title
    /// </summary>
    /// <param name="absolutePath">The absolute path of the URI</param>
    /// <param name="titleFunc">the func to get title</param>
    void UpdateTabTitle(string absolutePath, Func<string?> titleFunc);

    /// <summary>
    /// Update tab title
    /// </summary>
    /// <param name="absolutePath">The absolute path of the URI</param>
    /// <param name="title">the title</param>
    void UpdateTabTitle(string absolutePath, string? title);
}
