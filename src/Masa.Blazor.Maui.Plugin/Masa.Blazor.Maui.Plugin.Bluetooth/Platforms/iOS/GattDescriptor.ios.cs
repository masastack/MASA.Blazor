using CoreBluetooth;
using Foundation;

namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    partial class GattDescriptor
    {
        private CBDescriptor _descriptor;

        internal GattDescriptor(GattCharacteristic characteristic, CBDescriptor descriptor) : this(characteristic)
        {
            _descriptor = descriptor;
        }

        public static implicit operator CBDescriptor(GattDescriptor descriptor)
        {
            return descriptor._descriptor;
        }

        BluetoothUuid GetUuid()
        {
            return _descriptor.UUID;
        }

        byte[] PlatformGetValue()
        {
            return ((NSData)_descriptor.Value).ToArray();
        }

        Task<byte[]> PlatformReadValue()
        {
            TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>();
            CBPeripheral peripheral = Characteristic.Service.Device;

            void handler(object s, CBDescriptorEventArgs e)
            {
                if (e.Descriptor == _descriptor)
                {
                    peripheral.UpdatedValue -= handler;

                    if (!tcs.Task.IsCompleted)
                    {
                        tcs.SetResult(NSObjectToBytes(e.Descriptor.Value));
                    }
                }
            };

            peripheral.UpdatedValue += handler;

            ((CBPeripheral)Characteristic.Service.Device).ReadValue(_descriptor);
            return tcs.Task;
        }

        private static byte[] NSObjectToBytes(NSObject obj)
        {
            if (obj is NSString str)
            {
                return System.Text.Encoding.UTF8.GetBytes(str);
            }
            else if (obj is NSNumber num)
            {
                System.Diagnostics.Debug.WriteLine(num.ObjCType);
                switch (num.ObjCType)
                {
                    case "c":
                        return new byte[] { num.ByteValue };
                    case "s":
                        return BitConverter.GetBytes(num.Int16Value);
                    case "i":
                        return BitConverter.GetBytes(num.Int32Value);
                    case "l":
                        return BitConverter.GetBytes(num.Int32Value);
                    case "q":
                        return BitConverter.GetBytes(num.Int64Value);
                    case "C":
                        return new byte[] { num.ByteValue };
                    case "S":
                        return BitConverter.GetBytes(num.UInt16Value);
                    case "I":
                        return BitConverter.GetBytes(num.UInt32Value);
                    case "L":
                        return BitConverter.GetBytes(num.UInt32Value);
                    case "Q":
                        return BitConverter.GetBytes(num.UInt64Value);
                    case "f":
                        return BitConverter.GetBytes(num.FloatValue);
                    case "d":
                        return BitConverter.GetBytes(num.DoubleValue);

                    default:
                        return new byte[] { num.ByteValue };
                }
            }
            else if (obj is NSData data)
            {
                return data.ToArray();
            }

            return null;
        }

        Task PlatformWriteValue(byte[] value)
        {
            TaskCompletionSource<bool> tcs = null;
            CBPeripheral peripheral = Characteristic.Service.Device;

            tcs = new TaskCompletionSource<bool>();

            void handler(object s, CBDescriptorEventArgs e)
            {
                if (e.Descriptor == _descriptor)
                {
                    peripheral.WroteDescriptorValue -= handler;

                    if (!tcs.Task.IsCompleted)
                    {
                        tcs.SetResult(e.Error == null);
                    }
                }
            };

            peripheral.WroteDescriptorValue += handler;

            peripheral.WriteValue(NSData.FromArray(value), _descriptor);
            return tcs.Task;
        }
    }
}
