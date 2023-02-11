using Android.Bluetooth;
using Android.Bluetooth.LE;

namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    public static partial class MasaMauiBluetoothService
    {
        public static BluetoothManager _manager = (BluetoothManager)Android.App.Application.Context.GetSystemService(Android.App.Application.BluetoothService);
        private static DateTime time1;
        private static BluetoothAdapter _bluetoothAdapter = _manager?.Adapter;
        private static ScanSettings _settings = new ScanSettings.Builder()
            .SetScanMode(Android.Bluetooth.LE.ScanMode.Balanced)
            ?.Build();
        private static DevicesCallback _callback = new DevicesCallback();
        public static bool PlatformIsEnabled()
        {
            return _bluetoothAdapter is { IsEnabled: true };
        }

        private static async Task<IReadOnlyCollection<BluetoothDevice>> PlatformScanForDevices(string deviceName = "")
        {
            time1 = DateTime.Now;
            _bluetoothAdapter.BluetoothLeScanner.StartScan(null, _settings, _callback);

            if (string.IsNullOrEmpty(deviceName))
            {
                await Task.Run(() =>
                {
                    _callback.WaitOne();
                });
            }
            else
            {
                await Task.Run(() =>
                {
                    _callback.WaitOnlyOne(deviceName);
                });
            }


            _bluetoothAdapter.BluetoothLeScanner.StopScan(_callback);
            _discoveredDevices = _callback.Devices.AsReadOnly();

            return _discoveredDevices;
        }


        public static async Task PlatformSendDataAsync(string deviceName, Guid servicesUuid, Guid? characteristicsUuid, byte[] dataBytes, EventHandler<GattCharacteristicValueChangedEventArgs> gattCharacteristicValueChangedEventArgs)
        {
            BluetoothDevice blueDevice = _discoveredDevices.FirstOrDefault(o => o.Name == deviceName);
            if (!blueDevice.Gatt.IsConnected)
            {
                await blueDevice.Gatt.ConnectAsync();
            }
            var primaryServices = await blueDevice.Gatt.GetPrimaryServicesAsync();
            var primaryService = primaryServices.First(o => o.Uuid.Value == servicesUuid);

            var characteristics = await primaryService.GetCharacteristicsAsync();
            var characteristic = characteristics.FirstOrDefault(o => (o.Properties & GattCharacteristicProperties.Write) != 0);
            if (characteristicsUuid != null)
            {
                characteristic = characteristics.FirstOrDefault(o => o.Uuid.Value == characteristicsUuid);
            }

            //await characteristic.StopNotificationsAsync();
            var reTryCount = 5;
            var isFailed = false;
            while (reTryCount > 0)
            {

                try
                {
                    await characteristic.StartNotificationsAsync();
                    reTryCount = 0;
                }
                catch (OperationCanceledException ex)
                {
                    reTryCount--;
                    if (reTryCount == 0)
                    {
                        isFailed = true;
                    }
                }
                await Task.Delay(500);
            }

            if (isFailed)
            {
                throw new OperationCanceledException();
            }

            characteristic.CharacteristicValueChanged += gattCharacteristicValueChangedEventArgs;
            await characteristic.WriteValueWithResponseAsync(dataBytes);
        }

        public static async Task<PermissionStatus> PlatformCheckAndRequestBluetoothPermission()
        {
            var status = await Permissions.CheckStatusAsync<BluetoothPermissions>();

            status = await Permissions.RequestAsync<BluetoothPermissions>();

            return status;
        }
        private class DevicesCallback : ScanCallback
        {
            private readonly EventWaitHandle _eventWaitHandle = new(false, EventResetMode.AutoReset);

            public List<BluetoothDevice> Devices { get; } = new();
            private string scanDeviceName;
            public void WaitOne()
            {
                Task.Run(async () =>
                {
                    await Task.Delay(7000);
                    _eventWaitHandle.Set();
                });

                _eventWaitHandle.WaitOne();
            }

            public void WaitOnlyOne(string deviceName)
            {
                scanDeviceName = deviceName;
                _eventWaitHandle.WaitOne();
            }

            public override void OnBatchScanResults(IList<ScanResult> results)
            {
                System.Diagnostics.Debug.WriteLine("OnBatchScanResults");

                base.OnBatchScanResults(results);
            }

            public override void OnScanResult(ScanCallbackType callbackType, ScanResult result)
            {

                System.Diagnostics.Debug.WriteLine("OnScanResult");
                if (string.IsNullOrEmpty(scanDeviceName))
                {
                    if (!Devices.Contains(result.Device))
                    {
                        Devices.Add(result.Device);
                    }
                }
                else
                {
                    if (result.Device?.Name == scanDeviceName)
                    {
                        Devices.Add(result.Device);
                        _eventWaitHandle.Set();
                    }
                    else
                    {
                        if (time1 > DateTime.MinValue && (DateTime.Now - time1).Seconds > 10)
                        {
                            _eventWaitHandle.Set();
                            time1 = DateTime.MinValue;
                        }
                    }
                }

                base.OnScanResult(callbackType, result);
            }

            public override void OnScanFailed(ScanFailure errorCode)
            {
                System.Diagnostics.Debug.WriteLine("OnBatchScanResults");

                _eventWaitHandle.Set();

                base.OnScanFailed(errorCode);
            }
        }


        private class BluetoothPermissions : Permissions.BasePlatformPermission
        {
            public override (string androidPermission, bool isRuntime)[] RequiredPermissions
            {
                get
                {
                    var list = new List<(string androidPermission, bool isRuntime)>
                    {
                        
                    };
                    if (OperatingSystem.IsAndroidVersionAtLeast(32))
                    {
                        list.Add(new(global::Android.Manifest.Permission.BluetoothConnect, true));
                        list.Add(new(global::Android.Manifest.Permission.BluetoothScan, true));
                        list.Add(new(global::Android.Manifest.Permission.BluetoothAdvertise, true));
                    }
                    else
                    {
                        list.Add(new(global::Android.Manifest.Permission.Bluetooth, true));
                        list.Add(new(global::Android.Manifest.Permission.AccessFineLocation, true));
                        list.Add(new(global::Android.Manifest.Permission.BluetoothAdmin, true));
                    }
                    return list.ToArray();
                }
            }
        }
        static Task<bool> PlatformGetAvailability()
        {
            return Task.FromResult(BluetoothAdapter.DefaultAdapter.IsEnabled);
        }

        private static bool _oldAvailability;

        private static async void AddAvailabilityChanged()
        {
            _oldAvailability = await PlatformGetAvailability();
        }

        private static void RemoveAvailabilityChanged()
        {
        }
    }
}