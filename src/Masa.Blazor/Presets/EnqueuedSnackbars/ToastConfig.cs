using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor.Presets
{
    public class ToastConfig
    {
        public ToastConfig()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public bool Visible { get; set; } = true;
        public AlertTypes Type { get; set; }
        public string Color { get; set; }
        public bool Dark { get; set; }
        public bool Light { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? Duration { get; set; } = 4000;
        public RenderFragment ActionContent { get; set; }
        public string CloseIcon { get; set; } = "mdi-close";
        public Func<string, Task> OnClose { get; set; }
    }
}
