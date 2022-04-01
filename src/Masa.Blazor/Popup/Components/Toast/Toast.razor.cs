using BlazorComponent.Abstracts;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Concurrent;

namespace Masa.Blazor.Popup.Components
{
    public partial class Toast
    {
        [Inject]
        private IPopupService PopupService { get; set; }

        private readonly List<ToastConfig> _configs = new();

        private readonly ConcurrentDictionary<string, ToastConfig> _configDict = new();

        private int? _duration = 400000;

        private int _maxCount = 0;

        private ToastPosition _position = ToastPosition.BottomRight;
        public ComponentCssProvider CssProvider { get; } = new();

        protected override Task OnInitializedAsync()
        {
            if (PopupService != null)
            {
                PopupService.OnOpening += NotifyAsync;
                PopupService.OnConfig += Config;
            }

            SetComponentClass();

            return base.OnInitializedAsync();
        }

        protected void SetComponentClass()
        {
            CssProvider.Apply((cssbuilder) =>
            {
                cssbuilder.Add("m-toast-container");

            }, (styleBuilder) =>
            {
                styleBuilder.AddIf("top: 1rem; left: 1rem;", () => _position == ToastPosition.TopLeft);
                styleBuilder.AddIf("top: 1rem; right: 1rem;", () => _position == ToastPosition.TopRight);
                styleBuilder.AddIf("bottom: 1rem; left: 1rem;", () => _position == ToastPosition.BottomLeft);
                styleBuilder.AddIf("bottom: 1rem; right: 1rem;", () => _position == ToastPosition.BottomRight);
            });
        }

        private void Config(ToastGlobalConfig globalConfig)
        {
            _duration = globalConfig.Duration;
            _maxCount = globalConfig.MaxCount;
            _position = globalConfig.Position;

            if (_configs.Count > 0)
                InvokeAsync(StateHasChanged);
        }

        private ToastConfig Extend(ToastConfig config)
        {
            config.Duration ??= _duration;
            if (string.IsNullOrWhiteSpace(config.Key))
            {
                config.Key = Guid.NewGuid().ToString();
            }

            return config;
        }

        private Task NotifyAsync(ToastConfig config)
        {
            config = Extend(config);
            if (_maxCount > 0)
            {
                var count = _configDict.Count;
                if (count >= _maxCount)
                {
                    var removeConfig = _configs[0];
                    RemoveItem(removeConfig);
                }
            }

            if (_configDict.ContainsKey(config.Key))
            {
                var oldConfig = _configDict[config.Key];

                oldConfig.Type = config.Type;
                oldConfig.Content = config.Content;
                oldConfig.Duration = config.Duration;
            }
            else
            {
                _configDict.TryAdd(config.Key, config);
                _configs.Add(config);
            }

            return InvokeAsync(StateHasChanged);
        }

        public Task HandleOnCloseAsync(ToastConfig args)
        {
            return RemoveItem(args);
        }

        private Task RemoveItem(ToastConfig config)
        {
            if (_configDict.ContainsKey(config.Key))
            {
                config.Visible = false;
                config.OnClose?.Invoke(config.Key);
                InvokeAsync(StateHasChanged);

                _configDict.TryRemove(config.Key, out _);
                _configs.Remove(config);
                InvokeAsync(StateHasChanged);
            }

            return Task.CompletedTask;
        }
    }
}
