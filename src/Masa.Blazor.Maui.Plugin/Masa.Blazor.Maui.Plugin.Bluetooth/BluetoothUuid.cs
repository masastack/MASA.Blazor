using System.Diagnostics;

namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    /// <summary>
    /// Represents a Bluetooth UUID. Can be expressed as a Guid or a short ID for documented Bluetooth SIG definitions.
    /// </summary>
    public partial struct BluetoothUuid
    {
        internal static readonly Guid BluetoothBase = new Guid(0x00000000, 0x0000, 0x1000, 0x80, 0x00, 0x00, 0x80, 0x5F, 0x9B, 0x34, 0xFB);

        private Guid _uuid;

        private BluetoothUuid(Guid uuid)
        {
            _uuid = uuid;
        }

        public static implicit operator Guid(BluetoothUuid uuid)
        {
            return uuid._uuid;
        }

        public static implicit operator BluetoothUuid(Guid uuid)
        {
            return new BluetoothUuid(uuid);
        }

        public static implicit operator BluetoothUuid(ushort uuid)
        {
            return FromShortId(uuid);
        }

        public static explicit operator BluetoothUuid(string uuid)
        {
            return FromId(uuid);
        }

        public static explicit operator ushort(BluetoothUuid uuid)
        {
            ushort? val = TryGetShortId(uuid);
            if (val.HasValue)
                return val.Value;

            return default;
        }

        /// <summary>
        /// Returns true if both Uuids are equal.
        /// </summary>
        /// <param name="uuid1"></param>
        /// <param name="uuid2"></param>
        /// <returns></returns>
        public static bool operator ==(BluetoothUuid uuid1, BluetoothUuid uuid2)
        {
            return uuid1.Value == uuid2.Value;
        }

        /// <summary>
        /// Returns true if both Uuids are equal.
        /// </summary>
        /// <param name="uuid1"></param>
        /// <param name="uuid2"></param>
        /// <returns></returns>
        public static bool operator ==(BluetoothUuid uuid1, Guid uuid2)
        {
            return uuid1.Value == uuid2;
        }

        /// <summary>
        /// Returns true if both Uuids are unequal.
        /// </summary>
        /// <param name="uuid1"></param>
        /// <param name="uuid2"></param>
        /// <returns></returns>
        public static bool operator !=(BluetoothUuid uuid1, BluetoothUuid uuid2)
        {
            return uuid1.Value != uuid2.Value;
        }

        /// <summary>
        /// Returns true if both Uuids are unequal.
        /// </summary>
        /// <param name="uuid1"></param>
        /// <param name="uuid2"></param>
        /// <returns></returns>
        public static bool operator !=(BluetoothUuid uuid1, Guid uuid2)
        {
            return uuid1.Value != uuid2;
        }

        /// <summary>
        /// The full underlying value as a <see cref="Guid"/>.
        /// </summary>
        [DebuggerDisplay("{0}")]
        public Guid Value
        {
            get
            {
                return _uuid;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is BluetoothUuid uuid)
            {
                return Value == uuid.Value;
            }
            else if (obj is ushort shortId)
            {
                ushort? shortVal = TryGetShortId(Value);

                if (shortVal.HasValue && shortVal.Value == shortId)
                {
                    return true;
                }
            }
            else if (obj is Guid guid)
            {
                return Value == guid;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            var shortId = TryGetShortId(Value);
            if (shortId.HasValue)
                return shortId.Value.ToString("X4");

            return Value.ToString();
        }

        /// <summary>
        /// A default Uuid containing a zero Guid.
        /// </summary>
        /// <value>This is not a valid value so be careful passing this to any native function.</value>
        public static BluetoothUuid Empty
        {
            get
            {
                return default;
            }
        }

        public static BluetoothUuid FromGuid(Guid uuid)
        {
            return new BluetoothUuid(uuid);
        }

        /// <summary>
        /// Returns the bluetooth device UUID from a short ID.
        /// </summary>
        /// <param name="shortId">The short ID.</param>
        /// <returns>Returns the UUID.</returns>
        public static BluetoothUuid FromShortId(ushort shortId)
        {
            byte[] guidBytes = BluetoothBase.ToByteArray();
            BitConverter.GetBytes(shortId).CopyTo(guidBytes, 0);
            return new Guid(guidBytes);
        }

        /// <summary>
        /// Attempts to get the short Bluetooth ID from a UUID.
        /// </summary>
        /// <param name="uuid">The UUID.</param>
        /// <returns>Returns the short ID.</returns>
        public static ushort? TryGetShortId(Guid uuid)
        {
            var bytes = uuid.ToByteArray();
            var baseBytes = BluetoothBase.ToByteArray();
            bool match = true;
            for (int i = 4; i < 16; i++)
            {
                if (bytes[i] != baseBytes[i])
                {
                    match = false;
                    break;
                }
            }

            return match ? BitConverter.ToUInt16(bytes, 0) : (ushort?)null;
        }

        /// <summary>
        /// Returns the Uuid for a given Bluetooth SIG name.
        /// Names must beging with the "org.bluetooth" prefix.
        /// </summary>
        /// <param name="uuidName"></param>
        /// <returns></returns>
        public static BluetoothUuid FromId(string uuidName)
        {
            if (uuidName.StartsWith(GattServiceUuids.Namespace))
            {
                return GattServiceUuids.FromBluetoothUti(uuidName);
            }
            else if (uuidName.StartsWith(GattCharacteristicUuids.Namespace))
            {
                return GattCharacteristicUuids.FromBluetoothUti(uuidName);
            }
            else if (uuidName.StartsWith(GattDescriptorUuids.Namespace))
            {
                return GattDescriptorUuids.FromBluetoothUti(uuidName);
            }

            return default;
        }

        public static BluetoothUuid GetService(string name)
        {
            if (Guid.TryParse(name, out Guid uuid))
            {
                return uuid;
            }
            else if (ushort.TryParse(name, out ushort alias))
            {
                return FromShortId(alias);
            }
            else
            {
                return GattServiceUuids.FromBluetoothUti(name);
            }
        }

        public static BluetoothUuid GetCharacteristic(string name)
        {
            if (Guid.TryParse(name, out Guid uuid))
            {
                return uuid;
            }
            else if (ushort.TryParse(name, out ushort alias))
            {
                return FromShortId(alias);
            }
            else
            {
                return GattCharacteristicUuids.FromBluetoothUti(name);
            }
        }

        public static BluetoothUuid GetDescriptor(string name)
        {
            if (Guid.TryParse(name, out Guid uuid))
            {
                return uuid;
            }
            else if (ushort.TryParse(name, out ushort alias))
            {
                return FromShortId(alias);
            }
            else
            {
                return GattDescriptorUuids.FromBluetoothUti(name);
            }
        }
    }
}
