using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Masa.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            #region Masa options
            builder.Services.AddMasaBlazor(builder =>
            {
                builder.UseTheme(option =>
                {
                    option.Primary = "#4318FF";
                    option.Accent = "#4318FF";
                }
                );
            }).AddI18nForServer("wwwroot/i18n");

            #endregion

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, DemoAuthenticationStateProvider>();

            builder.Services.AddOptions();
            builder.Services.AddBlazorWebView();
            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddGlobalForServer();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            return builder.Build();
        }
    }
}