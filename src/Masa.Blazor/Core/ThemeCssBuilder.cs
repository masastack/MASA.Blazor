namespace Masa.Blazor;

public static class ThemeCssBuilder
{
    private static string BuildCssVariables(ThemeOptions options)
    {
        return $"""
                {BuildCssVariable("primary", options.Primary, options.OnPrimary)}
                  {BuildCssVariable("secondary", options.Secondary, options.OnSecondary)}
                  {BuildCssVariable("accent", options.Accent, options.OnAccent)}
                  {BuildCssVariable("info", options.Info, options.OnInfo)}
                  {BuildCssVariable("success", options.Success, options.OnSuccess)}
                  {BuildCssVariable("warning", options.Warning, options.OnWarning)}
                  {BuildCssVariable("error", options.Error, options.OnError)}
                  {BuildCssVariable(options.UserDefined)}

                  {BuildCssVariable("surface-dim", options.SurfaceDim)}
                  {BuildCssVariable("surface", options.Surface)}
                  {BuildCssVariable("surface-bright", options.SurfaceBright)}
                  {BuildCssVariable("surface-container-lowest", options.SurfaceContainerLowest)}
                  {BuildCssVariable("surface-container-low", options.SurfaceContainerLow)}
                  {BuildCssVariable("surface-container", options.SurfaceContainer)}
                  {BuildCssVariable("surface-container-high", options.SurfaceContainerHigh)}
                  {BuildCssVariable("surface-container-highest", options.SurfaceContainerHighest)}
                  {BuildCssVariable("on-surface", options.OnSurface)}
                  {BuildCssVariable("surface-variant", options.SurfaceVariant)}
                  {BuildCssVariable("on-surface-variant", options.OnSurfaceVariant)}
                  {BuildCssVariable("inverse-surface", options.InverseSurface)}
                  {BuildCssVariable("inverse-on-surface", options.InverseOnSurface)}
                  {BuildCssVariable("inverse-primary", options.InversePrimary)}

                  {options.Variables}
                  
                  --m-theme-outline: var(--m-theme-on-surface), var(--m-low-emphasis-opacity);
                  --m-theme-outline-variant: var(--m-theme-on-surface), var(--m-border-opacity);
                """;
    }

    public static string Build(Theme theme)
    {
        var options = theme.CurrentTheme;
        var combinePrefix = options.CombinePrefix;
        combinePrefix ??= string.Empty;
        combinePrefix = combinePrefix.EndsWith(' ') ? combinePrefix : $"{combinePrefix} ";

        var lstCss = new List<string>()
        {
            $$"""
              :root {
                {{BuildCssVariables(options)}}
              }
              {{BuildThemeCssVariables(theme.Themes)}}
              """,
            BuildBgCssClass(combinePrefix, "surface-dim", "surface"),
            BuildBgCssClass(combinePrefix, "surface", "surface"),
            BuildBgCssClass(combinePrefix, "surface-bright", "surface"),
            BuildBgCssClass(combinePrefix, "surface-container-lowest", "surface"),
            BuildBgCssClass(combinePrefix, "surface-container-low", "surface"),
            BuildBgCssClass(combinePrefix, "surface-container", "surface"),
            BuildBgCssClass(combinePrefix, "surface-container-high", "surface"),
            BuildBgCssClass(combinePrefix, "surface-container-highest", "surface"),
            BuildBgCssClass(combinePrefix, "primary", "primary"),
            BuildBgCssClass(combinePrefix, "secondary", "secondary"),
            BuildBgCssClass(combinePrefix, "accent", "accent"),
            BuildBgCssClass(combinePrefix, "info", "info"),
            BuildBgCssClass(combinePrefix, "success", "success"),
            BuildBgCssClass(combinePrefix, "warning", "warning"),
            BuildBgCssClass(combinePrefix, "error", "error"),
            BuildTextCssClass(combinePrefix, "primary"),
            BuildTextCssClass(combinePrefix, "secondary"),
            BuildTextCssClass(combinePrefix, "accent"),
            BuildTextCssClass(combinePrefix, "info"),
            BuildTextCssClass(combinePrefix, "success"),
            BuildTextCssClass(combinePrefix, "warning"),
            BuildTextCssClass(combinePrefix, "error"),
            BuildBorderCssClass(combinePrefix, "outline"),
            BuildBorderCssClass(combinePrefix, "outline-variant"),
        };

        foreach (var kv in options.UserDefined)
        {
            lstCss.Add(BuildTextCssClass(combinePrefix, kv.Key));
            lstCss.Add(BuildBgCssClass(combinePrefix, kv.Key, kv.Key));
        }

        return string.Concat(lstCss);
    }

    private static string BuildCssVariable(string role, string? color, string? onColor)
    {
        var stringBuilder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(color))
        {
            var value = ColorParser.ParseColorAsString(color);
            stringBuilder.AppendLine(
                $"""
                 --m-theme-{role}: {value};
                   --m-theme-{role}-text: {value};
                 """);
        }

        if (!string.IsNullOrWhiteSpace(onColor))
        {
            var value = ColorParser.ParseColorAsString(onColor);
            stringBuilder.Append($"  --m-theme-on-{role}: {value};");
        }

        return stringBuilder.ToString();
    }

    private static string BuildCssVariable(Dictionary<string, ColorPairing> userDefined)
    {
        return string.Join("", userDefined.Select(x => BuildCssVariable(x.Key, x.Value.Color, x.Value.OnColor)));
    }

    private static string BuildBgCssClass(string combinePrefix, string bg, string? group = null)
    {
        var str = $$"""

                    {{combinePrefix}}.{{bg}} {
                        background-color: rgba(var(--m-theme-{{bg}})) !important;
                    """;

        if (!string.IsNullOrWhiteSpace(group))
        {
            str += $$"""

                         color: rgba(var(--m-theme-on-{{group}})) !important;
                     }
                     """;
        }

        return str;
    }

    private static string BuildTextCssClass(string combinePrefix, string text)
    {
        return $$"""

                 {{combinePrefix}}.{{text}}--text {
                     color: rgba(var(--m-theme-{{text}})) !important;
                     caret-color: rgba(var(--m-theme-{{text}})) !important;
                 }
                 """;
    }

    private static string BuildBorderCssClass(string combinePrefix, string border)
    {
        return $$"""

                 {{combinePrefix}}.{{border}} {
                     border-color: rgba(var(--m-theme-{{border}})) !important;
                 }
                 """;
    }

    private static string? BuildCssVariable(string name, string? color)
    {
        if (string.IsNullOrWhiteSpace(color))
        {
            return null;
        }

        return $"--m-theme-{name}: {ColorParser.ParseColorAsString(color)};";
    }

    private static string BuildThemeCssVariables(Themes themes)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(BuildScope("light", themes.Light));
        stringBuilder.AppendLine(BuildScope("dark", themes.Dark));
        foreach (var userDefined in themes.UserDefined)
        {
            stringBuilder.AppendLine(BuildScope(userDefined.Key, userDefined.Value));
        }
        return stringBuilder.ToString();
    }

    private static string BuildScope(string theme, ThemeOptions options)
    {
        return $$"""
                 .theme--{{theme}} {
                   color-scheme: {{options.ColorScheme}};
                   {{BuildCssVariables(options)}}
                 }
                 """;
    }
}