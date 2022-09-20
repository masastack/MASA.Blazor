

namespace Masa.Maui.Plugin.Bluetooth
{
    public class BluetoothDevice
    {
        public string Name => this.GetName();
        private readonly Android.Bluetooth.BluetoothDevice _device;

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
    }
}
