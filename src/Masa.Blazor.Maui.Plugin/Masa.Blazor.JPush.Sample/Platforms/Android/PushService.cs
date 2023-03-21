using Android.App;

namespace Masa.Blazor.JPush.Sample.PlatformsAndroid
{
    [Service(Process = ":pushcore", Exported = true)]
    [IntentFilter(new string[] { "cn.jiguang.user.service.action" })]
    public class PushService : JCommonService
    {
    }
}
