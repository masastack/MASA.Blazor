namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    public sealed partial class GattDescriptor
    {
        internal GattDescriptor(GattCharacteristic characteristic)
        {
            Characteristic = characteristic;
        }

        /// <summary>
        /// The GATT characteristic this descriptor belongs to.
        /// </summary>
        public GattCharacteristic Characteristic { get; private set; }

        /// <summary>
        /// The UUID of the characteristic descriptor.
        /// </summary>
        public BluetoothUuid Uuid { get { return GetUuid(); } }

        /// <summary>
        /// The currently cached descriptor value. 
        /// This value gets updated when the value of the descriptor is read.
        /// </summary>
        public byte[] Value
        {
            get
            {
                return PlatformGetValue();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<byte[]> ReadValueAsync()
        {
            return PlatformReadValue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task WriteValueAsync(byte[] value)
        {
            return PlatformWriteValue(value);
        }
    }
}
