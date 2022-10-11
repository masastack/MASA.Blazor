using Android.Bluetooth;
using Android.Bluetooth.LE;

namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    public static partial class MasaMauiBluetoothService
    {
        public static BluetoothManager _manager = (BluetoothManager)Android.App.Application.Context.GetSystemService(Android.App.Application.BluetoothService);
        private static BluetoothAdapter _bluetoothAdapter = _manager?.Adapter;
        private static ScanSettings _settings = new ScanSettings.Builder()
            .SetScanMode(Android.Bluetooth.LE.ScanMode.Balanced)
            ?.Build();
        private static DevicesCallback _callback = new DevicesCallback();
        
        public static bool PlatformIsEnabledIsEnabled()
        {
            return _bluetoothAdapter is {IsEnabled: true};
        }

        private static async Task<IReadOnlyCollection<BluetoothDevice>> PlatformScanForDevices()
        {
            _bluetoothAdapter.BluetoothLeScanner.StartScan(null, _settings, _callback);

            await Task.Run(() =>
            {
                _callback.WaitOne();
            });

            _bluetoothAdapter.BluetoothLeScanner.StopScan(_callback);
            _discoveredDevices = _callback.Devices.AsReadOnly();

            return _discoveredDevices;
        }

        public static async Task SendDataAsync(string deviceName,Guid servicesUuid,Guid? characteristicsUuid, byte[] dataBytes, EventHandler<GattCharacteristicValueChangedEventArgs> gattCharacteristicValueChangedEventArgs)
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

            public void WaitOne()
            {
                Task.Run(async () =>
                {
                    await Task.Delay(5000);
                    _eventWaitHandle.Set();
                });

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

                if (!Devices.Contains(result.Device))
                {
                    Devices.Add(result.Device);
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
            public override (string androidPermission, bool isRuntime)[] RequiredPermissions =>
                new List<(string androidPermission, bool isRuntime)>
                {
                    (global::Android.Manifest.Permission.AccessFineLocation, true),
                    (global::Android.Manifest.Permission.Bluetooth, true),
                    (global::Android.Manifest.Permission.BluetoothAdmin, true),
                }.ToArray();
        }
    }
}