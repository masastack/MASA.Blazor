using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    public static partial class MasaMauiBluetoothService
    {
        private static IReadOnlyCollection<BluetoothDevice> _discoveredDevices;
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
    }
}
