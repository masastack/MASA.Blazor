namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    public static partial class MasaMauiBluetoothService
    {
        private static IReadOnlyCollection<BluetoothDevice> _discoveredDevices;
        private static event EventHandler availabilityChanged;
        public static event EventHandler AvailabilityChanged
        {
            add
            {
                if (availabilityChanged == null)
                {
                    AddAvailabilityChanged();
                }

                availabilityChanged += value;
            }
            remove
            {
                availabilityChanged -= value;

                if (availabilityChanged == null)
                {
                    RemoveAvailabilityChanged();
                }
            }
        }
        public static Task<IReadOnlyCollection<BluetoothDevice>> ScanForDevicesAsync()
        {
            return PlatformScanForDevices();
        }

        public static bool IsEnabled()
        {
            return PlatformIsEnabledIsEnabled();
        }

        public static async Task<PermissionStatus> CheckAndRequestBluetoothPermission()
        {
            return await PlatformCheckAndRequestBluetoothPermission();
        }

        private static void OnAvailabilityChanged()
        {
            availabilityChanged?.Invoke(null, EventArgs.Empty); ;
        }
    }
}
