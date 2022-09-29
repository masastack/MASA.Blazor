using CoreBluetooth;
using CoreFoundation;
using Foundation;
namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    // All the code in this file is only included on iOS.
    public static partial class MasaMauiBluetoothService
    {

        private static BluetoothDelegate _delegate = new();
        public static CBCentralManager _manager = new CBCentralManager(_delegate, DispatchQueue.DefaultGlobalQueue, new CBCentralInitOptions
        {
            ShowPowerAlert = true,
        });
        
        public static bool PlatformIsEnabledIsEnabled()
        {
            return _manager.State == CBManagerState.PoweredOn;
        }


        public static async Task<PermissionStatus> PlatformCheckAndRequestBluetoothPermission()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<BluetoothPermissions>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            status = await Permissions.RequestAsync<BluetoothPermissions>();
               
            return status;
        }

        private static async Task<IReadOnlyCollection<BluetoothDevice>> PlatformScanForDevices()
        {
            if (!_manager.IsScanning)
            {
                _manager.ScanForPeripherals(new CBUUID[] { }, new PeripheralScanningOptions
                {
                    AllowDuplicatesKey = true
                });

                await Task.Run(() => { _delegate.WaitOne(); });

                _manager.StopScan();
                _discoveredDevices = _delegate.Devices.AsReadOnly();
            }


            return _discoveredDevices;
        }

        private  class BluetoothDelegate : CBCentralManagerDelegate
        {
            private  EventWaitHandle _eventWaitHandle = new(false, EventResetMode.AutoReset);

            public  List<BluetoothDevice> Devices { get; } = new();

            public  void WaitOne()
            {
                Task.Run(async () =>
                {
                    await Task.Delay(5000);
                    _eventWaitHandle.Set();
                });

                _eventWaitHandle.WaitOne();
            }

            public  override void DiscoveredPeripheral(CBCentralManager central, CBPeripheral peripheral,
                NSDictionary advertisementData,
                NSNumber RSSI)
            {
                System.Diagnostics.Debug.WriteLine("OnScanResult");
                if (!Devices.Contains(peripheral))
                {
                    Devices.Add(peripheral);
                }
            }
            [Preserve]
            public override void UpdatedState(CBCentralManager central)
            {
                
            }
        }


        private  class BluetoothPermissions : Permissions.BasePlatformPermission
        {
            
            protected override Func<IEnumerable<string>> RequiredInfoPlistKeys
                =>
                    () => new string[] { "NSBluetoothAlwaysUsageDescription", "NSBluetoothPeripheralUsageDescription" };

            public override Task<PermissionStatus> CheckStatusAsync()
            {
                EnsureDeclared();

                return Task.FromResult(GetBleStatus());
            }

            private PermissionStatus GetBleStatus()
            {
                var status = _manager.State;
                return status switch
                {
                    CBManagerState.PoweredOn=> PermissionStatus.Granted,
                    CBManagerState.Unauthorized => PermissionStatus.Denied,
                    CBManagerState.Resetting => PermissionStatus.Restricted,
                    _ => PermissionStatus.Unknown,
                };
            }
        }

    }
}