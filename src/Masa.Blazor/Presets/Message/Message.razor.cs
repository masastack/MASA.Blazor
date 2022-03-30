namespace Masa.Blazor.Presets
{
    public partial class Message
    {
        private string Icon => Type switch
        {
            AlertTypes.Success => "mdi-check-circle",
            AlertTypes.Info => "mdi-information",
            AlertTypes.Warning => "mdi-exclamation",
            AlertTypes.Error => "mdi-alert",
            _ => null,
        };

        private string IconColor => Type switch
        {
            AlertTypes.Success => "success",
            AlertTypes.Info => "info",
            AlertTypes.Warning => "warning",
            AlertTypes.Error => "error",
            _ => null
        };

        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public AlertTypes Type { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public int Timeout { get; set; } = 3000;

        private async Task HandleOnClick()
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(false);
            }
            else
            {
                Value = false;
            }
        }
    }
}