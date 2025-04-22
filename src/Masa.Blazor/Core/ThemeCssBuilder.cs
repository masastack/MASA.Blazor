namespace Masa.Blazor;

public static class ThemeCssBuilder
{
    private static string BuildThemeCssVariable(ThemeOptions options)
    {
        return $"""
                {BuildColorCssVariable("primary", options.Primary, options.OnPrimary)}
                  {BuildColorCssVariable("secondary", options.Secondary, options.OnSecondary)}
                  {BuildColorCssVariable("accent", options.Accent, options.OnAccent)}
                  {BuildColorCssVariable("info", options.Info, options.OnInfo)}
                  {BuildColorCssVariable("success", options.Success, options.OnSuccess)}
                  {BuildColorCssVariable("warning", options.Warning, options.OnWarning)}
                  {BuildColorCssVariable("error", options.Error, options.OnError)}
                  {BuildColorCssVariable(options.UserDefined)}

                  {BuildColorCssVariable("surface-dim", options.SurfaceDim)}
                  {BuildColorCssVariable("surface", options.Surface)}
                  {BuildColorCssVariable("surface-bright", options.SurfaceBright)}
                  {BuildColorCssVariable("surface-container-lowest", options.SurfaceContainerLowest)}
                  {BuildColorCssVariable("surface-container-low", options.SurfaceContainerLow)}
                  {BuildColorCssVariable("surface-container", options.SurfaceContainer)}
                  {BuildColorCssVariable("surface-container-high", options.SurfaceContainerHigh)}
                  {BuildColorCssVariable("surface-container-highest", options.SurfaceContainerHighest)}
                  {BuildColorCssVariable("on-surface", options.OnSurface)}
                  {BuildColorCssVariable("surface-variant", options.SurfaceVariant)}
                  {BuildColorCssVariable("on-surface-variant", options.OnSurfaceVariant)}
                  {BuildColorCssVariable("outline", options.Outline)}
                  {BuildColorCssVariable("outline-variant", options.OutlineVariant)}
                  {BuildColorCssVariable("inverse-surface", options.InverseSurface)}
                  {BuildColorCssVariable("inverse-on-surface", options.InverseOnSurface)}
                  {BuildColorCssVariable("inverse-primary", options.InversePrimary)}

                  {options.Variables.ToString()}
                """;
    }

    private static string? BuildColorCssVariable(string name, string? color)
    {
        if (string.IsNullOrWhiteSpace(color))
        {
            return null;
        }

        return $"--m-theme-{name}: {ColorParser.ParseColorAsString(color)};";
    }

    private static string BuildNumberCssVariable(string name, float value)
    {
        return $"--m-{name}: {value};";
    }

    private static string BuildScope(string theme, bool dark, ThemeOptions options)
    {
        return $$"""
                 .m-theme--{{theme}} {
                   color-scheme: {{(dark ? "dark" : "normal")}};
                   {{BuildThemeCssVariable(options)}}
                 }
                 """;
    }

    public static string Build(Theme theme)
    {
        var options = theme.CurrentTheme;
        var dark = theme.Themes.Dark;
        var light = theme.Themes.Light;
        var isDark = theme.Dark;

        var combinePrefix = options.CombinePrefix;
        combinePrefix ??= string.Empty;
        combinePrefix = combinePrefix.EndsWith(' ') ? combinePrefix : $"{combinePrefix} ";

        var lstCss = new List<string>()
        {
            $$"""
              :root {
                {{BuildThemeCssVariable(options)}}
              }
              {{BuildScope("light", false, light)}}
              {{BuildScope("dark", true, dark)}}
              """,
            $"{combinePrefix}a {{ color: {options.Primary}; }}",
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
            BuildTextCssClass(combinePrefix, "on-surface"),
            BuildTextCssClass(combinePrefix, "on-surface-variant"),
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

    private static string BuildColorCssVariable(string role, string? color, string? onColor)
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

    private static string BuildColorCssVariable(Dictionary<string, ColorPairing> userDefined)
    {
        return string.Join("", userDefined.Select(x => BuildColorCssVariable(x.Key, x.Value.Color, x.Value.OnColor)));
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
}