using System.Diagnostics;

namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    partial class BluetoothDevice
    {
        private readonly Android.Bluetooth.BluetoothDevice _device;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private RemoteGattServer _gattServer;

        private BluetoothDevice(Android.Bluetooth.BluetoothDevice device)
        {
            _device = device;
        }

        public static implicit operator Android.Bluetooth.BluetoothDevice(BluetoothDevice device)
        {
            return device._device;
        }

        public static implicit operator BluetoothDevice(Android.Bluetooth.BluetoothDevice device)
        {
            return device == null ? null : new BluetoothDevice(device);
        }

        private static async Task<BluetoothDevice> PlatformFromId(string id)
        {
            return Android.Bluetooth.BluetoothAdapter.DefaultAdapter.GetRemoteDevice(id);
        }

        string GetId()
        {
            return _device.Address;
        }

        string GetName()
        {
            return _device.Name;
        }

        RemoteGattServer GetGatt()
        {
            if (_gattServer is null)
            {
                _gattServer = new RemoteGattServer(this);
            }

            return _gattServer;
        }

        public void Disconnect()
        {
            _gattServer=null;
        }
    }
}
