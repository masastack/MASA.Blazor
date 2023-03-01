using Foundation;
using UIKit;
using UserNotifications;

namespace iOSPushSample
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            UNUserNotificationCenter center = UNUserNotificationCenter.Current;

            var options = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Sound | UNAuthorizationOptions.CriticalAlert;
            center.RequestAuthorization(options,
                (bool success, NSError error) =>
                {
                }
            );
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
            return base.FinishedLaunching(application, launchOptions);
        }

        [Export("application:didReceiveRemoteNotification:fetchCompletionHandler:")]
        public void DidReceiveRemoteNotification(UIKit.UIApplication application, NSDictionary userInfo, Action<UIKit.UIBackgroundFetchResult> completionHandler)
        {
            foreach (var item in userInfo)
            {
                Console.WriteLine($"Key:{item.Key},Value:{item.Value}");
                var alertMsg = ((NSDictionary)item.Value)["alert"];
                Console.WriteLine($"alertMsg:{alertMsg}");
            }
            Console.WriteLine("DidReceiveRemoteNotification");
        }

        [Export("application:didRegisterForRemoteNotificationsWithDeviceToken:")]
        public void RegisteredForRemoteNotifications(UIKit.UIApplication application,
            Foundation.NSData deviceToken)
        {
            var token = ExtractToken(deviceToken);
            Preferences.Default.Set("PushToken", token);
            Console.WriteLine(token);
        }

        [Export("application:didFailToRegisterForRemoteNotificationsWithError:")]
        public void FailedToRegisterForRemoteNotifications(UIKit.UIApplication application, NSError error)
        {
            Console.WriteLine("FailedToRegisterForRemoteNotifications");
        }
        private string ExtractToken(NSData deviceToken)
        {
            if (deviceToken.Length == 0)
                return null;
            var result = new byte[deviceToken.Length];
            System.Runtime.InteropServices.Marshal.Copy(deviceToken.Bytes, result, 0, (int)deviceToken.Length);
            return BitConverter.ToString(result).Replace("-", "");
        }
    }
}