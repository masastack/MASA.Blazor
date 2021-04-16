using BlazorComponent;
using System.Collections.Generic;

namespace MASA.Blazor
{
    public class MasaBlazorOptions
    {
        /// <summary>
        /// Default is false
        /// </summary>
        public bool DarkTheme { get; set; }

        /// <summary>
        /// The theme options
        /// </summary>
        public ThemeOptions Theme { get; set; } = new ThemeOptions()
        {
            CombinePrefix = ".m-application",
            Primary = "#1976D2",
            Secondary = "#424242",
            Accent = "#82B1FF",
            Error = "#FF5252",
            Info = "#2196F3",
            Success = "#4CAF50",
            Warning = "#FFC107",
            UserDefined = new Dictionary<string, string>() { { "Tertiary", "#e57373" } },
        };
    }
}
