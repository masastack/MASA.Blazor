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
        Task PlatformConnect()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            void connectedHandler(object sender, CBPeripheralEventArgs e)
            {
                if (e.Peripheral.Identifier.IsEqual(((CBPeripheral)Device).Identifier))
                {
                    MasaMauiBluetoothService.ConnectedPeripheral -= connectedHandler;
                    MasaMauiBluetoothService.FailedToConnectPeripheral -= failedConnectHandler;
                    tcs.SetResult(true);
                }
            };

            void failedConnectHandler(object sender, CBPeripheralErrorEventArgs e)
            {
                if (e.Peripheral.Identifier.IsEqual(((CBPeripheral)Device).Identifier))
                {
                    MasaMauiBluetoothService.ConnectedPeripheral -= connectedHandler;
                    MasaMauiBluetoothService.FailedToConnectPeripheral -= failedConnectHandler;
                    tcs.SetResult(false);
                }
            };

            MasaMauiBluetoothService.ConnectedPeripheral += connectedHandler;
            MasaMauiBluetoothService.FailedToConnectPeripheral += failedConnectHandler;
#if __IOS__
            if (Device.RequiresAncs)
            {
                MasaMauiBluetoothService._manager.ConnectPeripheral(Device, new CBConnectPeripheralOptions { RequiresAncs = Device.RequiresAncs });
            }
            else
            {
#endif
                MasaMauiBluetoothService._manager.ConnectPeripheral(Device);
#if __IOS__
            }
#endif

            switch (((CBPeripheral)Device).State)
            {
                case CBPeripheralState.Connected:
                    MasaMauiBluetoothService.ConnectedPeripheral -= connectedHandler;
                    MasaMauiBluetoothService.FailedToConnectPeripheral -= failedConnectHandler;
                    return Task.CompletedTask;

                case CBPeripheralState.Connecting:
                    Task.Run(async () =>
                    {
                        await Task.Delay(5000);
                        if (!tcs.Task.IsCompletedSuccessfully)
                        {
                            tcs.SetResult(false);
                        }
                    });
                    return tcs.Task;

                default:
                    MasaMauiBluetoothService._manager.CancelPeripheralConnection(Device);
                    return Task.CompletedTask;
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

        bool PlatformRequestMtu(int mtu)
        {
            return false;
        }
    }
}
