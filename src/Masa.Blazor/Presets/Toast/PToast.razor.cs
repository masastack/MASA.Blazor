using BlazorComponent.Abstracts;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Concurrent;

namespace Masa.Blazor.Presets
{
    public partial class PToast
    {
        [Parameter]
        public ToastPosition Position { get; set; } = ToastPosition.BottomRight;

        [Parameter]
        public int? Duration { get; set; } = 4000;

        [Parameter]
        public int MaxCount { get; set; }

        [Parameter]
        public EventCallback<ToastConfig> OnClose { get; set; }

        private readonly Dictionary<string, ToastConfig> _configDict = new();

        private readonly List<ToastConfig> _configs = new();

        private ComponentCssProvider CssProvider { get; } = new();

        protected override Task OnInitializedAsync()
        {
            SetComponentClass();

            return base.OnInitializedAsync();
        }

        private void SetComponentClass()
        {
            CssProvider.Apply((cssBuilder) =>
            {
                cssBuilder.Add("m-toast-container");

            }, (styleBuilder) =>
            {
                styleBuilder
                    .AddIf("top: 1rem; left: 1rem;", () => Position == ToastPosition.TopLeft)
                    .AddIf("top: 1rem; right: 1rem;", () => Position == ToastPosition.TopRight)
                    .AddIf("bottom: 1rem; left: 1rem;", () => Position == ToastPosition.BottomLeft)
                    .AddIf("bottom: 1rem; right: 1rem;", () => Position == ToastPosition.BottomRight);
            });
        }

        public async Task AddToast(ToastConfig config)
        {
            config.Duration ??= Duration;
            if (MaxCount > 0)
            {
                var count = _configDict.Count;
                if (count >= MaxCount)
                {
                    var removeConfig = _configs[0];
                    await RemoveItem(removeConfig);
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
                _configDict.Add(config.Key, config);
                _configs.Add(config);
            }

            StateHasChanged();
        }

        protected async Task HandleOnCloseAsync(ToastConfig config)
        {
            await RemoveItem(config);
        }

        private async Task RemoveItem(ToastConfig config)
        {
            if (_configDict.ContainsKey(config.Key))
            {
                config.Visible = false;
                config.OnClose?.Invoke(config.Key);
                await InvokeAsync(StateHasChanged);

                _configDict.Remove(config.Key, out _);
                _configs.Remove(config);
                await InvokeAsync(StateHasChanged);
            }

            if (OnClose.HasDelegate)
                await OnClose.InvokeAsync(config);
        }
    }
}
