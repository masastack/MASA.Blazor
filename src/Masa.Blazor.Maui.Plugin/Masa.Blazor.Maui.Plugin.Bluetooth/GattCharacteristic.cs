namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    public sealed partial class GattCharacteristic
    {
        internal GattCharacteristic(GattService service)
        {
            Service = service;
        }

        /// <summary>
        /// The GATT service this characteristic belongs to.
        /// </summary>
        public GattService Service { get; private set; }

        /// <summary>
        /// The UUID of the characteristic.
        /// </summary>
        /// <seealso cref="BluetoothUuid"/>
        public BluetoothUuid Uuid { get { return GetUuid(); } }

        /// <summary>
        /// The properties of this characteristic.
        /// </summary>
        /// <seealso cref="GattCharacteristicProperties"/>
        public GattCharacteristicProperties Properties { get { return GetProperties(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used on multiple platforms")]
        private string GetManualUserDescription()
        {
            var descriptor = GetDescriptorAsync(GattDescriptorUuids.CharacteristicUserDescription).Result;

            if (descriptor != null)
            {
                var bytes = descriptor.ReadValueAsync().Result;
                return System.Text.Encoding.UTF8.GetString(bytes);
            }

            return string.Empty;
        }

        /// <summary>
        /// The currently cached characteristic value. 
        /// This value gets updated when the value of the characteristic is read or updated via a notification or indication.
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
            //if (!Service.Device.Gatt.Connected)
            //throw new NetworkException();

            return PlatformReadValue();
        }

        /// <summary>
        /// Performs a Characteristic Value write to a Bluetooth LE device.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task WriteValueWithResponseAsync(byte[] value)
        {
            ThrowOnInvalidValue(value);
            return PlatformWriteValue(value, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task WriteValueWithoutResponseAsync(byte[] value)
        {
            ThrowOnInvalidValue(value);
            return PlatformWriteValue(value, false);
        }

        private void ThrowOnInvalidValue(byte[] value)
        {
            if (value is null)
                throw new ArgumentNullException("value");

            if (value.Length > 512)
                throw new ArgumentOutOfRangeException("value", "Attribute value cannot be longer than 512 bytes");
        }

        public Task<GattDescriptor> GetDescriptorAsync(BluetoothUuid descriptor)
        {
            return PlatformGetDescriptor(descriptor);
        }

        public Task<IReadOnlyList<GattDescriptor>> GetDescriptorsAsync()
        {
            return PlatformGetDescriptors();
        }

        private event EventHandler<GattCharacteristicValueChangedEventArgs> characteristicValueChanged;

        void OnCharacteristicValueChanged(GattCharacteristicValueChangedEventArgs args)
        {
            characteristicValueChanged?.Invoke(this, args);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<GattCharacteristicValueChangedEventArgs> CharacteristicValueChanged
        {
            add
            {
                characteristicValueChanged += value;
                AddCharacteristicValueChanged();

            }
            remove
            {
                characteristicValueChanged -= value;
                RemoveCharacteristicValueChanged();
            }
        }

        public Task StartNotificationsAsync()
        {
            return PlatformStartNotifications();
        }

        public Task StopNotificationsAsync()
        {
            return PlatformStopNotifications();
        }
    }

    public sealed class GattCharacteristicValueChangedEventArgs : EventArgs
    {
        internal GattCharacteristicValueChangedEventArgs(byte[] newValue)
        {
            Value = newValue;
        }

        public byte[] Value { get; private set; }
    }
}
