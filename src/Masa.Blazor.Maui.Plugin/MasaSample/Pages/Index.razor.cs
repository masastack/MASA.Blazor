
using Masa.Blazor.Maui.Plugin.Bluetooth;
using Microsoft.AspNetCore.Components;

namespace MauiBlueToothDemo.Pages
{
    public partial class Index
    {
        private string _selectedDevice;

        private bool ShowProgress { get; set; }
        private List<string> BluetoothDeviceList { get; set; } = new();

        private List<string> AllDeviceResponse { get; set; } = new();

        /// <summary>
        /// Start scanning nearby Ble devices
        /// </summary>
        /// <returns></returns>
        private async Task ScanBleDeviceAsync()
        {
            if (MasaMauiBluetoothService.IsEnabled())
            {
                if ((await MasaMauiBluetoothService.CheckAndRequestBluetoothPermission())== PermissionStatus.Granted)
                {
                    ShowProgress = true;
                    var deviceList = await MasaMauiBluetoothService.ScanForDevicesAsync();
                    BluetoothDeviceList = deviceList.Where(o => !string.IsNullOrEmpty(o.Name)).Select(o => o.Name).Distinct().ToList();
                    ShowProgress = false;
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Bluetooth not enabled");
            }

        }

        /// <summary>
        /// Send a Ble command 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private async Task SendDataAsync(string cmd = "AT+QVERSION")
        {
            var byteData = System.Text.Encoding.Default.GetBytes(cmd);
            await SendDataAsync(_selectedDevice, byteData);
        }

        /// <summary>
        /// Send a Ble command to a specific device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="byteData"></param>
        /// <returns></returns>
        private async Task SendDataAsync(string deviceName, byte[] byteData)
        {
            if (byteData.Any())
            {
                AllDeviceResponse = new List<string>();
                if (MasaMauiBluetoothService.IsEnabled())
                {
                    await MasaMauiBluetoothService.SendDataAsync(deviceName,
                        Guid.Parse("0000ffff-0000-1000-8000-00805f9b34fb"), null, byteData, OnCharacteristicChanged);
                }

            }
        }

        /// <summary>
        /// Characteristic change callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnCharacteristicChanged(object sender, GattCharacteristicValueChangedEventArgs e)
        {
            var deviceResponse = System.Text.Encoding.Default.GetString(e.Value);
            AllDeviceResponse.Add(deviceResponse);

            InvokeAsync(StateHasChanged);
        }
    }
}
