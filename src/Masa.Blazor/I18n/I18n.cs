using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;

namespace Masa.Blazor.Core.I18n;

public class I18n
{
    public event EventHandler? CultureChanged;

    public I18n(IOptions<MasaBlazorOptions> options)
    {
        var cultureName = options.Value.Locale?.Current;

        var culture = GetValidCulture(cultureName, options.Value.Locale?.Fallback ?? "en-US");
        CultureInfo? uiCulture = null;

        var uiCultureName = options.Value.Locale?.UICurrent;
        if (!string.IsNullOrWhiteSpace(uiCultureName))
        {
            uiCulture = GetValidCulture(uiCultureName, options.Value.Locale?.UIFallback ?? culture.Name);
        }

        SetCulture(culture, uiCulture);
    }

    [NotNull] public CultureInfo? Culture { get; private set; }

    [NotNull] public IReadOnlyDictionary<string, string>? Locale { get; private set; }

    public void SetCulture(CultureInfo culture, CultureInfo? uiCulture = null)
    {
        SetCultureInternal(culture, uiCulture ?? culture);
    }

    private void SetCultureInternal(CultureInfo culture, CultureInfo uiCulture)
    {
        SetCultureAndLocale(uiCulture);

        CultureChanged?.Invoke(this, EventArgs.Empty);

        CultureInfo.DefaultThreadCurrentCulture = culture;
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

    private static CultureInfo GetValidCulture(string? cultureName, string fallbackCultureName)
    {
        CultureInfo? culture = null;

        try
        {
            culture = CultureInfo.CreateSpecificCulture(cultureName ?? fallbackCultureName);
        }
        catch (Exception)
        {
            // ignored
        }

        if (culture is null && cultureName is not null)
        {
            try
            {
                culture = CultureInfo.CreateSpecificCulture(fallbackCultureName);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        culture ??= CultureInfo.CurrentCulture;

        // https://github.com/dotnet/runtime/issues/18998#issuecomment-254565364
        // `CultureInfo.CreateSpecificCulture` has the different behavior in different OS,
        // so need to standardize the culture.
        return culture.Name switch
        {
            "zh-Hans-CN" => new CultureInfo("zh-CN"),
            "zh-Hant-CN" => new CultureInfo("zh-TW"),
            _ => culture
        };
    }
}