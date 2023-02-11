namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    partial class RemoteGattServer
    {
        private Android.Bluetooth.BluetoothGatt _gatt;
        private Android.Bluetooth.BluetoothGattCallback _gattCallback;
        private bool _servicesDiscovered = false;
        private int requestedMtu;
        private void PlatformInit()
        {
            // android default - replaced by callback after request or change
            Mtu = 20;
            _gattCallback = new GattCallback(this);
            _gatt = ((Android.Bluetooth.BluetoothDevice)Device).ConnectGatt(Android.App.Application.Context, AutoConnect, _gattCallback, Android.Bluetooth.BluetoothTransports.Le);
            _gatt.RequestMtu(512);
        }

        public static implicit operator Android.Bluetooth.BluetoothGatt(RemoteGattServer gatt)
        {
            return gatt._gatt;
        }

        internal event EventHandler<ConnectionStateEventArgs> ConnectionStateChanged;
        internal event EventHandler<CharacteristicEventArgs> CharacteristicChanged;
        internal event EventHandler<CharacteristicEventArgs> CharacteristicRead;
        internal event EventHandler<CharacteristicEventArgs> CharacteristicWrite;
        internal event EventHandler<DescriptorEventArgs> DescriptorRead;
        internal event EventHandler<DescriptorEventArgs> DescriptorWrite;
        internal event EventHandler<GattEventArgs> ServicesDiscovered;
        internal event EventHandler<RssiEventArgs> ReadRemoteRssi;

        

        internal class GattCallback : Android.Bluetooth.BluetoothGattCallback
        {
            private readonly RemoteGattServer _owner;
            
            internal GattCallback(RemoteGattServer owner)
            {
                _owner = owner;
            }

            

            public override void OnMtuChanged(Android.Bluetooth.BluetoothGatt gatt, int mtu, Android.Bluetooth.GattStatus status)
            {
                System.Diagnostics.Debug.WriteLine($"OnMtuChanged Status:{status} Size:{mtu}");
                _owner.Mtu = mtu;
                base.OnMtuChanged(gatt, mtu, status);
            }

            public override void OnConnectionStateChange(Android.Bluetooth.BluetoothGatt gatt, Android.Bluetooth.GattStatus status, Android.Bluetooth.ProfileState newState)
            {
                System.Diagnostics.Debug.WriteLine($"ConnectionStateChanged Status:{status} NewState:{newState}");
                _owner.ConnectionStateChanged?.Invoke(_owner, new ConnectionStateEventArgs { Status = status, State = newState });
                if (newState == Android.Bluetooth.ProfileState.Connected)
                {
                    if (!_owner._servicesDiscovered)
                        gatt.DiscoverServices();
                }
                else
                {
                    _owner.Device.OnGattServerDisconnected();
                    _owner.Device.Disconnect();
                }
            }

            public override void OnCharacteristicRead(Android.Bluetooth.BluetoothGatt gatt, Android.Bluetooth.BluetoothGattCharacteristic characteristic, Android.Bluetooth.GattStatus status)
            {
                System.Diagnostics.Debug.WriteLine($"CharacteristicRead {characteristic.Uuid} Status:{status}");
                _owner.CharacteristicRead?.Invoke(_owner, new CharacteristicEventArgs { Characteristic = characteristic, Status = status });
            }

            public override void OnCharacteristicWrite(Android.Bluetooth.BluetoothGatt gatt, Android.Bluetooth.BluetoothGattCharacteristic characteristic, Android.Bluetooth.GattStatus status)
            {
                System.Diagnostics.Debug.WriteLine($"CharacteristicWrite {characteristic.Uuid} Status:{status}");
                _owner.CharacteristicWrite?.Invoke(_owner, new CharacteristicEventArgs { Characteristic = characteristic, Status = status });
            }

            public override void OnCharacteristicChanged(Android.Bluetooth.BluetoothGatt gatt, Android.Bluetooth.BluetoothGattCharacteristic characteristic)
            {
                System.Diagnostics.Debug.WriteLine($"CharacteristicChanged {characteristic.Uuid}");
                _owner.CharacteristicChanged?.Invoke(_owner, new CharacteristicEventArgs { Characteristic = characteristic });
            }

            public override void OnDescriptorRead(Android.Bluetooth.BluetoothGatt gatt, Android.Bluetooth.BluetoothGattDescriptor descriptor, Android.Bluetooth.GattStatus status)
            {
                System.Diagnostics.Debug.WriteLine($"DescriptorRead {descriptor.Uuid} Status:{status}");
                _owner.DescriptorRead?.Invoke(_owner, new DescriptorEventArgs { Descriptor = descriptor, Status = status });
            }

            public override void OnDescriptorWrite(Android.Bluetooth.BluetoothGatt gatt, Android.Bluetooth.BluetoothGattDescriptor descriptor, Android.Bluetooth.GattStatus status)
            {
                System.Diagnostics.Debug.WriteLine($"DescriptorWrite {descriptor.Uuid} Status:{status}");
                _owner.DescriptorWrite?.Invoke(_owner, new DescriptorEventArgs { Descriptor = descriptor, Status = status });
            }

            public override void OnServicesDiscovered(Android.Bluetooth.BluetoothGatt gatt, Android.Bluetooth.GattStatus status)
            {
                System.Diagnostics.Debug.WriteLine($"ServicesDiscovered Status:{status}");
                _owner._servicesDiscovered = true;
                _owner.ServicesDiscovered?.Invoke(_owner, new GattEventArgs { Status = status });
            }

            public override void OnReadRemoteRssi(Android.Bluetooth.BluetoothGatt gatt, int rssi, Android.Bluetooth.GattStatus status)
            {
                System.Diagnostics.Debug.WriteLine($"ReadRemoteRssi {rssi}");
                _owner.ReadRemoteRssi?.Invoke(_owner, new RssiEventArgs { Status = status, Rssi = (short)rssi });
            }

            public override void OnPhyUpdate(Android.Bluetooth.BluetoothGatt gatt, Android.Bluetooth.LE.ScanSettingsPhy txPhy, Android.Bluetooth.LE.ScanSettingsPhy rxPhy, Android.Bluetooth.GattStatus status)
            {
                System.Diagnostics.Debug.WriteLine($"PhyUpdate TX:{txPhy} RX:{rxPhy} Status:{status}");
            }
        }

        bool GetConnected()
        {
            return MasaMauiBluetoothService._manager.GetConnectionState(Device,Android.Bluetooth.ProfileType.Gatt) == Android.Bluetooth.ProfileState.Connected;
        }

        private async Task<bool> WaitForServiceDiscovery()
        {
            if (_servicesDiscovered)
                return true;

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            void handler(object s, GattEventArgs e)
            {
                ServicesDiscovered -= handler;

                if (!tcs.Task.IsCompleted)
                {
                    tcs.SetResult(true);
                }
            };

            ServicesDiscovered += handler;
            return await tcs.Task;
        }

        Task PlatformConnect()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            void handler(object s, ConnectionStateEventArgs e)
            {
                ConnectionStateChanged -= handler;

                switch (e.Status)
                {
                    case Android.Bluetooth.GattStatus.Success:
                        tcs.SetResult(e.State == Android.Bluetooth.ProfileState.Connected);
                        break;

                    default:
                        tcs.SetResult(false);
                        break;
                }
            }

            ConnectionStateChanged += handler;
            bool success = _gatt.Connect();
            if (success)
            {
                if (IsConnected)
                    return Task.FromResult(true);

                return tcs.Task;
            }
            else
            {
                ConnectionStateChanged -= handler;
                return Task.FromException(new OperationCanceledException());
            }
        }

        void PlatformDisconnect()
        {
            _gatt.Disconnect();
        }

        void PlatformCleanup()
        {
            // Android has no explicit cleanup 🤪
        }

        async Task<GattService> PlatformGetPrimaryService(BluetoothUuid service)
        {
            await WaitForServiceDiscovery();

            Android.Bluetooth.BluetoothGattService nativeService = _gatt.GetService(service);

            return nativeService is null ? null : new GattService(Device, nativeService);
        }

        async Task<List<GattService>> PlatformGetPrimaryServices(BluetoothUuid? service)
        {
            var services = new List<GattService>();

            await WaitForServiceDiscovery();

            foreach (var serv in _gatt.Services)
            {
                // if a service was specified only add if service uuid is a match
                if (serv.Type == Android.Bluetooth.GattServiceType.Primary && (!service.HasValue || service.Value == serv.Uuid))
                {
                    services.Add(new GattService(Device, serv));
                }
            }

            return services;
        }

        Task<short> PlatformReadRssi()
        {
            TaskCompletionSource<short> tcs = new TaskCompletionSource<short>();

            void handler(object s, RssiEventArgs e)
            {
                ReadRemoteRssi -= handler;

                switch (e.Status)
                {
                    case Android.Bluetooth.GattStatus.Success:
                        tcs.SetResult(e.Rssi);
                        break;

                    default:
                        tcs.SetResult(0);
                        break;
                }
            }

            ReadRemoteRssi += handler;
            bool success = _gatt.ReadRemoteRssi();
            if (success)
            {
                return tcs.Task;
            }
            else
            {
                return Task.FromResult((short)0);
            }
        }

        

        bool PlatformRequestMtu(int mtu)
        {
            requestedMtu = mtu;
            if (IsConnected)
            {
                return _gatt.RequestMtu(mtu);
            }

            return false;
        }
    }

    internal class GattEventArgs : EventArgs
    {
        public Android.Bluetooth.GattStatus Status
        {
            get; internal set;
        }
    }

    internal class ConnectionStateEventArgs : GattEventArgs
    {
        public Android.Bluetooth.ProfileState State
        {
            get; internal set;
        }
    }

    internal class CharacteristicEventArgs : GattEventArgs
    {
        public Android.Bluetooth.BluetoothGattCharacteristic Characteristic
        {
            get; internal set;
        }
    }

    internal class DescriptorEventArgs : GattEventArgs
    {
        public Android.Bluetooth.BluetoothGattDescriptor Descriptor
        {
            get; internal set;
        }
    }

    internal class RssiEventArgs : GattEventArgs
    {
        public short Rssi
        {
            get; internal set;
        }
    }
}
