using System.Reflection;

namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    [BluetoothUti(Namespace)]
    public static class GattServiceUuids
    {
        internal const string Namespace = "org.bluetooth.service";

        /// <summary>
        /// Returns the Uuid for a service given the Uniform Type Identifier.
        /// </summary>
        /// <param name="bluetoothUti">Uniform Type Identifier of the service e.g. org.bluetooth.service.generic_access</param>
        /// <returns>The service Uuid on success else Guid.Empty.</returns>
        public static BluetoothUuid FromBluetoothUti(string bluetoothUti)
        {
            string requestedUti = bluetoothUti.ToLower();
            if (requestedUti.StartsWith(Namespace))
            {
                requestedUti = requestedUti.Substring(requestedUti.LastIndexOf("."));
            }

            var fields = typeof(GattServiceUuids).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var field in fields)
            {
                var attr = field.GetCustomAttribute(typeof(BluetoothUtiAttribute));
                if (attr != null && ((BluetoothUtiAttribute)attr).Uti == requestedUti)
                {
                    return (BluetoothUuid)field.GetValue(null);
                }
            }

            return default;
        }

        public static string GetServiceName(BluetoothUuid uuid, bool includeNamespace = false)
        {
            var shortid = BluetoothUuid.TryGetShortId(uuid);
            if (shortid.HasValue && (shortid.Value & 0xFF00) == 0x1800)
            {
                var fields = typeof(GattServiceUuids).GetFields(BindingFlags.Static | BindingFlags.Public);
                foreach (var field in fields)
                {
                    if ((BluetoothUuid)field.GetValue(null) == uuid)
                    {
                        var attr = field.GetCustomAttribute(typeof(BluetoothUtiAttribute));
                        if (attr != null)
                        {
                            return (includeNamespace ? Namespace + "." : string.Empty) + ((BluetoothUtiAttribute)attr).Uti;
                        }

                        return string.Empty;
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the Bluetooth SIG-defined UUID for the Generic Access Service.
        /// </summary>
        [BluetoothUti("generic_access")]
        public static readonly BluetoothUuid GenericAccess = 0x1800;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Alert Notification Service UUID.
        /// </summary>
        [BluetoothUti("alert_notification")]
        public static readonly BluetoothUuid AlertNotification = 0x1811;

        /// <summary>
        /// The Automation IO service is used to expose the analog inputs/outputs and digital input/outputs of a generic IO module (IOM).
        /// </summary>
        [BluetoothUti("automation_io")]
        public static readonly BluetoothUuid AutomationIO = 0x1815;

        /// <summary>
        /// The Battery Service exposes the state of a battery within a device. 
        /// </summary>
        [BluetoothUti("battery_service")]
        public static readonly BluetoothUuid Battery = 0x180F;

        /// <summary>
        /// 
        /// </summary>
        [BluetoothUti("binary_sensor")]
        public static readonly BluetoothUuid BinarySensor = 0x183B;

        /// <summary>
        /// The Blood Pressure Service exposes blood pressure and other data related to a blood pressure monitor.
        /// </summary>
        [BluetoothUti("blood_pressure")]
        public static readonly BluetoothUuid BloodPressure = 0x1810;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Body Composition service UUID.
        /// </summary>
        [BluetoothUti("body_composition")]
        public static readonly BluetoothUuid BodyComposition = 0x181B;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Bond Management service UUID.
        /// </summary>
        [BluetoothUti("bond_management")]
        public static readonly BluetoothUuid BondManagement = 0x181E;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Continuous Glucose Monitoring service UUID.
        /// </summary>
        [BluetoothUti("continuous_glucose_monitoring")]
        public static readonly BluetoothUuid ContinuousGlucoseMonitoring = 0x181F;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Current Time service UUID.
        /// </summary>
        [BluetoothUti("current_time")]
        public static readonly BluetoothUuid CurrentTime = 0x1805;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Cycling Power service UUID.
        /// </summary>
        [BluetoothUti("cycling_power")]
        public static readonly BluetoothUuid CyclingPower = 0x1818;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Cycling Speed and Cadence Service UUID.
        /// </summary>
        [BluetoothUti("cycling_speed_and_cadence")]
        public static readonly BluetoothUuid CyclingSpeedAndCadence = 0x1816;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Device Information service UUID.
        /// </summary>
        [BluetoothUti("device_information")]
        public static readonly BluetoothUuid DeviceInformation = 0x180A;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Environmental Sensing service UUID.
        /// </summary>
        [BluetoothUti("environmental_sensing")]
        public static readonly BluetoothUuid EnvironmentalSensing = 0x181A;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Fitness Machine service UUID.
        /// </summary>
        [BluetoothUti("fitness_machine")]
        public static readonly BluetoothUuid FitnessMachine = 0x1826;

        /// <summary>
        /// Gets the Bluetooth SIG-defined UUID for the Generic Attribute Service.
        /// </summary>
        [BluetoothUti("generic_attribute")]
        public static readonly BluetoothUuid GenericAttribute = 0x1801;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Glucose Service UUID.
        /// </summary>
        [BluetoothUti("glucose")]
        public static readonly BluetoothUuid Glucose = 0x1808;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Health Thermometer Service UUID.
        /// </summary>
        [BluetoothUti("health_thermometer")]
        public static readonly BluetoothUuid HealthThermometer = 0x1809;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Heart Rate Service UUID.
        /// </summary>
        [BluetoothUti("heart_rate")]
        public static readonly BluetoothUuid HeartRate = 0x180D;

        /// <summary>
        /// Gets the Bluetooth SIG-defined HTTP Proxy Service UUID.
        /// </summary>
        [BluetoothUti("http_proxy")]
        public static readonly BluetoothUuid HttpProxy = 0x1823;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Human Interface Device service UUID.
        /// </summary>
        [BluetoothUti("human_interface_device")]
        public static readonly BluetoothUuid HumanInterfaceDevice = 0x1812;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Immediate Alert service UUID.
        /// </summary>
        [BluetoothUti("immediate_alert")]
        public static readonly BluetoothUuid ImmediateAlert = 0x1802;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Indoor Positioning service UUID.
        /// </summary>
        [BluetoothUti("indoor_positioning")]
        public static readonly BluetoothUuid IndoorPositioning = 0x1821;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Insulin Delivery service UUID.
        /// </summary>
        [BluetoothUti("insulin_delivery")]
        public static readonly BluetoothUuid InsulinDelivery = 0x183A;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Internet Protocol Support service UUID.
        /// </summary>
        [BluetoothUti("internet_protocol_support")]
        public static readonly BluetoothUuid InternetProtocolSupport = 0x1820;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Link Loss service UUID.
        /// </summary>
        [BluetoothUti("link_loss")]
        public static readonly BluetoothUuid LinkLoss = 0x1803;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Location and Navigation service UUID.
        /// </summary>
        [BluetoothUti("location_and_navigation")]
        public static readonly BluetoothUuid LocationAndNavigation = 0x1819;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Mesh Provisioning service UUID.
        /// </summary>
        [BluetoothUti("mesh_provisioning")]
        public static readonly BluetoothUuid MeshProvisioning = 0x1827;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Mesh Proxy service UUID.
        /// </summary>
        [BluetoothUti("mesh_proxy")]
        public static readonly BluetoothUuid MeshProxy = 0x1828;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Next DST Change service UUID.
        /// </summary>
        [BluetoothUti("next_dst_change")]
        public static readonly BluetoothUuid NextDstChange = 0x1807;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Object Transfer service UUID.
        /// </summary>
        [BluetoothUti("object_transfer")]
        public static readonly BluetoothUuid ObjectTransfer = 0x1825;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Phone Alert Status service UUID.
        /// </summary>
        [BluetoothUti("phone_alert_status")]
        public static readonly BluetoothUuid PhoneAlertStatus = 0x180E;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Pulse Oximeter service UUID.
        /// </summary>
        [BluetoothUti("pulse_oximeter")]
        public static readonly BluetoothUuid PulseOximeter = 0x1822;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Reconnection Configuration service UUID.
        /// </summary>
        [BluetoothUti("reconnection_configuration")]
        public static readonly BluetoothUuid ReconnectionConfiguration = 0x1829;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Reference Time Update service UUID.
        /// </summary>
        [BluetoothUti("reference_time_update")]
        public static readonly BluetoothUuid ReferenceTimeUpdate = 0x1806;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Running Speed and Cadence Service UUID.
        /// </summary>
        [BluetoothUti("running_speed_and_cadence")]
        public static readonly BluetoothUuid RunningSpeedAndCadence = 0x1814;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Scan Parameters service UUID.
        /// </summary>
        [BluetoothUti("scan_parameters")]
        public static readonly BluetoothUuid ScanParameters = 0x1813;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Transport Discovery service UUID.
        /// </summary>
        [BluetoothUti("transport_discovery")]
        public static readonly BluetoothUuid TransportDiscovery = 0x1824;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Tx Power service UUID.
        /// </summary>
        [BluetoothUti("tx_power")]
        public static readonly BluetoothUuid TxPower = 0x1804;

        /// <summary>
        /// Gets the Bluetooth SIG-defined User Data service UUID.
        /// </summary>
        [BluetoothUti("user_data")]
        public static readonly BluetoothUuid UserData = 0x181C;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Weight Scale service UUID.
        /// </summary>
        [BluetoothUti("weight_scale")]
        public static readonly BluetoothUuid WeightScale = 0x181D;
    }
}
