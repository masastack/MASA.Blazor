using BemIt.Extensions;

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

                  --m-theme-surface: {{options.Surface}};
                  --m-theme-on-surface: {{options.OnSurface}};
                  --m-theme-surface-container: {{options.SurfaceContainer}};
                  --m-theme-inverse-surface: {{options.InverseSurface}};
                  --m-theme-inverse-on-surface: {{options.InverseOnSurface}};
                  --m-theme-inverse-primary: {{options.InversePrimary}};

                  --m-theme-light-surface: {{light.Surface}};
                  --m-theme-light-on-surface: {{light.OnSurface}};
                  --m-theme-light-surface-container: {{light.SurfaceContainer}};
                  --m-theme-dark-surface: {{dark.Surface}};
                  --m-theme-dark-on-surface: {{dark.OnSurface}};
                  --m-theme-dark-surface-container: {{dark.SurfaceContainer}};
                  --m-theme-light-inverse-surface: {{light.InverseSurface}};
                  --m-theme-light-inverse-on-surface: {{light.InverseOnSurface}};
                  --m-theme-light-inverse-primary: {{light.InversePrimary}};
                  --m-theme-dark-inverse-surface: {{dark.InverseSurface}};
                  --m-theme-dark-inverse-on-surface: {{dark.InverseOnSurface}};
                  --m-theme-dark-inverse-primary: {{dark.InversePrimary}};
              }

              """,
            $"{combinePrefix}a {{ color: {options.Primary}; }}",
            Build(combinePrefix, nameof(options.Primary).ToLowerInvariant(), hasOnColor: true),
            Build(combinePrefix, nameof(options.Secondary).ToLowerInvariant(), hasOnColor: true),
            Build(combinePrefix, nameof(options.Accent).ToLowerInvariant(), hasOnColor: true),
            Build(combinePrefix, nameof(options.Info).ToLowerInvariant(), hasOnColor: true),
            Build(combinePrefix, nameof(options.Success).ToLowerInvariant(), hasOnColor: true),
            Build(combinePrefix, nameof(options.Warning).ToLowerInvariant(), hasOnColor: true),
            Build(combinePrefix, nameof(options.Error).ToLowerInvariant(), hasOnColor: true),
            Build(combinePrefix, nameof(options.Surface).ToLowerInvariant(), hasOnColor: true),
            Build(combinePrefix, options.UserDefined),
            BuildBgCssClass(combinePrefix, nameof(options.SurfaceContainer).ToKebab())
        };

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

    private static string Build(string combinePrefix, string selector, bool hasOnColor = false)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($$"""

                               {{combinePrefix}}.{{selector}} {
                                   background-color: var(--m-theme-{{selector}}) !important;
                                   border-color: var(--m-theme-{{selector}}) !important;
                               """);

        if (hasOnColor)
        {
            stringBuilder.Append($"""

                                      color: var(--m-theme-on-{selector}) !important;
                                  """);
        }

        stringBuilder.Append($$"""

                               }
                               {{combinePrefix}}.{{selector}}--text {
                                   color: var(--m-theme-{{selector}}) !important;
                                   caret-color: var(--m-theme-{{selector}}) !important;
                               }
                               """);

        return stringBuilder.ToString();
    }

    private static string Build(string combinePrefix, Dictionary<string, string> userDefined)
    {
        return string.Join("", userDefined.Select(x => Build(combinePrefix, x.Key.ToLowerInvariant())));
    }
    
    private static string BuildBgCssClass(string combinePrefix, string className)
    {
        return $$"""

               {{combinePrefix}}.{{className}} {
                   background-color: var(--m-theme-{{className}}) !important;
                   border-color: var(--m-theme-{{className}}) !important;
               }
               """;
    }
}