namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    public sealed partial class BluetoothDevice
    {
        public string Id { get { return GetId(); } }
        public string Name { get { return GetName(); } }
        public string LocalName { get; set; }
        public RemoteGattServer Gatt { get { return GetGatt(); } }

        internal void OnGattServerDisconnected()
        {
            GattServerDisconnected?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Fired when an active GATT connection is lost.
        /// </summary>
        public event EventHandler GattServerDisconnected;

        public override bool Equals(object obj)
        {
            BluetoothDevice device = obj as BluetoothDevice;
            if (device != null)
            {
                return Id.Equals(device.Id);
            }

            return base.Equals(obj);
        }
    }
}
