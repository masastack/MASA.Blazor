using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MASA.Blazor.Presets
{
    public partial class Message
    {
        private string Icon => Type switch
        {
            AlertType.Success => "mdi-check-circle",
            AlertType.Info => "mdi-information",
            AlertType.Warning => "mdi-exclamation",
            AlertType.Error => "mdi-alert",
            _ => null,
        };

        private string IconColor => Type switch
        {
            AlertType.Success => "success",
            AlertType.Info => "info",
            AlertType.Warning => "warning",
            AlertType.Error => "error",
            _ => null
        };

        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public EventCallback<bool> VisibleChanged { get; set; }

        [Parameter]
        public AlertType Type { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public int Timeout { get; set; } = 3000;

        private async Task HandleOnClick()
        {
            if (VisibleChanged.HasDelegate)
            {
                await VisibleChanged.InvokeAsync(false);
            }
        }
    }
}
