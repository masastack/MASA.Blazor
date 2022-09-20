using Masa.Blazor.MAUI.Bluetooth;
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
#if ANDROID
        builder.Services.AddSingleton<MasaMauiBluetoothService>();
#endif
        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddMasaBlazor();
        return builder.Build();
	}
}
