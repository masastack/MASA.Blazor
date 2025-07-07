namespace Masa.Blazor;

public static class ThemeCssBuilder
{
    public static string Build(Theme theme)
    {
        var options = theme.CurrentTheme;
        var combinePrefix = options.CombinePrefix;
        combinePrefix = combinePrefix?.Trim() ?? string.Empty;

        var lstCss = new List<string>()
        {
            $$"""
              :root {
                {{options}}
              }
              {{BuildThemeCssVariables(theme.Themes)}}
              """
        };

        lstCss.AddRange(_surfaceRoles.Select(role => BuildBgCssClass(combinePrefix, role)));
        lstCss.Add(BuildBgCssClass(combinePrefix, "inverse-surface", "inverse-on-surface"));
        lstCss.AddRange(_baseRoles.Select(role => BuildBgCssClass(combinePrefix, role)));
        lstCss.AddRange(_baseRoles.Select(role => BuildTextCssClass(combinePrefix, role)));
        lstCss.Add(BuildTextCssClass(combinePrefix, "inverse-primary"));
        lstCss.Add(BuildBorderCssClass(combinePrefix, "outline"));
        lstCss.Add(BuildBorderCssClass(combinePrefix, "outline-variant"));

        foreach (var kv in options.UserDefined)
        {
            lstCss.Add(BuildTextCssClass(combinePrefix, kv.Key));
            lstCss.Add(BuildBgCssClass(combinePrefix, kv.Key, "on-" + kv.Key));
        }

        return string.Concat(lstCss);
    }


    private static string[] _surfaceRoles =
    [
        "surface-dim",
        "surface",
        "surface-bright",
        "surface-container-lowest",
        "surface-container-low",
        "surface-container",
        "surface-container-high",
        "surface-container-highest"
    ];

    private static string[] _baseRoles =
    [
        "primary",
        "secondary",
        "accent",
        "tertiary",
        "info",
        "success",
        "warning",
        "error"
    ];

    private static string BuildBgCssClass(string combinePrefix, string role)
    {
        if (_surfaceRoles.Contains(role))
        {
            return BuildBgCssClass(combinePrefix, role, "on-surface");
        }

        return BuildBgCssClass(combinePrefix, role, "on-" + role);
    }

    private static string BuildBgCssClass(string combinePrefix, string bg, string? text)
    {
        var str = $$"""

                    {{combinePrefix}} .{{bg}} {
                        background-color: rgba(var(--m-theme-{{bg}})) !important;
                    """;

        if (!string.IsNullOrWhiteSpace(text))
        {
            str += $$"""

                         color: rgba(var(--m-theme-{{text}})) !important;
                     }
                     """;
        }

        return str;
    }

    private static string BuildTextCssClass(string combinePrefix, string text)
    {
        return $$"""

                 {{combinePrefix}} .{{text}}--text {
                     color: rgba(var(--m-theme-{{text}})) !important;
                     caret-color: rgba(var(--m-theme-{{text}})) !important;
                 }
                 """;
    }

    private static string BuildBorderCssClass(string combinePrefix, string border)
    {
        return $$"""

                 {{combinePrefix}} .{{border}} {
                     border-color: rgba(var(--m-theme-{{border}})) !important;
                 }
                 """;
    }

    private static string BuildThemeCssVariables(Themes themes)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(themes.Light.Build("light"));
        stringBuilder.AppendLine(themes.Dark.Build("dark"));
        foreach (var userDefined in themes.UserDefined)
        {
            stringBuilder.AppendLine(userDefined.Value.Build(userDefined.Key));
        }

        return stringBuilder.ToString();
    }
}