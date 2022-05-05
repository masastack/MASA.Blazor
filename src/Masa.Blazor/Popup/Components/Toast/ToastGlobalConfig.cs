
using Masa.Blazor.Presets;

namespace Masa.Blazor.Popup.Components
{
    public class ToastGlobalConfig
    {
        public int? Duration { get; set; } = 4000;

        public int MaxCount { get; set; }

        public ToastPosition Position { get; set; }
    }
}
