using BlazorComponent;
using Microsoft.Extensions.DependencyInjection;

namespace MASA.Blazor
{
    public class MasaBlazorOptionsBuilder
    {
        public MasaBlazorOptionsBuilder(IServiceCollection services)
        {
            Options = new MasaBlazorOptions();
            Services = services;
        }

        internal MasaBlazorOptions Options { get; }

        internal IServiceCollection Services { get; }

        public MasaBlazorOptionsBuilder UseExceptionFilter<TExceptionFilter>()
            where TExceptionFilter : class, IExceptionFilter
        {
            Services.AddSingleton<IExceptionFilter, TExceptionFilter>();
            return this;
        }
        public MasaBlazorOptionsBuilder UseDarkTheme()
        {
            Options.DarkTheme = true;
            return this;
        }

        public MasaBlazorOptionsBuilder UseTheme(ThemeOptions options)
        {
            Options.Theme = options;
            return this;
        }
    }
}
