using CoreBluetooth;
using Foundation;

namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    partial class GattCharacteristic
    {
        private CBCharacteristic _characteristic;

        internal GattCharacteristic(GattService service, CBCharacteristic characteristic) : this(service)
        {
            _characteristic = characteristic;
        }

        public static implicit operator CBCharacteristic(GattCharacteristic characteristic)
        {
            return characteristic._characteristic;
        }

        BluetoothUuid GetUuid()
        {
            return _characteristic.UUID;
        }

        GattCharacteristicProperties GetProperties()
        {
            return (GattCharacteristicProperties)((int)_characteristic.Properties & 0xff);
        }


        Task<GattDescriptor> PlatformGetDescriptor(BluetoothUuid descriptor)
        {
            TaskCompletionSource<GattDescriptor> tcs = new TaskCompletionSource<GattDescriptor>();
            CBPeripheral peripheral = Service.Device;

            void handler(object sender, CBCharacteristicEventArgs args)
            {
                peripheral.DiscoveredDescriptor -= handler;

                if (args.Error != null)
                {
                    tcs.SetException(new Exception(args.Error.ToString()));
                    return;
                }

                foreach (CBDescriptor cbdescriptor in _characteristic.Descriptors)
                {
                    if ((BluetoothUuid)cbdescriptor.UUID == descriptor)
                    {
                        tcs.SetResult(new GattDescriptor(this, cbdescriptor));
                        return;
                    }
                }

                tcs.SetResult(null);
            }

            peripheral.DiscoveredDescriptor += handler;
            peripheral.DiscoverDescriptors(_characteristic);

            return tcs.Task;
        }

        Task<IReadOnlyList<GattDescriptor>> PlatformGetDescriptors()
        {
            TaskCompletionSource<IReadOnlyList<GattDescriptor>> tcs = new TaskCompletionSource<IReadOnlyList<GattDescriptor>>();
            CBPeripheral peripheral = Service.Device;

            void handler(object sender, CBCharacteristicEventArgs args)
            {
                peripheral.DiscoveredDescriptor -= handler;

                if (args.Error != null)
                {
                    tcs.SetException(new Exception(args.Error.ToString()));
                    return;
                }

                List<GattDescriptor> descriptors = new List<GattDescriptor>();

                foreach (CBDescriptor cbdescriptor in _characteristic.Descriptors)
                {
                    descriptors.Add(new GattDescriptor(this, cbdescriptor));
                }

                tcs.SetResult(descriptors.AsReadOnly());
            }

            peripheral.DiscoveredDescriptor += handler;
            peripheral.DiscoverDescriptors(_characteristic);

            return tcs.Task;
        }

        byte[] PlatformGetValue()
        {
            return _characteristic.Value.ToArray();
        }

        Task<byte[]> PlatformReadValue()
        {
            TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>();
            CBPeripheral peripheral = Service.Device;

            void handler(object s, CBCharacteristicEventArgs e)
            {
                if (e.Characteristic == _characteristic)
                {
                    peripheral.UpdatedCharacterteristicValue += handler;

                    if (e.Error != null)
                    {
                        tcs.SetException(new Exception(e.Error.ToString()));
                    }
                    else
                    {
                        if (!tcs.Task.IsCompleted)
                        {
                            tcs.SetResult(e.Characteristic.Value.ToArray());
                        }
                    }
                }
            };

            peripheral.UpdatedCharacterteristicValue += handler;
            peripheral.ReadValue(_characteristic);
            return tcs.Task;
        }

        Task PlatformWriteValue(byte[] value, bool requireResponse)
        {
            TaskCompletionSource<bool> tcs = null;
            CBPeripheral peripheral = Service.Device;

            if (requireResponse)
            {
                tcs = new TaskCompletionSource<bool>();

                void handler(object s, CBCharacteristicEventArgs e)
                {
                    if (e.Characteristic == _characteristic)
                    {
                        peripheral.WroteCharacteristicValue -= handler;

                        if (!tcs.Task.IsCompleted)
                        {
                            tcs.SetResult(e.Error == null);
                        }
                    }
                };

                peripheral.WroteCharacteristicValue += handler;
            }

            CBCharacteristicWriteType writeType = requireResponse ? CBCharacteristicWriteType.WithResponse : CBCharacteristicWriteType.WithoutResponse;

            if (!requireResponse && !peripheral.CanSendWriteWithoutResponse)
                writeType = CBCharacteristicWriteType.WithResponse;

            ((CBPeripheral)Service.Device).WriteValue(NSData.FromArray(value), _characteristic, writeType);

            if (requireResponse)
                return tcs.Task;

            return Task.CompletedTask;
        }

        void AddCharacteristicValueChanged()
        {
            CBPeripheral peripheral = Service.Device;
            peripheral.UpdatedCharacterteristicValue += Peripheral_UpdatedCharacterteristicValue;
        }

        void Peripheral_UpdatedCharacterteristicValue(object sender, CBCharacteristicEventArgs e)
        {
            if (e.Characteristic == _characteristic)
                OnCharacteristicValueChanged(new GattCharacteristicValueChangedEventArgs(e.Characteristic.Value.ToArray()));
        }

        void RemoveCharacteristicValueChanged()
        {
            CBPeripheral peripheral = Service.Device;
            peripheral.UpdatedCharacterteristicValue -= Peripheral_UpdatedCharacterteristicValue;

        }

        private Task PlatformStartNotifications()
        {
            CBPeripheral peripheral = Service.Device;
            peripheral.SetNotifyValue(true, _characteristic);
            return Task.CompletedTask;
        }

        private Task PlatformStopNotifications()
        {
            CBPeripheral peripheral = Service.Device;
            peripheral.SetNotifyValue(false, _characteristic);
            return Task.CompletedTask;
        }
    }
}
