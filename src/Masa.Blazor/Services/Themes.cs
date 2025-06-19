using System.Collections.ObjectModel;

namespace Masa.Blazor;

public class Themes(ThemeOptions light, ThemeOptions dark)
{
    internal readonly Dictionary<string, ThemeOptions> UserDefined = [];

    public ThemeOptions Dark { get; } = dark;

    public ThemeOptions Light { get; } = light;

    public ThemeOptions this[string name]
    {
        get
        {
            if (string.Equals(name, "light", StringComparison.OrdinalIgnoreCase))
            {
                return Light;
            }

            if (string.Equals(name, "dark", StringComparison.OrdinalIgnoreCase))
            {
                return Dark;
            }

            if (UserDefined.TryGetValue(name, out var themeOptions))
            {
                return themeOptions;
            }

            throw new KeyNotFoundException(name);
        }
    }

    public void Add(string name, bool dark, Action<ThemeOptions> themeConfigure)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (IsBuiltIn(name))
        {
            throw new InvalidOperationException($"Theme name '{name}' is reserved for built-in themes.");
        }

        if (UserDefined.ContainsKey(name))
        {
            return;
        }

        var themeOptions = (dark ? Dark : Light).ShallowCopy();
        themeConfigure.Invoke(themeOptions);
        UserDefined.Add(name, themeOptions);
    }

    public bool Exists(string name)
    {
        if (string.Equals(name, "light", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(name, "dark", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return UserDefined.ContainsKey(name);
    }

    private static bool IsBuiltIn(string name)
    {
        return string.Equals(name, "light", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(name, "dark", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Get all themes including built-in light and dark themes, and user-defined themes.
    /// </summary>
    /// <returns>A read-only dictionary containing all themes.</returns>
    public IReadOnlyDictionary<string, ThemeOptions> GetAll()
    {
        var result = new Dictionary<string, ThemeOptions>(UserDefined.Count + 2)
        {
            { "light", Light },
            { "dark", Dark }
        };

        foreach (var item in UserDefined)
        {
            result.Add(item.Key, item.Value);
        }

        return new ReadOnlyDictionary<string, ThemeOptions>(result);
    }
}