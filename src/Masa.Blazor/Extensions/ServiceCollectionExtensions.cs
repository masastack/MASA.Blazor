using BlazorComponent.Web;
using Masa.Blazor;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMasaBlazor(this IServiceCollection services, MasaBlazorOptions options = null)
        {
            InitBlazorComponentVariables(options);

            services.AddBlazorComponent();
            services.TryAddSingleton<IExceptionFilterProvider, ExceptionFilterProvider>();
            services.TryAddScoped<MasaBlazor>();
            services.TryAddScoped<Application>();
            services.TryAddScoped(serviceProvider => new Breakpoint(serviceProvider.GetService<Window>())
            {
                MobileBreakpoint = 1264,
                ScrollBarWidth = 16,
                Thresholds = new BreakpointThresholds
                {
                    Xs = 600,
                    Sm = 960,
                    Md = 1280,
                    Lg = 1920
                }
            });
            services.TryAddScoped<IPopupService, PopupService>();
            services.AddSingleton<IAbstractComponentTypeMapper, MasaBlazorComponentTypeMapper>();

            return services;
        }

        public static IServiceCollection AddMasaBlazor(this IServiceCollection services, Action<MasaBlazorOptionsBuilder> builderAction)
        {
            var builder = new MasaBlazorOptionsBuilder(services);
            builderAction?.Invoke(builder);

            return services.AddMasaBlazor(builder.Options);
        }

        private static void InitBlazorComponentVariables(MasaBlazorOptions options)
        {
            options ??= new MasaBlazorOptions();

            Variables.DarkTheme = options.DarkTheme;
            Variables.Theme = options.Theme;
        }
    }
}
