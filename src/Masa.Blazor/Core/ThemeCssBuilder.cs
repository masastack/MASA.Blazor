namespace Masa.Blazor
{
    public class ThemeCssBuilder
    {
        public string Build(Theme theme)
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
                      --m-theme-primary: {{options.Primary}};
                      --m-theme-primary-text: {{options.Primary}};
                      --m-theme-on-primary: {{options.OnPrimary}};
                      --m-theme-secondary: {{options.Secondary}};
                      --m-theme-secondary-text: {{options.Secondary}};
                      --m-theme-on-secondary: {{options.OnSecondary}};
                      --m-theme-accent: {{options.Accent}};
                      --m-theme-accent-text: {{options.Accent}};
                      --m-theme-on-accent: {{options.OnAccent}};
                      --m-theme-error: {{options.Error}};
                      --m-theme-error-text: {{options.Error}};
                      --m-theme-on-error: {{options.OnError}};
                      --m-theme-info: {{options.Info}};
                      --m-theme-info-text: {{options.Info}};
                      --m-theme-on-info: {{options.OnInfo}};
                      --m-theme-success: {{options.Success}};
                      --m-theme-success-text: {{options.Success}};
                      --m-theme-on-success: {{options.OnSuccess}};
                      --m-theme-warning: {{options.Warning}};
                      --m-theme-warning-text: {{options.Warning}};
                      --m-theme-on-warning: {{options.OnWarning}};

                      --m-theme-surface: {{options.Surface}};
                      --m-theme-on-surface: {{options.OnSurface}};
                      --m-theme-inverse-surface: {{options.InverseSurface}};
                      --m-theme-inverse-on-surface: {{options.InverseOnSurface}};
                      --m-theme-inverse-primary: {{options.InversePrimary}};

                      --m-theme-light-surface: {{light.Surface}};
                      --m-theme-light-on-surface: {{light.OnSurface}};
                      --m-theme-dark-surface: {{dark.Surface}};
                      --m-theme-dark-on-surface: {{dark.OnSurface}};
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
            };

            foreach (var pair in options.UserDefined)
            {
                lstCss.Add(Build(combinePrefix, pair.Key.ToLowerInvariant()));
            }

            return string.Concat(lstCss);
        }

        private string Build(string combinePrefix, string selector, bool hasOnColor = false)
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
    }
}