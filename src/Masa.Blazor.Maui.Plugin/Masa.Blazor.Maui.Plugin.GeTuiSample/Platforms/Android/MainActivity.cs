using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Masa.Blazor.Maui.Plugin.GeTuiSample
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Com.Igexin.Sdk.PushManager.Instance.Initialize(this);
            var cid = Com.Igexin.Sdk.PushManager.Instance.GetClientid(this);
            System.Diagnostics.Debug.WriteLine($"cid:{cid}");
        }
    }
}