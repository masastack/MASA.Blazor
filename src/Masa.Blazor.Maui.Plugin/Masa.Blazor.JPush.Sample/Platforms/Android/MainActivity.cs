using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using CN.Jpush.Android.Api;
using CN.Jpush.Android.Data;
namespace Masa.Blazor.JPush.Sample
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            JPushInterface.Init(Android.App.Application.Context);
            JPushInterface.SetDebugMode(true);
           var  rid = JPushInterface.GetRegistrationID(Android.App.Application.Context);
        }
    }
}