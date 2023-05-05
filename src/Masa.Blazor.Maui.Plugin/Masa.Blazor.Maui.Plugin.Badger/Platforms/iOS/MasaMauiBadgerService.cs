using Foundation;
using UIKit;
using UserNotifications;
namespace Masa.Blazor.Maui.Plugin.Badger
{
	public static partial class MasaMauiBadgerService
	{
        private static void PlatformSetNotificationCount(int count)

		{
			// Requests the user’s authorization to allow local and remote notifications for your app.
			UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Badge, (r, e) =>{});

			// The number currently set as the badge of the app icon on the Home screen
			// Set to 0 (zero) to hide the badge number. The default value of this property is 0.
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = count;
        }
	}
}