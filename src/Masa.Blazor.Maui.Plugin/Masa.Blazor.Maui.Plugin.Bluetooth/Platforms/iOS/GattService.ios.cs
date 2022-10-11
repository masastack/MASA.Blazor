using CoreBluetooth;

namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    partial class GattService
    {
        private CBService _service;

        internal GattService(BluetoothDevice device, CBService service) : this(device)
        {
            _service = service;
        }

        public static implicit operator CBService(GattService service)
        {
            return service._service;
        }

        BluetoothUuid GetUuid()
        {
            return _service.UUID;
        }

        bool GetIsPrimary()
        {
            return true;
        }

        Task<GattCharacteristic> PlatformGetCharacteristic(BluetoothUuid characteristic)
        {
            ((CBPeripheral)Device).DiscoverCharacteristics(new CBUUID[] { characteristic }, _service);
            GattCharacteristic matchingCharacteristic = null;

            foreach (CBCharacteristic cbcharacteristic in _service.Characteristics)
            {
                if ((BluetoothUuid)cbcharacteristic.UUID == characteristic)
                {
                    matchingCharacteristic = new GattCharacteristic(this, cbcharacteristic);
                    break;
                }
            }

            return Task.FromResult(matchingCharacteristic);
        }

        Task<IReadOnlyList<GattCharacteristic>> PlatformGetCharacteristics()
        {
            TaskCompletionSource<IReadOnlyList<GattCharacteristic>> tcs = new TaskCompletionSource<IReadOnlyList<GattCharacteristic>>();
            CBPeripheral peripheral = Device;

            void handler(object sender, CBServiceEventArgs args)
            {
#if NET6_0_OR_GREATER
                peripheral.DiscoveredCharacteristics -= handler;
#else
                peripheral.DiscoveredCharacteristic -= handler;
#endif
                if (args.Error != null)
                    tcs.SetException(new Exception(args.Error.ToString()));

                List<GattCharacteristic> characteristics = new List<GattCharacteristic>();

                foreach (CBCharacteristic cbcharacteristic in _service.Characteristics)
                {
                    characteristics.Add(new GattCharacteristic(this, cbcharacteristic));
                }

                tcs.SetResult(characteristics.AsReadOnly());
            }

#if NET6_0_OR_GREATER
            peripheral.DiscoveredCharacteristics += handler;
#else
            peripheral.DiscoveredCharacteristic += handler;
#endif
            peripheral.DiscoverCharacteristics(_service);

            return tcs.Task;
        }

        private Task<GattService> PlatformGetIncludedServiceAsync(BluetoothUuid service)
        {
            TaskCompletionSource<GattService> tcs = new TaskCompletionSource<GattService>();
            CBPeripheral peripheral = Device;

            void handler(object sender, CBServiceEventArgs args)
            {
                peripheral.DiscoveredIncludedService -= handler;

                if (args.Error != null)
                {
                    tcs.SetException(new Exception(args.Error.ToString()));
                }
                else
                {
                    tcs.SetResult(new GattService(Device, args.Service));
                }
            }

            peripheral.DiscoveredIncludedService += handler;
            peripheral.DiscoverIncludedServices(new CBUUID[] { service }, _service);

            return tcs.Task;
        }

        private Task<IReadOnlyList<GattService>> PlatformGetIncludedServicesAsync()
        {
            TaskCompletionSource<IReadOnlyList<GattService>> tcs = new TaskCompletionSource<IReadOnlyList<GattService>>();
            CBPeripheral peripheral = Device;

            void handler(object sender, CBServiceEventArgs args)
            {
                peripheral.DiscoveredIncludedService -= handler;

                if (args.Error != null)
                {
                    tcs.SetException(new Exception(args.Error.ToString()));
                }
                else
                {
                    List<GattService> services = new List<GattService>();

                    foreach (var includedService in _service.IncludedServices)
                    {
                        services.Add(new GattService(Device, includedService));
                    }

                    tcs.SetResult(services.AsReadOnly());
                }
            }

            peripheral.DiscoveredIncludedService += handler;
            peripheral.DiscoverIncludedServices(new CBUUID[] { }, _service);

            return tcs.Task;
        }
    }
}
