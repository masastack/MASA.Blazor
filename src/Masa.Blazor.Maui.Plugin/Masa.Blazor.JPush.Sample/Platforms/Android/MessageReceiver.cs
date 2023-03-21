using Android.App;
using Android.Content;

namespace Masa.Blazor.JPush.Sample.PlatformsAndroid
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    [IntentFilter(new string[]
        {
            "cn.jpush.android.intent.RECEIVE_MESSAGE",
        },
        Categories = new string[]
        {
            "com.AppAgent.lsd"
        })]
    public class MessageReceiver : JPushMessageReceiver
    {
    }
}
