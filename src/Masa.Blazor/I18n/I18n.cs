using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;

namespace Masa.Blazor;

public class I18n
{
    public event EventHandler? CultureChanged;

    public I18n(MasaBlazorOptions options)
    {
        var defaultCulture = new CultureInfo("en-US");
        SetCulture(options.Locale?.Culture ?? defaultCulture, options.Locale?.UICulture ?? defaultCulture);
    }

    /// <summary>
    /// The UICulture of the current thread.
    /// </summary>
    [NotNull] public CultureInfo? Culture { get; private set; }

    [NotNull] public IReadOnlyDictionary<string, string>? Locale { get; private set; }

    /// <summary>
    /// Sets the culture for the current thread and optionally sets the UI culture.
    /// </summary>
    /// <param name="culture">The culture to set for the current thread.</param>
    /// <param name="uiCulture">Optional. The culture to set for the UI. If not provided, the value of 'culture' is used.</param>
    public void SetCulture(CultureInfo culture, CultureInfo? uiCulture = null)
    {
        SetUICulture(uiCulture ?? culture);

        CultureInfo.DefaultThreadCurrentCulture = culture;
    }

    /// <summary>
    /// Sets the UI culture for the current thread.
    /// </summary>
    /// <param name="uiCulture">The culture to set for the UI.</param>
    public void SetUICulture(CultureInfo uiCulture)
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