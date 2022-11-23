using Masa.Blazor.Maui.Plugin.Bluetooth;
using MauiBlueToothDemo.Data;

namespace MauiBlueToothDemo;

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

		builder.Services.AddMauiBlazorWebView();
		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddMasaBlazor();
		//AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
		return builder.Build();
	}

    private static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"********************************** UNHANDLED EXCEPTION! Details: {e.Exception.Message}");
    }
}
