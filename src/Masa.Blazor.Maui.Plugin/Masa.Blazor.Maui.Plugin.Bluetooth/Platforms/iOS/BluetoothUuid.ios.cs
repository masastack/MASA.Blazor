using CoreBluetooth;
using System.Runtime.InteropServices;

namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    partial struct BluetoothUuid
    {
        public static implicit operator CBUUID(BluetoothUuid uuid)
        {
            return CBUUID.FromString(uuid.Value.ToString());
        }

        public static implicit operator BluetoothUuid(CBUUID uuid)
        {
            byte[] b = new byte[16];
            System.Diagnostics.Debug.WriteLine(uuid.ToString());

            switch (uuid.Data.Length)
            {
                case 2:
                    b = BluetoothUuid.BluetoothBase.ToByteArray();
                    b[0] = uuid.Data[1];
                    b[1] = uuid.Data[0];
                    break;

                default:
                    b[0] = uuid.Data[3];
                    b[1] = uuid.Data[2];
                    b[2] = uuid.Data[1];
                    b[3] = uuid.Data[0];
                    b[4] = uuid.Data[5];
                    b[5] = uuid.Data[4];
                    b[6] = uuid.Data[7];
                    b[7] = uuid.Data[6];
                    Marshal.Copy(uuid.Data.Bytes, b, 8, 8);

                    break;
            }

            return new Guid(b);
        }
    }
}
