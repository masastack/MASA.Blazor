using System.Diagnostics;
using CoreBluetooth;
namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    partial class RemoteGattServer
    {
        private void PlatformInit()
        {
            if (Device != null)
            { MasaMauiBluetoothService._manager.ConnectPeripheral(Device); }
        }

        bool GetConnected()
        {
            switch (((CBPeripheral)Device).State)
            {
                case CBPeripheralState.Connected:
                    return true;

                default:
                    return false;
            }
        }


        void PlatformDisconnect()
        {
            if (Device != null)
            { MasaMauiBluetoothService._manager.CancelPeripheralConnection(Device); }
        }

        void PlatformCleanup()
        {
        }

        Task<GattService> PlatformGetPrimaryService(BluetoothUuid service)
        {
            return Task.Run(() =>
            {
                EventWaitHandle handle = new EventWaitHandle(false, EventResetMode.AutoReset);

                GattService matchingService = null;

                ((CBPeripheral)Device).DiscoveredService += (sender, args) =>
                {
                    handle.Set();
                };

                ((CBPeripheral)Device).DiscoverServices(new CBUUID[] { service });

                handle.WaitOne();

                foreach (CBService cbservice in ((CBPeripheral)Device).Services)
                {
                    if ((BluetoothUuid)cbservice.UUID == service)
                    {
                        matchingService = new GattService(Device, cbservice);
                    }
                }

                return matchingService;
            });
        }

        Task<List<GattService>> PlatformGetPrimaryServices(BluetoothUuid? service)
        {
            return Task.Run(() =>
            {
                var services = new List<GattService>();

                EventWaitHandle handle = new EventWaitHandle(false, EventResetMode.AutoReset);

                ((CBPeripheral)Device).DiscoveredService += (sender, args) =>
                {
                    handle.Set();
                };

                if (service.HasValue)
                {
                    ((CBPeripheral)Device).DiscoverServices(new CBUUID[] { service });
                }
                else
                {
                    ((CBPeripheral)Device).DiscoverServices();
                }

                handle.WaitOne();

                foreach (CBService cbservice in ((CBPeripheral)Device).Services)
                {
                    services.Add(new GattService(Device, cbservice));
                }

                return services;
            });
        }

        Task<short> PlatformReadRssi()
        {
            TaskCompletionSource<short> tcs = new TaskCompletionSource<short>();
            var peripheral = (CBPeripheral)Device;

            void handler(object s, CBRssiEventArgs e)
            {
                peripheral.RssiRead -= handler;

                if (!tcs.Task.IsCompleted)
                {
                    tcs.SetResult(e.Rssi.Int16Value);
                }
            };

            peripheral.RssiRead += handler;
            peripheral.ReadRSSI();

            return tcs.Task;
        }
    }
}
