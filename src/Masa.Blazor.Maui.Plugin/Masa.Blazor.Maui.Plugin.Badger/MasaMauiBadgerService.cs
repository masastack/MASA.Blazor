namespace Masa.Blazor.Maui.Plugin.Badger
{
    // All the code in this file is included in all platforms.
    public static partial class MasaMauiBadgerService
    {
        public static void SetNotificationCount(int count)
        {
            PlatformSetNotificationCount(count);
        }
    }
}