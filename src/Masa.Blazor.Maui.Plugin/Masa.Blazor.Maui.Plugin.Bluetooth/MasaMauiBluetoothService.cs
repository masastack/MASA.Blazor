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
        public static Task<IReadOnlyCollection<BluetoothDevice>> ScanForDevicesAsync(string deviceName = "")
        {
            return PlatformScanForDevices(deviceName);
        }



        public static bool IsEnabled()
        {
            var isEnable = false;
            for (var i = 0; i < 20; i++)
            {
                if (PlatformIsEnabled())
                {
                    isEnable = true;
                    break;
                }

                Thread.Sleep(100);
            }
            return isEnable;

        }

        public static BluetoothDevice GetBluetoothDevice(string name)
        {
            return _discoveredDevices.FirstOrDefault(o => o.Name == name);
        }

        public static async Task<PermissionStatus> CheckAndRequestBluetoothPermission()
        {
            return await PlatformCheckAndRequestBluetoothPermission();
        }

        private static void OnAvailabilityChanged()
        {
            availabilityChanged?.Invoke(null, EventArgs.Empty); ;
        }

        public static async Task SendDataAsync(string deviceName, Guid servicesUuid, Guid? characteristicsUuid,
            byte[] dataBytes,
            EventHandler<GattCharacteristicValueChangedEventArgs> gattCharacteristicValueChangedEventArgs)
        {

            await PlatformSendDataAsync(deviceName, servicesUuid, characteristicsUuid, dataBytes, gattCharacteristicValueChangedEventArgs);
        }
    }
}
