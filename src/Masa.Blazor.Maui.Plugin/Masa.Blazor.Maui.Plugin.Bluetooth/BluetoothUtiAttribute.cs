namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    /// <summary>
    /// Attribute to attach a Uniform Type Identifier to a Bluetooth UUID.
    /// </summary>
    /// <remarks>
    /// Usage on a class indicates a namespace such as org.bluetooth.characteristic and on a field represents the individual name.</remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
    public sealed class BluetoothUtiAttribute : Attribute
    {
        public BluetoothUtiAttribute() { }

        public BluetoothUtiAttribute(string uti)
        {
            Uti = uti;
        }

        /// <summary>
        /// Uniform Type Identifier e.g. 'org.bluetooth.service.generic_access'.
        /// </summary>
        public string Uti
        {
            get;
            private set;
        }
    }
}
