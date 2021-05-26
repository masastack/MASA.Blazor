using BlazorComponent;
using MASA.Blazor;
using MASA.Blazor.Model;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMasaBlazor(this IServiceCollection services, MasaBlazorOptions options = null)
        {
            InitBlazorComponentVariables(options);

            services.AddBlazorComponent();
            return services;
        }

        public static IServiceCollection AddMasaBlazor(this IServiceCollection services, Action<MasaBlazorOptionsBuilder> builderAction)
        {
            var builder = new MasaBlazorOptionsBuilder(services);
            builderAction?.Invoke(builder);

            InitBlazorComponentVariables(builder.Options);

            services.AddBlazorComponent();
            return services;
        }

        private static void InitBlazorComponentVariables(MasaBlazorOptions options)
        {
            options ??= new MasaBlazorOptions();

            Variables.DarkTheme = options.DarkTheme;
            Variables.Theme = options.Theme;
        }
    }
}
