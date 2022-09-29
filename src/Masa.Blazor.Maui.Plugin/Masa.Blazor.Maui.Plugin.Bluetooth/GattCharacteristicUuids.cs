using System.Reflection;

namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    /// <summary>
    /// Provides characteristic UUIDs for common GATT characteristics.
    /// </summary>
    /// <remarks>To view a list of all Bluetooth SIG-defined characteristic UUIDs, see <a href="https://www.bluetooth.com/specifications/gatt/characteristics">Bluetooth SIG-defined Characteristic UUIDs</a>.</remarks>
    [BluetoothUti(Namespace)]
    public static class GattCharacteristicUuids
    {
        internal const string Namespace = "org.bluetooth.characteristic";

        /// <summary>
        /// Returns the Uuid for a characteristic given the Uniform Type Identifier.
        /// </summary>
        /// <param name="bluetoothUti">Uniform Type Identifier of the characteristic e.g. aerobic_heart_rate_lower_limit</param>
        /// <returns>The characteristic Uuid on success else Guid.Empty.</returns>
        public static BluetoothUuid FromBluetoothUti(string bluetoothUti)
        {
            string requestedUti = bluetoothUti.ToLower();
            if (requestedUti.StartsWith(Namespace))
            {
                requestedUti = requestedUti.Replace(Namespace + ".", string.Empty);
            }

            var fields = typeof(GattCharacteristicUuids).GetFields(BindingFlags.Static | BindingFlags.Public);
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

        public static string GetCharacteristicName(BluetoothUuid uuid, bool includeNamespace = false)
        {
            var shortid = BluetoothUuid.TryGetShortId(uuid);
            if (shortid.HasValue && (shortid.Value & 0xF200) == 0x2200)
            {
                var fields = typeof(GattCharacteristicUuids).GetFields(BindingFlags.Static | BindingFlags.Public);
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
        /// Lower limit of the heart rate where the user enhances his endurance while exercising.
        /// </summary>
        [BluetoothUti("aerobic_heart_rate_lower_limit")]
        public static readonly BluetoothUuid AerobicHeartRateLowerLimit = 0x2A7E;

        [BluetoothUti("aerobic_heart_rate_upper_limit")]
        public static readonly BluetoothUuid AerobicHeartRateUpperLimit = 0x2A84;

        [BluetoothUti("aerobic_threshold")]
        public static readonly BluetoothUuid AerobicThreshold = 0x2A7F;

        [BluetoothUti("age")]
        public static readonly BluetoothUuid Age = 0x2A80;

        [BluetoothUti("aggregate")]
        public static readonly BluetoothUuid Aggregate = 0x2A5A;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Alert Category ID characteristic UUID.
        /// </summary>
        [BluetoothUti("alert_category_id")]
        public static readonly BluetoothUuid AlertCategoryId = 0x2A43;

        /// <summary>
        /// Gets the Bluetooth SIG-Defined Alert Category ID Bit Mask characteristic UUID
        /// </summary>
        [BluetoothUti("alert_category_id_bit_mask")]
        public static readonly BluetoothUuid AlertCategoryIdBitMask = 0x2A42;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Alert Level characteristic UUID.
        /// </summary>
        [BluetoothUti("alert_level")]
        public static readonly BluetoothUuid AlertLevel = 0x2A06;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Alert Notification Control Point characteristic UUID.
        /// </summary>
        [BluetoothUti("alert_notification_control_point")]
        public static readonly BluetoothUuid AlertNotificationControlPoint = 0x2A44;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Alert Status characteristic UUID.
        /// </summary>
        [BluetoothUti("alert_status")]
        public static readonly BluetoothUuid AlertStatus = 0x2A3F;

        [BluetoothUti("altitude")]
        public static readonly BluetoothUuid Altitude = 0x2AB3;

        [BluetoothUti("anaerobic_heart_rate_lower_limit")]
        public static readonly BluetoothUuid AnaerobicHeartRateLowerLimit = 0x2A81;

        [BluetoothUti("anaerobic_heart_rate_upper_limit")]
        public static readonly BluetoothUuid AnaerobicHeartRateUpperLimit = 0x2A82;

        [BluetoothUti("anaerobic_threshold")]
        public static readonly BluetoothUuid AnaerobicThreshold = 0x2A83;

        [BluetoothUti("analog")]
        public static readonly BluetoothUuid Analog = 0x2A58;

        [BluetoothUti("analog_output")]
        public static readonly BluetoothUuid AnalogOutput = 0x2A59;

        [BluetoothUti("apparent_wind_direction")]
        public static readonly BluetoothUuid ApparentWindDirection = 0x2A73;

        [BluetoothUti("apparent_wind_speed")]
        public static readonly BluetoothUuid ApparentWindSpeed = 0x2A72;

        [BluetoothUti("gap.appearance")]
        public static readonly BluetoothUuid Appearance = 0x2A01;

        [BluetoothUti("barometric_pressure_trend")]
        public static readonly BluetoothUuid BarometricPressureTrend = 0x2AA3;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Battery Level characteristic UUID.
        /// </summary>
        [BluetoothUti("battery_level")]
        public static readonly BluetoothUuid BatteryLevel = 0x2A19;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Battery Level State characteristic UUID.
        /// </summary>
        [BluetoothUti("battery_level_state")]
        public static readonly BluetoothUuid BatteryLevelState = 0x2A1B;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Battery Level State characteristic UUID.
        /// </summary>
        [BluetoothUti("battery_power_state")]
        public static readonly BluetoothUuid BatteryPowerState = 0x2A1A;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Blood Pressure Feature characteristic UUID.
        /// </summary>
        [BluetoothUti("blood_pressure_feature")]
        public static readonly BluetoothUuid BloodPressureFeature = 0x2A49;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Blood Pressure Measurement Characteristic UUID.
        /// </summary>
        [BluetoothUti("blood_pressure_measurement")]
        public static readonly BluetoothUuid BloodPressureMeasurement = 0x2A35;

        [BluetoothUti("body_composition_feature")]
        public static readonly BluetoothUuid BodyCompositionFeature = 0x2A9B;

        [BluetoothUti("body_composition_measurement")]
        public static readonly BluetoothUuid BodyCompositionMeasurement = 0x2A9C;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Body Sensor Location characteristic UUID.
        /// </summary>
        [BluetoothUti("body_sensor_location")]
        public static readonly BluetoothUuid BodySensorLocation = 0x2A38;

        [BluetoothUti("bond_management_control_point")]
        public static readonly BluetoothUuid BondManagementControlPoint = 0x2AA4;

        [BluetoothUti("bond_management_feature")]
        public static readonly BluetoothUuid BondManagementFeatures = 0x2AA5;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Boot Keyboard Input Report characteristic UUID.
        /// </summary>
        [BluetoothUti("boot_keyboard_input_report")]
        public static readonly BluetoothUuid BootKeyboardInputReport = 0x2A22;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Boot Keyboard Output Report characteristic UUID.
        /// </summary>
        [BluetoothUti("boot_keyboard_output_report")]
        public static readonly BluetoothUuid BootKeyboardOutputReport = 0x2A32;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Boot Mouse Input Report characteristic UUID.
        /// </summary>
        [BluetoothUti("boot_mouse_input_report")]
        public static readonly BluetoothUuid BootMouseInputReport = 0x2A33;

        public static readonly BluetoothUuid BssControlPoint = 0x2B2B;

        public static readonly BluetoothUuid BssResponse = 0x2B2C;

        [BluetoothUti("gap.central_address_resolution")]
        public static readonly BluetoothUuid CentralAddressResolution = 0x2AA6;

        [BluetoothUti("cgm_feature")]
        public static readonly BluetoothUuid CgmFeature = 0x2AA8;

        [BluetoothUti("cgm_measurement")]
        public static readonly BluetoothUuid CgmMeasurement = 0x2AA7;

        [BluetoothUti("cgm_session_run_time")]
        public static readonly BluetoothUuid CgmSessionRunTime = 0x2AAB;

        [BluetoothUti("cgm_session_start_time")]
        public static readonly BluetoothUuid CgmSessionStartTime = 0x2AAA;

        [BluetoothUti("cgm_specific_ops_control_point")]
        public static readonly BluetoothUuid CgmSpecificOpsControlPoint = 0x2AAC;

        [BluetoothUti("cgm_status")]
        public static readonly BluetoothUuid CgmStatus = 0x2AA9;

        public static readonly BluetoothUuid ClientSupportedFeatures = 0x2B29;

        [BluetoothUti("cross_trainer_data")]
        public static readonly BluetoothUuid CrossTrainerData = 0x2ACE;

        /// <summary>
        /// Gets the Bluetooth SIG-defined CSC Feature characteristic UUID.
        /// </summary>
        [BluetoothUti("csc_feature")]
        public static readonly BluetoothUuid CscFeature = 0x2A5C;

        /// <summary>
        /// Gets the Bluetooth SIG-defined CSC Measurement characteristic UUID.
        /// </summary>
        [BluetoothUti("csc_measurement")]
        public static readonly BluetoothUuid CscMeasurement = 0x2A5B;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Current Time characteristic UUID.
        /// </summary>
        [BluetoothUti("current_time")]
        public static readonly BluetoothUuid CurrentTime = 0x2A2B;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Cycling Power Control Point characteristic UUID.
        /// </summary>
        [BluetoothUti("cycling_power_control_point")]
        public static readonly BluetoothUuid CyclingPowerControlPoint = 0x2A66;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Cycling Power Feature characteristic UUID.
        /// </summary>
        [BluetoothUti("cycling_power_feature")]
        public static readonly BluetoothUuid CyclingPowerFeature = 0x2A65;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Cycling Power Measurement characteristic UUID.
        /// </summary>
        [BluetoothUti("cycling_power_measurement")]
        public static readonly BluetoothUuid CyclingPowerMeasurement = 0x2A63;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Cycling Power Vector characteristic UUID.
        /// </summary>
        [BluetoothUti("cycling_power_vector")]
        public static readonly BluetoothUuid CyclingPowerVector = 0x2A64;

        [BluetoothUti("database_change_increment")]
        public static readonly BluetoothUuid DatabaseChangeIncrement = 0x2A99;

        [BluetoothUti("database_hash")]
        public static readonly BluetoothUuid DatabaseHash = 0x2B2A;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Date of Birth characteristic UUID.
        /// </summary>
        [BluetoothUti("date_of_birth")]
        public static readonly BluetoothUuid DateOfBirth = 0x2A85;

        [BluetoothUti("date_of_threshold_assessment")]
        public static readonly BluetoothUuid DateOfThresholdAssessment = 0x2A86;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Date Time characteristic UUID.
        /// </summary>
        [BluetoothUti("date_time")]
        public static readonly BluetoothUuid DateTime = 0x2A08;

        [BluetoothUti("date_utc")]
        public static readonly BluetoothUuid DateUtc = 0x2AED;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Day Date Time characteristic UUID.
        /// </summary>
        [BluetoothUti("day_date_time")]
        public static readonly BluetoothUuid DayDateTime = 0x2A0A;

        /// <summary>
        /// Gets the Bluetooth SIG-defined Day Of Week characteristic UUID.
        /// </summary>
        [BluetoothUti("day_of_week")]
        public static readonly BluetoothUuid DayOfWeek = 0x2A09;

        [BluetoothUti("descriptor_value_changed")]
        public static readonly BluetoothUuid DescriptorValueChanged = 0x2A7D;

        [BluetoothUti("gap.device_name")]
        public static readonly BluetoothUuid DeviceName = 0x2A00;

        [BluetoothUti("dew_point")]
        public static readonly BluetoothUuid DewPoint = 0x2A7B;

        [BluetoothUti("digital")]
        public static readonly BluetoothUuid Digital = 0x2A56;

        [BluetoothUti("digital_output")]
        public static readonly BluetoothUuid DigitalOutput = 0x2A57;

        /// <summary>
        /// Gets the Bluetooth SIG-defined DST Offset characteristic UUID.
        /// </summary>
        [BluetoothUti("dst_offset")]
        public static readonly BluetoothUuid DstOffset = 0x2A0D;

        [BluetoothUti("elevation")]
        public static readonly BluetoothUuid Elevation = 0x2A6C;

        [BluetoothUti("email_address")]
        public static readonly BluetoothUuid EmailAddress = 0x2A87;

        public static readonly BluetoothUuid EmergencyId = 0x2B2D;
        public static readonly BluetoothUuid EmergencyText = 0x2B2E;

        [BluetoothUti("exact_time_100")]
        public static readonly BluetoothUuid ExactTime100 = 0x2A0B;

        [BluetoothUti("exact_time_256")]
        public static readonly BluetoothUuid ExactTime256 = 0x2A0C;

        [BluetoothUti("fat_burn_heart_rate_lower_limit")]
        public static readonly BluetoothUuid FatBurnHeartRateLowerLimit = 0x2A88;

        [BluetoothUti("fat_burn_heart_rate_upper_limit")]
        public static readonly BluetoothUuid FatBurnHeartRateUpperLimit = 0x2A89;

        [BluetoothUti("firmware_revision_string")]
        public static readonly BluetoothUuid FirmwareRevisionString = 0x2A26;

        [BluetoothUti("first_name")]
        public static readonly BluetoothUuid FirstName = 0x2A8A;

        [BluetoothUti("fitness_machine_control_point")]
        public static readonly BluetoothUuid FitnessMachineControlPoint = 0x2AD9;

        [BluetoothUti("fitness_machine_feature")]
        public static readonly BluetoothUuid FitnessMachineFeature = 0x2ACC;

        [BluetoothUti("fitness_machine_status")]
        public static readonly BluetoothUuid FitnessMachineStatus = 0x2ADA;

        [BluetoothUti("five_zone_heart_rate_limits")]
        public static readonly BluetoothUuid FiveZoneHeartRateLimits = 0x2A8B;

        [BluetoothUti("floor_number")]
        public static readonly BluetoothUuid FloorNumber = 0x2AB2;


        [BluetoothUti("gender")]
        public static readonly BluetoothUuid Gender = 0x2A8C;

        [BluetoothUti("glucose_feature")]
        public static readonly BluetoothUuid GlucoseFeature = 0x2A51;

        [BluetoothUti("glucose_measurement")]
        public static readonly BluetoothUuid GlucoseMeasurement = 0x2A18;

        [BluetoothUti("glucose_measurement_context")]
        public static readonly BluetoothUuid GlucoseMeasurementContext = 0x2A34;

        [BluetoothUti("gust_factor")]
        public static readonly BluetoothUuid GustFactor = 0x2A74;

        [BluetoothUti("hardware_revision_string")]
        public static readonly BluetoothUuid HardwareRevisionString = 0x2A27;

        [BluetoothUti("heart_rate_control_point")]
        public static readonly BluetoothUuid HeartRateControlPoint = 0x2A39;

        [BluetoothUti("heart_rate_max")]
        public static readonly BluetoothUuid HeartRateMax = 0x2A8D;

        [BluetoothUti("heart_rate_measurement")]
        public static readonly BluetoothUuid HeartRateMeasurement = 0x2A37;

        [BluetoothUti("heat_index")]
        public static readonly BluetoothUuid HeatIndex = 0x2A7A;

        [BluetoothUti("height")]
        public static readonly BluetoothUuid Height = 0x2A8E;

        [BluetoothUti("hid_control_point")]
        public static readonly BluetoothUuid HidControlPoint = 0x2A4C;

        [BluetoothUti("hid_information")]
        public static readonly BluetoothUuid HidInformation = 0x2A4A;

        [BluetoothUti("hip_circumference")]
        public static readonly BluetoothUuid HipCircumference = 0x2A8F;

        [BluetoothUti("http_control_point")]
        public static readonly BluetoothUuid HttpControlPoint = 0x2ABA;

        [BluetoothUti("http_entity_body")]
        public static readonly BluetoothUuid HttpEntityBody = 0x2AB9;

        [BluetoothUti("http_headers")]
        public static readonly BluetoothUuid HttpHeaders = 0x2AB7;

        [BluetoothUti("http_status_code")]
        public static readonly BluetoothUuid HttpStatusCode = 0x2AB8;

        [BluetoothUti("https_security")]
        public static readonly BluetoothUuid HttpsSecurity = 0x2ABB;

        [BluetoothUti("humidity")]
        public static readonly BluetoothUuid Humidity = 0x2A6F;

        [BluetoothUti("idd_annunciation_status")]
        public static readonly BluetoothUuid IddAnnunciationStatus = 0x2B22;

        [BluetoothUti("idd_command_control_point")]
        public static readonly BluetoothUuid IddCommandControlPoint = 0x2B25;

        [BluetoothUti("idd_command_data")]
        public static readonly BluetoothUuid IddCommandData = 0x2B26;

        [BluetoothUti("idd_features")]
        public static readonly BluetoothUuid IddFeatures = 0x2B23;

        [BluetoothUti("idd_history_data")]
        public static readonly BluetoothUuid IddHistoryData = 0x2B28;

        [BluetoothUti("idd_record_access_control_point")]
        public static readonly BluetoothUuid IddRecordAccessControlPoint = 0x2B27;

        [BluetoothUti("idd_status")]
        public static readonly BluetoothUuid IddStatus = 0x2B21;

        [BluetoothUti("idd_status_changed")]
        public static readonly BluetoothUuid IddStatusChanged = 0x2B20;

        [BluetoothUti("idd_status_reader_control_point")]
        public static readonly BluetoothUuid IddStatusReaderControlPoint = 0x2B24;

        [BluetoothUti("indoor_bike_data")]
        public static readonly BluetoothUuid IndoorBikeData = 0x2AD2;

        [BluetoothUti("indoor_positioning_configuration")]
        public static readonly BluetoothUuid IndoorPositioningConfiguration = 0x2AAD;

        [BluetoothUti("intermediate_cuff_pressure")]
        public static readonly BluetoothUuid IntermediateCuffPressure = 0x2A36;

        [BluetoothUti("intermediate_temperature")]
        public static readonly BluetoothUuid IntermediateTemperature = 0x2A1E;

        [BluetoothUti("irradiance")]
        public static readonly BluetoothUuid Irradiance = 0x2A77;

        [BluetoothUti("language")]
        public static readonly BluetoothUuid Language = 0x2AA2;

        [BluetoothUti("last_name")]
        public static readonly BluetoothUuid LastName = 0x2A90;

        [BluetoothUti("latitude")]
        public static readonly BluetoothUuid Latitude = 0x2AAE;

        [BluetoothUti("ln_control_point")]
        public static readonly BluetoothUuid LNControlPoint = 0x2A6B;

        [BluetoothUti("ln_feature")]
        public static readonly BluetoothUuid LNFeature = 0x2A6A;

        [BluetoothUti("local_east_coordinate")]
        public static readonly BluetoothUuid LocalEastCoordinate = 0x2AB1;

        [BluetoothUti("local_north_coordinate")]
        public static readonly BluetoothUuid LocalNorthCoordinate = 0x2AB0;

        [BluetoothUti("local_time_information")]
        public static readonly BluetoothUuid LocalTimeInformation = 0x2A0F;

        [BluetoothUti("location_and_speed")]
        public static readonly BluetoothUuid LocationAndSpeed = 0x2A67;

        [BluetoothUti("location_name")]
        public static readonly BluetoothUuid LocationName = 0x2AB5;

        [BluetoothUti("longitude")]
        public static readonly BluetoothUuid Longitude = 0x2AAF;

        [BluetoothUti("magnetic_declination")]
        public static readonly BluetoothUuid MagneticDeclination = 0x2A2C;

        [BluetoothUti("Magnetic_flux_density_2D")]
        public static readonly BluetoothUuid MagneticFluxDensity2D = 0x2AA0;

        [BluetoothUti("Magnetic_flux_density_3D")]
        public static readonly BluetoothUuid MagneticFluxDensity3D = 0x2AA1;

        [BluetoothUti("manufacturer_name_string")]
        public static readonly BluetoothUuid ManufacturerNameString = 0x2A29;

        [BluetoothUti("maximum_recommended_heart_rate")]
        public static readonly BluetoothUuid MaximumRecommendedHeartRate = 0x2A91;

        [BluetoothUti("measurement_interval")]
        public static readonly BluetoothUuid MeasurementInterval = 0x2A21;

        [BluetoothUti("model_number_string")]
        public static readonly BluetoothUuid ModelNumberString = 0x2A24;

        [BluetoothUti("navigation")]
        public static readonly BluetoothUuid Navigation = 0x2A68;

        [BluetoothUti("network_availability")]
        public static readonly BluetoothUuid NetworkAvailability = 0x2A3E;

        [BluetoothUti("new_alert")]
        public static readonly BluetoothUuid NewAlert = 0x2A46;

        [BluetoothUti("object_action_control_point")]
        public static readonly BluetoothUuid ObjectActionControlPoint = 0x2AC5;

        [BluetoothUti("object_changed")]
        public static readonly BluetoothUuid ObjectChanged = 0x2AC8;

        [BluetoothUti("object_first_created")]
        public static readonly BluetoothUuid ObjectFirstCreated = 0x2AC1;

        [BluetoothUti("object_id")]
        public static readonly BluetoothUuid ObjectID = 0x2AC3;

        [BluetoothUti("object_last_modified")]
        public static readonly BluetoothUuid ObjectLastModified = 0x2AC2;

        [BluetoothUti("object_list_control_point")]
        public static readonly BluetoothUuid ObjectListControlPoint = 0x2AC6;

        [BluetoothUti("object_list_filter")]
        public static readonly BluetoothUuid ObjectListFilter = 0x2AC7;

        [BluetoothUti("object_name")]
        public static readonly BluetoothUuid ObjectName = 0x2ABE;

        [BluetoothUti("object_properties")]
        public static readonly BluetoothUuid ObjectProperties = 0x2AC4;

        [BluetoothUti("object_size")]
        public static readonly BluetoothUuid ObjectSize = 0x2AC0;

        [BluetoothUti("object_type")]
        public static readonly BluetoothUuid ObjectType = 0x2ABF;

        [BluetoothUti("ots_feature")]
        public static readonly BluetoothUuid OtsFeature = 0x2ABD;

        [BluetoothUti("gap.peripheral_preferred_connection_parameters")]
        public static readonly BluetoothUuid PeripheralPreferredConnectionParameters = 0x2A04;

        [BluetoothUti("gap.peripheral_privacy_flag")]
        public static readonly BluetoothUuid PeripheralPrivacyFlag = 0x2A02;

        [BluetoothUti("plx_spot_check_measurement")]
        public static readonly BluetoothUuid PlxSpotCheckMeasurement = 0x2A5E;

        [BluetoothUti("plx_continuous_measurement")]
        public static readonly BluetoothUuid PlxContinuousMeasurement = 0x2A5F;

        [BluetoothUti("plx_features")]
        public static readonly BluetoothUuid PlxFeatures = 0x2A60;

        [BluetoothUti("pollen_concentration")]
        public static readonly BluetoothUuid PollenConcentration = 0x2A75;

        [BluetoothUti("position_quality")]
        public static readonly BluetoothUuid PositionQuality = 0x2A69;

        [BluetoothUti("pressure")]
        public static readonly BluetoothUuid Pressure = 0x2A6D;

        [BluetoothUti("rainfall")]
        public static readonly BluetoothUuid Rainfall = 0x2A78;

        [BluetoothUti("gap.reconnection_address")]
        public static readonly BluetoothUuid ReconnectionAddress = 0x2A03;

        [BluetoothUti("reference_time_information")]
        public static readonly BluetoothUuid ReferenceTimeInformation = 0x2A14;

        [BluetoothUti("ieee_11073-20601_regulatoryertification_data_list")]
        public static readonly BluetoothUuid RegulatoryCertificationDataList = 0x2A2A;

        [BluetoothUti("sensor_location")]
        public static readonly BluetoothUuid SensorLocation = 0x2A5D;

        [BluetoothUti("server_supported_features")]
        public static readonly BluetoothUuid ServerSupportedFeatures = 0x2B3A;

        [BluetoothUti("gap.service_changed")]
        public static readonly BluetoothUuid ServiceChanged = 0x2A05;

        [BluetoothUti("tempurature")]
        public static readonly BluetoothUuid Tempurature = 0x2A6E;

        [BluetoothUti("tempurature_measurement")]
        public static readonly BluetoothUuid TempuratureMeasurement = 0x2A1C;

        [BluetoothUti("tempurature_type")]
        public static readonly BluetoothUuid TempuratureType = 0x2A1D;

        [BluetoothUti("time_accuracy")]
        public static readonly BluetoothUuid TimeAccuracy = 0x2A12;

        [BluetoothUti("time_source")]
        public static readonly BluetoothUuid TimeSource = 0x2A13;

        [BluetoothUti("time_update_control_point")]
        public static readonly BluetoothUuid TimeUpdateControlPoint = 0x2A16;

        [BluetoothUti("time_update_state")]
        public static readonly BluetoothUuid TimeUpdateState = 0x2A17;

        [BluetoothUti("time_with_dst")]
        public static readonly BluetoothUuid TimeWithDst = 0x2A11;

        [BluetoothUti("time_zone")]
        public static readonly BluetoothUuid TimeZone = 0x2A0E;

        [BluetoothUti("true_wind_direction")]
        public static readonly BluetoothUuid TrueWindDirection = 0x2A71;

        [BluetoothUti("true_wind_speed")]
        public static readonly BluetoothUuid TrueWindSpeed = 0x2A70;

        [BluetoothUti("uv_index")]
        public static readonly BluetoothUuid UVIndex = 0x2A76;

        [BluetoothUti("wind_chill")]
        public static readonly BluetoothUuid WindChill = 0x2A79;

    }
}
