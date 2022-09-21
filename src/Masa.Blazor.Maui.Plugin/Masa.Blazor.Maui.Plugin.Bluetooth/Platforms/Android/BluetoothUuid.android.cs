using Android.OS;
using Java.Util;
namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    partial struct BluetoothUuid
    {
        public static implicit operator UUID(BluetoothUuid uuid)
        {
            return UUID.FromString(uuid.Value.ToString());
        }

        public static implicit operator ParcelUuid(BluetoothUuid uuid)
        {
            return ParcelUuid.FromString(uuid.Value.ToString());
        }

        public static implicit operator BluetoothUuid(UUID uuid)
        {
            return Guid.Parse(uuid.ToString());
        }

        public static implicit operator BluetoothUuid(ParcelUuid uuid)
        {
            return Guid.Parse(uuid.ToString());
        }
    }
}
