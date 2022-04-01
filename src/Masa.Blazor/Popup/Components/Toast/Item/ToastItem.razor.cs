using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor.Popup.Components
{
    public partial class ToastItem : AlertingPopupComponentBase
    {
        [Parameter]
        public EventCallback<ToastConfig> HandleOnCloseAsync { get; set; }

        [Parameter]
        public ToastConfig Config { get; set; }

        public async Task HandleOnClose()
        {
            if (HandleOnCloseAsync.HasDelegate)
                await HandleOnCloseAsync.InvokeAsync(Config);
        }

        protected override Task OnInitializedAsync()
        {
            this.Type = Config.Type;
            return base.OnInitializedAsync();
        }

        public string ComputedColor
        {
            get
            {
                if (Config.IsDark && string.IsNullOrEmpty(Config.Color))
                {
                    return "#323d6f";
                }

                return Config.Color;
            }
        }
    }
}
