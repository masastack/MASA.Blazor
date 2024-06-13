namespace Masa.Blazor;

public class Locale
{
    public CultureInfo Culture { get; }

    public CultureInfo UICulture { get; }

    public Locale(string current, string fallback = "en-US")
    {
        Culture = GetValidCulture(current, fallback);
        UICulture = Culture;
    }

    public Locale(string current, string uiCurrent, string fallback, string uiFallback)
    {
        Culture = GetValidCulture(current, fallback);
        UICulture = GetValidCulture(uiCurrent, uiFallback);
    }

    public Locale(CultureInfo culture, CultureInfo? uiCulture = null)
    {
        Culture = culture;
        UICulture = uiCulture ?? culture;
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