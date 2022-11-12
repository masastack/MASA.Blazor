using CoreBluetooth;
using CoreFoundation;
using Foundation;
using System.Diagnostics;

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
        private static bool availability = false;
        internal static event EventHandler UpdatedState;
        internal static event EventHandler<CBPeripheralEventArgs> ConnectedPeripheral;
        internal static event EventHandler<CBPeripheralErrorEventArgs> DisconnectedPeripheral;
        internal static event EventHandler<CBDiscoveredPeripheralEventArgs> DiscoveredPeripheral;
        internal static event EventHandler<CBPeripheralErrorEventArgs> FailedToConnectPeripheral;

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
                    AllowDuplicatesKey = true,
                });

                await Task.Run(() => { _delegate.WaitOne(); });

                _manager.StopScan();
                _discoveredDevices = _delegate.Devices.AsReadOnly();
            }
            
            return _discoveredDevices;
        }

        public static async Task PlatformSendDataAsync(string deviceName, Guid servicesUuid, Guid? characteristicsUuid, byte[] dataBytes, EventHandler<GattCharacteristicValueChangedEventArgs> gattCharacteristicValueChangedEventArgs)
        {
            BluetoothDevice blueDevice = _discoveredDevices.FirstOrDefault(o => o.Name == deviceName);

            var primaryServices = await blueDevice.Gatt.GetPrimaryServicesAsync();
            var primaryService = primaryServices.First(o => o.Uuid.Value == servicesUuid);

            var characteristics = await primaryService.GetCharacteristicsAsync();
            var characteristic = characteristics.FirstOrDefault(o => (o.Properties & GattCharacteristicProperties.Write) != 0);
            if (characteristicsUuid != null)
            {
                characteristic = characteristics.FirstOrDefault(o => o.Uuid.Value == characteristicsUuid);
            }

            await characteristic.StartNotificationsAsync();
            characteristic.CharacteristicValueChanged += gattCharacteristicValueChangedEventArgs;
            await characteristic.WriteValueWithResponseAsync(dataBytes);
        }

        private class BluetoothDelegate : CBCentralManagerDelegate
        {
            private EventWaitHandle _eventWaitHandle = new(false, EventResetMode.AutoReset);

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
                System.Diagnostics.Debug.WriteLine($"{peripheral.Name}");
            
                //var sss = $"{string.Join(",", advertisementData.Keys.ToList())}";
                var device = (BluetoothDevice) peripheral;
                if (!Devices.Contains(device))
                {
                    Devices.Add(device);
                    //if (sss.Contains("LocalName"))
                    //{
                    //    System.Diagnostics.Debug.WriteLine(123);
                    //    device.LocalName = advertisementData["kCBAdvDataLocalName"].ToString();
                    //    System.Diagnostics.Debug.WriteLine("device.LocalName:"+ device.LocalName);
                    //}

                }
 
                System.Diagnostics.Debug.WriteLine($"{String.Join(",", Devices.Select(o => o.Name).Distinct().ToList())}");
            }
            public override void UpdatedState(CBCentralManager central)
            {
                System.Diagnostics.Debug.WriteLine($"UpdatedState,{central.State}");
                Debug.WriteLine(central.State);
                MasaMauiBluetoothService.UpdatedState?.Invoke(central, EventArgs.Empty);

                bool newAvailability = false;

#if !__WATCHOS__
                switch (central.State)
                {
#if NET6_0_OR_GREATER
                    case CBManagerState.PoweredOn:
                    case CBManagerState.Resetting:
#else
                    case CBCentralManagerState.PoweredOn:
                    case CBCentralManagerState.Resetting:
#endif
                        newAvailability = true;
                        break;

                    default:
                        newAvailability = false;
                        break;
                }
#endif

                if (availability != newAvailability)
                {
                    availability = newAvailability;
                    OnAvailabilityChanged();
                }
            }
            public override void ConnectedPeripheral(CBCentralManager central, CBPeripheral peripheral)
            {
                System.Diagnostics.Debug.WriteLine($"Connected {peripheral.Identifier}");
                MasaMauiBluetoothService.ConnectedPeripheral?.Invoke(central, new CBPeripheralEventArgs(peripheral));
            }

            public override void DisconnectedPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error)
            {
                System.Diagnostics.Debug.WriteLine($"Disconnected {peripheral.Identifier} {error}");
                MasaMauiBluetoothService.DisconnectedPeripheral?.Invoke(central, new CBPeripheralErrorEventArgs(peripheral, error));
            }
            public override void FailedToConnectPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to connect {peripheral.Identifier} {error.Code}");
                MasaMauiBluetoothService.FailedToConnectPeripheral?.Invoke(central, new CBPeripheralErrorEventArgs(peripheral, error));
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

        private static bool _oldAvailability;
        static Task<bool> PlatformGetAvailability()
        {
            bool available = false;

            switch (_manager.State)
            {
#if NET6_0_OR_GREATER || __WATCHOS__
                case CBManagerState.PoweredOn:
                case CBManagerState.Resetting:
#else
                case CBCentralManagerState.PoweredOn:
                case CBCentralManagerState.Resetting:             
#endif
                    available = true;
                    break;
            }

            return Task.FromResult(available);
        }
        private static async void AddAvailabilityChanged()
        {
            _oldAvailability = await PlatformGetAvailability();
            _manager.UpdatedState += _manager_UpdatedState;
        }

        private static void RemoveAvailabilityChanged()
        {
            _manager.UpdatedState -= _manager_UpdatedState;
        }

        private static async void _manager_UpdatedState(object sender, EventArgs e)
        {
            bool newAvailability = await PlatformGetAvailability();
            if (newAvailability != _oldAvailability)
            {
                _oldAvailability = newAvailability;
                OnAvailabilityChanged();
            }
        }
    }
}