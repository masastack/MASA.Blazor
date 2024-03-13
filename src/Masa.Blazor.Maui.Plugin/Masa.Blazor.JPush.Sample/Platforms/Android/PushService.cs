using Android.App;
using CN.Jpush.Android.Service;

namespace Masa.Blazor.JPush.Sample.PlatformsAndroid
{
    [Service(Process = ":pushcore", Exported = true)]
    [IntentFilter(new string[] { "cn.jiguang.user.service.action" })]
    public class PushService : JCommonService
    {
    }
}
