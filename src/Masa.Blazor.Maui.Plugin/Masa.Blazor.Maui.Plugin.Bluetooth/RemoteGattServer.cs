namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    public sealed partial class RemoteGattServer
    {
        /// <summary>
        /// Permit to manage autoconnect in GattServer
        /// </summary>
        /// <remarks>available only on monoandroid</remarks>
        public bool AutoConnect { get; set; } = false;

        /// <summary>
        /// Gets the parent remote device.
        /// </summary>
        public BluetoothDevice Device { get; private set; }

        internal RemoteGattServer(BluetoothDevice device)
        {
            Device = device;
            PlatformInit();
        }

        private void Device_GattServerDisconnected(object sender, EventArgs e)
        {
            Device.OnGattServerDisconnected();
        }

        [Obsolete("Use IsConnected instead", true)]
        public bool Connected { get { return IsConnected; } }

        /// <summary>
        /// Gets a value indicating if there is an active connection with the remote GATT server.
        /// </summary>
        /// <value>True if connected, else False.</value>
        public bool IsConnected { get { return GetConnected(); } }

        /// <summary>
        /// Gets the maximum transmission unit (MTU) size.
        /// </summary>
        public int Mtu { get; internal set; }


        /// <summary>
        /// Open a connection to the remote GATT server.
        /// </summary>
        /// <returns></returns>
        public Task ConnectAsync()
        {
            return PlatformConnect();
        }

        /// <summary>
        /// Disconnects the remote GATT server.
        /// </summary>
        /// <remarks>Some platforms do not have an explicit Disconnect and will keep the connection open until there are no references to associated services and characteristics.</remarks>
        public void Disconnect()
        {
            PlatformDisconnect();
            Device.OnGattServerDisconnected();
            PlatformCleanup();
        }

        /// <summary>
        /// Returns the primary service matching the specified UUID.
        /// </summary>
        /// <param name="service">The requested service or null if not present.</param>
        /// <returns></returns>
        public Task<GattService> GetPrimaryServiceAsync(BluetoothUuid service)
        {
            return PlatformGetPrimaryService(service);
        }

        /// <summary>
        /// Returns primary services, optionally filtering to the supplied UUID.
        /// </summary>
        /// <param name="service">Optional service UUID.</param>
        /// <returns>A list of matching primary services.</returns>
        public Task<List<GattService>> GetPrimaryServicesAsync(BluetoothUuid? service = null)
        {
            return PlatformGetPrimaryServices(service);
        }

        /// <summary>
        /// Requests the Received Signal Strength Indication (RSSI) from the remote device.
        /// </summary>
        /// <returns>RSSI or Zero if unavailable.</returns>
        public Task<short> ReadRssi()
        {
            return PlatformReadRssi();
        }

        /// <summary>
        /// Request the maximum transmission unit (MTU) size.
        /// </summary>
        /// <param name="mtu">mtu size from 0 to 512</param>
        /// <returns>true if succesful.</returns>
        /// <remarks>Currently supported only on Android.</remarks>
        public bool RequestMtu(int mtu)
        {
            return PlatformRequestMtu(mtu);
        }
    }
}
