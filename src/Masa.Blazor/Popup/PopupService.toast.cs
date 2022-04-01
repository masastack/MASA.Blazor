using Masa.Blazor.Popup.Components;
using OneOf;

namespace Masa.Blazor
{
    public partial class PopupService
    {
        public event Action<ToastGlobalConfig> OnConfig;
        public event Func<ToastConfig, Task> OnOpening;

        private Task Open(ToastConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (OnOpening != null)
            {
                return OnOpening.Invoke(config);
            }

            return Task.CompletedTask;
        }

        public Task ToastAsync(AlertTypes type, string title)
        {
            return PreOpen(type, title);
        }

        public Task ToastAsync(AlertTypes type, ToastConfig config)
        {
            return PreOpen(type, config);
        }

        public Task ToastAsync(AlertTypes type, Action<ToastConfig> configAction)
        {
            return PreOpen(type, configAction);
        }

        #region Success Api
        public Task ToastSuccessAsync(string title)
        {
            return PreOpen(AlertTypes.Success, title);
        }
        public Task ToastSuccessAsync(ToastConfig config)
        {
            return PreOpen(AlertTypes.Success, config);
        }
        public Task ToastSuccessAsync(Action<ToastConfig> configAction)
        {
            return PreOpen(AlertTypes.Success, configAction);
        }
        #endregion

        #region Error Api
        public Task ToastErrorAsync(string title)
        {
            return PreOpen(AlertTypes.Error, title);
        }
        public Task ToastErrorAsync(ToastConfig config)
        {
            return PreOpen(AlertTypes.Error, config);
        }
        public Task ToastErrorAsync(Action<ToastConfig> configAction)
        {
            return PreOpen(AlertTypes.Error, configAction);
        }
        #endregion

        #region Info Api
        public Task ToastInfoAsync(string title)
        {
            return PreOpen(AlertTypes.Info, title);
        }
        public Task ToastInfoAsync(ToastConfig config)
        {
            return PreOpen(AlertTypes.Info, config);
        }
        public Task ToastInfoAsync(Action<ToastConfig> configAction)
        {
            return PreOpen(AlertTypes.Info, configAction);
        }
        #endregion

        #region Warning Api
        public Task ToastWarningAsync(string title)
        {
            return PreOpen(AlertTypes.Warning, title);
        }
        public Task ToastWarningAsync(ToastConfig config)
        {
            return PreOpen(AlertTypes.Warning, config);
        }
        public Task ToastWarningAsync(Action<ToastConfig> configAction)
        {
            return PreOpen(AlertTypes.Warning, configAction);
        }
        #endregion

        private Task PreOpen(AlertTypes type, OneOf<string, ToastConfig, Action<ToastConfig>> content)
        {
            ToastConfig config;

            if (content.IsT2)
            {
                config = new();

                var configAction = content.AsT2;

                configAction?.Invoke(config);
            }
            else if (content.IsT1)
            {
                config = content.AsT1;
            }
            else
            {
                config = new();

                config.Title = content.AsT0;
            }

            config.Type = type;
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
            else if(configParam.AsT1 != null)
            {
                config = new();
                configParam.AsT1.Invoke(config);
            }

            if(config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            OnConfig?.Invoke(config);

            return Task.CompletedTask;
        }
    }
}
