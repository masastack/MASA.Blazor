using Masa.Contrib.Storage.ObjectStorage.Aliyun.Options;

#if ANDROID
using MediaPickSample.PlatformsAndroid;
#endif
#if IOS
using MediaPickSample.PlatformsIOS;
#endif
using MediaPickSample.Service;
using Microsoft.Extensions.Logging;

namespace MediaPickSample
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

            builder.Services.AddMauiBlazorWebView();

#if ANDROID
            builder.Services.AddSingleton<IPhotoPickerService, AndroidPhotoPickerService>();
#endif
#if IOS
builder.Services.AddSingleton<IPhotoPickerService, IOSPhotoPickerService>();
#endif
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddMasaBlazor();
            builder.Services.AddAliyunStorage(() =>
            {
                return new AliyunStorageOptions(
                            "accessKeyId",
                            "accessKeySecret",
                            "oss-cn-hangzhou.aliyuncs.com",
                            "roleArn",
                            "roleSessionName")
                {
                    Sts = new AliyunStsOptions("oss-cn-hangzhou")
                };
 
            });
            return builder.Build();
        }
    }
}