namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    public sealed partial class GattService
    {
        internal GattService(BluetoothDevice device)
        {
            Device = device;
        }

        public BluetoothDevice Device { get; private set; }

        public BluetoothUuid Uuid { get { return GetUuid(); } }

        public bool IsPrimary { get { return GetIsPrimary(); } }

        public Task<GattCharacteristic> GetCharacteristicAsync(BluetoothUuid characteristic)
        {
            return PlatformGetCharacteristic(characteristic);
        }

        public Task<IReadOnlyList<GattCharacteristic>> GetCharacteristicsAsync()
        {
            return PlatformGetCharacteristics();
        }

        public Task<GattService> GetIncludedServiceAsync(BluetoothUuid service)
        {
            return PlatformGetIncludedServiceAsync(service);
        }

        public Task<IReadOnlyList<GattService>> GetIncludedServicesAsync()
        {
            return PlatformGetIncludedServicesAsync();
        }

#if DEBUG
        public event EventHandler ServiceAdded;
        public event EventHandler ServiceChanged;
        public event EventHandler ServiceRemoved;
#endif
    }
}
