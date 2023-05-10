namespace Masa.Blazor;

public class Icons
{
    public Icons(IconSet defaultSet, IconAliases aliases)
    {
        DefaultSet = defaultSet;
        Aliases = aliases;
    }

    public Icons(string name, IconAliases aliases)
    {
        Name = name;
        Aliases = aliases;
    }

    public IconSet? DefaultSet { get; set; }

    public IconAliases Aliases { get; set; }

    /// <summary>
    /// Custom icon set name
    /// </summary>
    public string? Name { get; set; }
}