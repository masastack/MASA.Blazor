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

    private static bool IsBuiltIn(string name)
    {
        return string.Equals(name, "light", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(name, "dark", StringComparison.OrdinalIgnoreCase);
    }
}