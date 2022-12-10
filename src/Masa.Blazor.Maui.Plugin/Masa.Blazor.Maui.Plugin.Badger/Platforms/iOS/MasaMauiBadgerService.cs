using UIKit;
using UserNotifications;
namespace Masa.Blazor.Maui.Plugin.Badger
{
		// All the code in this file is only included on iOS.
	public static partial class MasaMauiBadgerService
	{
		public static void PlatformSetNotificationCount(int count)
		{
			UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Badge, (r, e) =>{});
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = count;
		}
	}

}