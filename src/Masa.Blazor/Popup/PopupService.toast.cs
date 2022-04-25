using Masa.Blazor.Popup.Components;
using OneOf;

namespace Masa.Blazor
{
    public partial class PopupService
    {
        public event Action<ToastGlobalConfig> OnToastConfig;
        public event Func<ToastConfig, Task> OnToastOpening;

        private Task Open(ToastConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (OnToastOpening != null)
            {
                return OnToastOpening.Invoke(config);
            }

            return Task.CompletedTask;
        }

        public Task ToastAsync(string title, AlertTypes type)
        {
            return PreOpen(title, type);
        }

        public Task ToastAsync(ToastConfig config)
        {
            return PreOpen(config);
        }

        public Task ToastAsync(Action<ToastConfig> configAction)
        {
            return PreOpen(configAction);
        }

        public Task ToastSuccessAsync(string title)
        {
            return PreOpen(title, AlertTypes.Success);
        }

        public Task ToastErrorAsync(string title)
        {
            return PreOpen(title, AlertTypes.Error);
        }

        public Task ToastInfoAsync(string title)
        {
            return PreOpen(title, AlertTypes.Info);
        }

        public Task ToastWarningAsync(string title)
        {
            return PreOpen(title, AlertTypes.Warning);
        }

        private Task PreOpen(string title, AlertTypes type)
        {
            ToastConfig config = new();
            config.Title = title;
            config.Type = type;
            return Open(config);
        }

        private Task PreOpen(OneOf<ToastConfig, Action<ToastConfig>> content)
        {
            ToastConfig config;

            if (content.IsT1)
            {
                config = new();

                var configAction = content.AsT1;

                configAction?.Invoke(config);
            }
            else
            {
                config = content.AsT0;
            }

            return Open(config);
        }

        public Task Config(ToastGlobalConfig config)
        {
            return GlobalConfig(config);
        }
        public Task Config(Action<ToastGlobalConfig> configAcion)
        {
            return GlobalConfig(configAcion);
        }

        private Task GlobalConfig(OneOf<ToastGlobalConfig, Action<ToastGlobalConfig>> configParam)
        {
            ToastGlobalConfig config = null;

            if (configParam.IsT0)
            {
                config = configParam.AsT0;
            }
            else if (configParam.AsT1 != null)
            {
                config = new();
                configParam.AsT1.Invoke(config);
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            OnToastConfig?.Invoke(config);

            return Task.CompletedTask;
        }
    }
}
