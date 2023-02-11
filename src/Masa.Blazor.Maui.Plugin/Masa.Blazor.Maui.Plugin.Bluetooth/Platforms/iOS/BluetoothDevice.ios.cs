using CoreBluetooth;
namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    partial class BluetoothDevice
    {
        private CBPeripheral _peripheral;
        private RemoteGattServer _gatt;

        private BluetoothDevice(CBPeripheral peripheral)
        {
            _peripheral = peripheral;
        }

        public static implicit operator BluetoothDevice(CBPeripheral peripheral)
        {
            return peripheral == null ? null : new BluetoothDevice(peripheral);
        }

        public static implicit operator CBPeripheral(BluetoothDevice device)
        {
            return device._peripheral;
        }

        string GetId()
        {
            return _peripheral.Identifier.ToString();
        }

        string GetName()
        {
            return _peripheral.Name;
        }

        RemoteGattServer GetGatt()
        {
            if (_gatt == null)
            {
                _gatt = new RemoteGattServer(this);
            }

            return _gatt;
        }

        public void Disconnect()
        {
            _gatt = null;
        }

#if __IOS__
        /// <summary>
        /// Specifies whether to request Apple Notification Center Services to allow access to notifications from the remote device when connecting.
        /// </summary>
        public bool RequiresAncs
        {
            get; set;
        }
#endif
    }
}
