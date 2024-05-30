using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;

namespace Masa.Blazor;

public class I18n
{
    public event EventHandler? CultureChanged;

    public I18n(IOptions<MasaBlazorOptions> options)
    {
        SetCulture(options.Value.Locale?.UICulture ?? new CultureInfo("en-US"));
    }

    [NotNull] public CultureInfo? Culture { get; private set; }

    [NotNull] public IReadOnlyDictionary<string, string>? Locale { get; private set; }

    public void SetCulture(CultureInfo uiCulture)
    {
        SetCultureAndLocale(uiCulture);

        CultureChanged?.Invoke(this, EventArgs.Empty);

        CultureInfo.DefaultThreadCurrentUICulture = uiCulture;
    }

    public void AddLocale(CultureInfo culture, IReadOnlyDictionary<string, string>? locale) =>
        I18nCache.AddLocale(culture, locale, true);

    public IEnumerable<CultureInfo> SupportedCultures => I18nCache.GetCultures();

    [return: NotNullIfNotNull("defaultValue")]
    public string? T(string? key, [DoesNotReturnIf(true)] bool whenNullReturnKey = true, string? defaultValue = null,
        params object[] args)
    {
        return T(null, key, whenNullReturnKey, defaultValue, args);
    }

    public string T(string? key, params object[] args)
    {
        return T(null, key, true, null, args);
    }

    [return: NotNullIfNotNull("defaultValue")]
    public string? T(string? scope, string? key, [DoesNotReturnIf(true)] bool whenNullReturnKey = true,
        string? defaultValue = null,
        params object[] args)
    {
        string? value = null;

        if (key is not null)
        {
            var scopeKey = scope is null ? key : $"{scope}.{key}";

            var matchKey = Locale.Keys.FirstOrDefault(k => k.Equals(scopeKey, StringComparison.OrdinalIgnoreCase));
            if (matchKey is not null)
            {
                value = Locale.GetValueOrDefault(matchKey);
            }

            if (value is null && whenNullReturnKey)
            {
                if (key.Trim().Contains(' ') || key.StartsWith(".") || key.EndsWith("."))
                {
                    value = key;
                }
                else
                {
                    value = key.Split('.').Last();
                }
            }
        }

        if (value is null)
        {
            return defaultValue;
        }

        if (args.Length == 0)
        {
            return value;
        }

        try
        {
            return string.Format(value, args);
        }
        catch (FormatException)
        {
            return value;
        }
    }

    private void SetCultureAndLocale(CultureInfo uiCulture)
    {
        if (!EmbeddedLocales.ContainsLocale(uiCulture))
        {
            AddLocale(uiCulture, EmbeddedLocales.GetSpecifiedLocale(uiCulture));
        }

        Culture = uiCulture;
        Locale = I18nCache.GetLocale(uiCulture);
    }
}