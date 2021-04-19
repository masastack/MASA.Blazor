using System;
using System.Globalization;

namespace MASA.Blazor.Doc.Localization
{
    public interface ILanguageService
    {
        CultureInfo CurrentCulture { get; }

        Resources Resources { get; }

        event EventHandler<CultureInfo> LanguageChanged;

        void SetLanguage(CultureInfo culture);
    }
}