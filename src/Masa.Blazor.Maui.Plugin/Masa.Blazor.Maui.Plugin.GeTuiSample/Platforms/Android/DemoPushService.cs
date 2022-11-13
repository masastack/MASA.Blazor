using Android.App;

namespace Masa.Blazor.Maui.Plugin.GeTuiSample
{
    [Service(Process = ":pushservice", Exported = false)]
    public class DemoPushService : Com.Igexin.Sdk.PushService
    {
    }
}
