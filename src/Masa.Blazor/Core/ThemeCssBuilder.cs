namespace Masa.Blazor;

public static class ThemeCssBuilder
{
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
                  color-scheme: {{(isDark ? "dark" : "normal")}};
                  {{BuildCssVariable("primary", options.Primary)}}
                  {{BuildCssVariable("secondary", options.Secondary)}}
                  {{BuildCssVariable("accent", options.Accent)}}
                  {{BuildCssVariable("info", options.Info)}}
                  {{BuildCssVariable("success", options.Success)}}
                  {{BuildCssVariable("warning", options.Warning)}}
                  {{BuildCssVariable("error", options.Error)}}
                  {{BuildCssVariable(options.UserDefined)}}

                  --m-theme-on-primary: {{options.OnPrimary}};
                  --m-theme-on-secondary: {{options.OnSecondary}};
                  --m-theme-on-accent: {{options.OnAccent}};
                  --m-theme-on-error: {{options.OnError}};
                  --m-theme-on-info: {{options.OnInfo}};
                  --m-theme-on-success: {{options.OnSuccess}};
                  --m-theme-on-warning: {{options.OnWarning}};

                  --m-theme-surface-dim: {{options.SurfaceDim}};
                  --m-theme-surface: {{options.Surface}};
                  --m-theme-surface-bright: {{options.SurfaceBright}};
                  --m-theme-surface-container-lowest: {{options.SurfaceContainerLowest}};
                  --m-theme-surface-container-low: {{options.SurfaceContainerLow}};
                  --m-theme-surface-container: {{options.SurfaceContainer}};
                  --m-theme-surface-container-high: {{options.SurfaceContainerHigh}};
                  --m-theme-surface-container-highest: {{options.SurfaceContainerHighest}};
                  --m-theme-on-surface: {{options.OnSurface}};
                  --m-theme-on-surface-variant: {{options.OnSurfaceVariant}};
                  --m-theme-outline: {{options.Outline}};
                  --m-theme-outline-variant: {{options.OutlineVariant}};
                  --m-theme-inverse-surface: {{options.InverseSurface}};
                  --m-theme-inverse-on-surface: {{options.InverseOnSurface}};
                  --m-theme-inverse-primary: {{options.InversePrimary}};

                  --m-theme-light-surface-dim: {{light.SurfaceDim}};
                  --m-theme-light-surface: {{light.Surface}};
                  --m-theme-light-surface-bright: {{light.SurfaceBright}};
                  --m-theme-light-surface-container-lowest: {{light.SurfaceContainerLowest}};
                  --m-theme-light-surface-container-low: {{light.SurfaceContainerLow}};
                  --m-theme-light-surface-container: {{light.SurfaceContainer}};
                  --m-theme-light-surface-container-high: {{light.SurfaceContainerHigh}};
                  --m-theme-light-surface-container-highest: {{light.SurfaceContainerHighest}};
                  --m-theme-dark-surface-dim: {{dark.SurfaceDim}};
                  --m-theme-dark-surface: {{dark.Surface}};
                  --m-theme-dark-surface-bright: {{dark.SurfaceBright}}; 
                  --m-theme-dark-surface-container-lowest: {{dark.SurfaceContainerLowest}};
                  --m-theme-dark-surface-container-low: {{dark.SurfaceContainerLow}};
                  --m-theme-dark-surface-container: {{dark.SurfaceContainer}};
                  --m-theme-dark-surface-container-high: {{dark.SurfaceContainerHigh}};
                  --m-theme-dark-surface-container-highest: {{dark.SurfaceContainerHighest}};
                    
                  --m-theme-light-on-surface: {{light.OnSurface}};
                  --m-theme-light-on-surface-variant: {{light.OnSurfaceVariant}};
                  --m-theme-dark-on-surface: {{dark.OnSurface}};
                  --m-theme-dark-on-surface-variant: {{dark.OnSurfaceVariant}};

                  --m-theme-light-outline: {{light.Outline}};
                  --m-theme-light-outline-variant: {{light.OutlineVariant}};
                  --m-theme-dark-outline: {{dark.Outline}};
                  --m-theme-dark-outline-variant: {{dark.OutlineVariant}};

                  --m-theme-light-inverse-surface: {{light.InverseSurface}};
                  --m-theme-light-inverse-on-surface: {{light.InverseOnSurface}};
                  --m-theme-light-inverse-primary: {{light.InversePrimary}};
                  --m-theme-dark-inverse-surface: {{dark.InverseSurface}};
                  --m-theme-dark-inverse-on-surface: {{dark.InverseOnSurface}};
                  --m-theme-dark-inverse-primary: {{dark.InversePrimary}};
              }

              """,
            $"{combinePrefix}a {{ color: {options.Primary}; }}",
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
            BuildBgCssClass(combinePrefix, "surface-dim", "surface"),
            BuildBgCssClass(combinePrefix, "surface", "surface"),
            BuildBgCssClass(combinePrefix, "surface-bright", "surface"),
            BuildBgCssClass(combinePrefix, "surface-container-lowest", "surface"),
            BuildBgCssClass(combinePrefix, "surface-container-low", "surface"),
            BuildBgCssClass(combinePrefix, "surface-container", "surface"),
            BuildBgCssClass(combinePrefix, "surface-container-high", "surface"),
            BuildBgCssClass(combinePrefix, "surface-container-highest", "surface"),
            BuildBorderCssClass(combinePrefix, "outline"),
            BuildBorderCssClass(combinePrefix, "outline-variant"),
        };

        foreach (var kv in options.UserDefined)
        {
            lstCss.Add(BuildTextCssClass(combinePrefix, kv.Key));
            lstCss.Add(BuildBgCssClass(combinePrefix, kv.Key));
        }

        return string.Concat(lstCss);
    }

    private static string BuildCssVariable(string role, string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? string.Empty
            : $"""
               --m-theme-{role}: {value};
                   --m-theme-{role}-text: {value};
               """;
    }

    private static string BuildCssVariable(Dictionary<string, string> userDefined)
    {
        return string.Join("", userDefined.Select(x => BuildCssVariable(x.Key, x.Value)));
    }
    
    private static string BuildBgCssClass(string combinePrefix, string bg, string? group = null)
    {
        var str = $$"""

               {{combinePrefix}}.{{bg}} {
                   background-color: var(--m-theme-{{bg}}) !important;
               """;

        if (!string.IsNullOrWhiteSpace(group))
        {
            str += $$"""

                       color: var(--m-theme-on-{{group}}) !important;
                   }
                   """;
        }

        return str;
    }

    private static string BuildTextCssClass(string combinePrefix, string text)
    {
        return $$"""

               {{combinePrefix}}.{{text}}--text {
                   color: var(--m-theme-{{text}}) !important;
                   caret-color: var(--m-theme-{{text}}) !important;
               }
               """;
    }
    
    private static string BuildBorderCssClass(string combinePrefix, string border)
    {
        return $$"""

               {{combinePrefix}}.{{border}} {
                   border-color: var(--m-theme-{{border}}) !important;
               }
               """;
    }
}