namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    partial class GattCharacteristic
    {
        private readonly Android.Bluetooth.BluetoothGattCharacteristic _characteristic;

        internal GattCharacteristic(GattService service, Android.Bluetooth.BluetoothGattCharacteristic characteristic) : this(service)
        {
            _characteristic = characteristic;
        }

        public static implicit operator Android.Bluetooth.BluetoothGattCharacteristic(GattCharacteristic characteristic)
        {
            return characteristic._characteristic;
        }

        BluetoothUuid GetUuid()
        {
            return _characteristic.Uuid;
        }

        GattCharacteristicProperties GetProperties()
        {
            return (GattCharacteristicProperties)(int)_characteristic.Properties;
        }

        //string GetUserDescription()
        //{
        //    return GetManualUserDescription();
        //}

        Task<GattDescriptor> PlatformGetDescriptor(BluetoothUuid descriptor)
        {
            var gattDescriptor = _characteristic.GetDescriptor(descriptor);
            if (gattDescriptor is null)
                return Task.FromResult<GattDescriptor>(null);

            return Task.FromResult(new GattDescriptor(this, gattDescriptor));
        }

        async Task<IReadOnlyList<GattDescriptor>> PlatformGetDescriptors()
        {
            List<GattDescriptor> descriptors = new List<GattDescriptor>();

            foreach (var descriptor in _characteristic.Descriptors)
            {
                descriptors.Add(new GattDescriptor(this, descriptor));
            }

            return descriptors;
        }

        byte[] PlatformGetValue()
        {
            return _characteristic.GetValue();
        }

        Task<byte[]> PlatformReadValue()
        {
            TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>();

            void handler(object s, CharacteristicEventArgs e)
            {
                if (e.Characteristic == _characteristic)
                {
                    Service.Device.Gatt.CharacteristicRead -= handler;

                    if (!tcs.Task.IsCompleted)
                    {
                        tcs.SetResult(_characteristic.GetValue());
                    }
                }
            };

            Service.Device.Gatt.CharacteristicRead += handler;
            bool read = ((Android.Bluetooth.BluetoothGatt)Service.Device.Gatt).ReadCharacteristic(_characteristic);
            return tcs.Task;
        }

        Task PlatformWriteValue(byte[] value, bool requireResponse)
        {
            TaskCompletionSource<bool> tcs = null;

            if (requireResponse)
            {
                tcs = new TaskCompletionSource<bool>();

                void handler(object s, CharacteristicEventArgs e)
                {
                    if (e.Characteristic == _characteristic)
                    {
                        Service.Device.Gatt.CharacteristicWrite -= handler;

                        if (!tcs.Task.IsCompleted)
                        {
                            tcs.SetResult(e.Status == Android.Bluetooth.GattStatus.Success);
                        }
                    }
                };

                Service.Device.Gatt.CharacteristicWrite += handler;
            }

            bool written = _characteristic.SetValue(value);
            _characteristic.WriteType = requireResponse ? Android.Bluetooth.GattWriteType.Default : Android.Bluetooth.GattWriteType.NoResponse;
            written = ((Android.Bluetooth.BluetoothGatt)Service.Device.Gatt).WriteCharacteristic(_characteristic);

            if (written && requireResponse)
                return tcs.Task;

            return Task.CompletedTask;
        }

        void AddCharacteristicValueChanged()
        {
            Service.Device.Gatt.CharacteristicChanged += Gatt_CharacteristicChanged;
        }

        private void Gatt_CharacteristicChanged(object sender, CharacteristicEventArgs e)
        {
            if (e.Characteristic == _characteristic)
                OnCharacteristicValueChanged(new GattCharacteristicValueChangedEventArgs(e.Characteristic.GetValue()));
        }

        void RemoveCharacteristicValueChanged()
        {
            Service.Device.Gatt.CharacteristicChanged -= Gatt_CharacteristicChanged;
        }

        private async Task PlatformStartNotifications()
        {
            byte[] data;

            if (_characteristic.Properties.HasFlag(Android.Bluetooth.GattProperty.Notify))
                data = Android.Bluetooth.BluetoothGattDescriptor.EnableNotificationValue.ToArray();
            else if (_characteristic.Properties.HasFlag(Android.Bluetooth.GattProperty.Indicate))
                data = Android.Bluetooth.BluetoothGattDescriptor.EnableIndicationValue.ToArray();
            else
                return;

            ((Android.Bluetooth.BluetoothGatt)Service.Device.Gatt).SetCharacteristicNotification(_characteristic, true);
            var descriptor = await GetDescriptorAsync(GattDescriptorUuids.ClientCharacteristicConfiguration);
            await descriptor.WriteValueAsync(data);
        }

        private async Task PlatformStopNotifications()
        {
            ((Android.Bluetooth.BluetoothGatt)Service.Device.Gatt).SetCharacteristicNotification(_characteristic, false);
            if (Service.Device.Gatt.IsConnected)
            {
                var descriptor = await GetDescriptorAsync(GattDescriptorUuids.ClientCharacteristicConfiguration);
                await descriptor.WriteValueAsync(new byte[] { 0, 0 });
            }
        }
    }
}
