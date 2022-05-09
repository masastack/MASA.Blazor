using BlazorComponent.Abstracts;
using Masa.Blazor.Presets;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Concurrent;

namespace Masa.Blazor.Popup.Components
{
    public partial class Toast : IDisposable
    {
        [Inject]
        private IPopupService PopupService { get; set; }

        private PToast _toastRef { get; set; }

        private int? _duration = 4000;

        private int _maxCount = 0;

        private ToastPosition _position = ToastPosition.BottomRight;

        protected override Task OnInitializedAsync()
        {
            if (PopupService != null)
            {
                PopupService.OnToastOpening += NotifyAsync;
                PopupService.OnToastConfig += Config;
            }

            return base.OnInitializedAsync();
        }

        private void Config(ToastGlobalConfig globalConfig)
        {
            _duration = globalConfig.Duration;
            _maxCount = globalConfig.MaxCount;
            _position = globalConfig.Position;

            StateHasChanged();
        }

        private Task NotifyAsync(ToastConfig config)
        {
            return _toastRef.AddToast(config);
        }

        public void Dispose()
        {
            PopupService.OnToastOpening -= NotifyAsync;
            PopupService.OnToastConfig -= Config;
        }
    }
}
