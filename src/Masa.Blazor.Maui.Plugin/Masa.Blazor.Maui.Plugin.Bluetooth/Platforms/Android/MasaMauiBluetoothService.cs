using Android.Bluetooth;
using Android.Bluetooth.LE;

namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    public class MasaMauiBluetoothService
    {
        private readonly BluetoothManager _bluetoothManager;
        private readonly BluetoothAdapter _bluetoothAdapter;
        private readonly ScanSettings _settings;
        private readonly DevicesCallback _callback;
        public MasaMauiBluetoothService()
        {
            _bluetoothManager = (BluetoothManager)Android.App.Application.Context.GetSystemService(Android.App.Application.BluetoothService);
            _bluetoothAdapter = _bluetoothManager?.Adapter;
               _settings = new ScanSettings.Builder()
                .SetScanMode(Android.Bluetooth.LE.ScanMode.Balanced)
                ?.Build();
            _callback = new DevicesCallback();
        }

        public bool IsEnabled()
        {
            return _bluetoothAdapter is {IsEnabled: true};
        }
        public async Task<IReadOnlyCollection<BluetoothDevice>> ScanLeDeviceAsync()
        {
            //第一个参数可以设置过滤条件-蓝牙名称，名称前缀，服务号等,这里暂时不设置过滤条件
            _bluetoothAdapter.BluetoothLeScanner.StartScan(null, _settings, _callback);

            await Task.Run(() =>
            {
                _callback.WaitOne();
            });

            _bluetoothAdapter.BluetoothLeScanner.StopScan(_callback);


            return _callback.Devices.AsReadOnly();
        }
        
        public async Task<bool> CheckAndRequestBluetoothPermission()
        {
            var status = await Permissions.CheckStatusAsync<BluetoothPermissions>();

            if (status == PermissionStatus.Granted)
                return true;
            status = await Permissions.RequestAsync<BluetoothPermissions>();

            if (status == PermissionStatus.Granted)
                return true;
            return false;
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
                    //(global::Android.Manifest.Permission.AccessCoarseLocation, true),
                    (global::Android.Manifest.Permission.AccessFineLocation, true),
                    (global::Android.Manifest.Permission.Bluetooth, true),
                    (global::Android.Manifest.Permission.BluetoothAdmin, true),
                }.ToArray();
        }
    }
}