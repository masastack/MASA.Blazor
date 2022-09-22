
using Masa.Blazor.Maui.Plugin.Bluetooth;
using Microsoft.AspNetCore.Components;

namespace MauiBlueToothDemo.Pages
{
    public partial class Index
    {
        private string SelectedDevice;

        private bool ShowProgress { get; set; }
        private List<string> BluetoothDeviceList { get; set; } = new();

        private List<string> _allDeviceResponse = new List<string>();
        [Inject]
        private MasaMauiBluetoothService BluetoothService { get; set; }

        private async Task ScanBLEDeviceAsync()
        {
            if (BluetoothService.IsEnabled())
            {
                if (await BluetoothService.CheckAndRequestBluetoothPermission())
                {
                    ShowProgress = true;
                    var deviceList = await BluetoothService.ScanLeDeviceAsync();
                    BluetoothDeviceList = deviceList.Where(o => !string.IsNullOrEmpty(o.Name)).Select(o => o.Name).Distinct().ToList();
                    ShowProgress = false;
                }
            }
        }

        private async Task SendDataAsync(string cmd= "AT+QVERSION")
        {
            var byteData = System.Text.Encoding.Default.GetBytes(cmd);
            await SendDataAsync(SelectedDevice, byteData);
        }

        private async Task SendDataAsync(string deviceSerialNo, byte[] byteData)
        {
            if (byteData.Any())
            {
                _allDeviceResponse = new List<string>();
#if ANDROID
                await BluetoothService.SendDataAsync(deviceSerialNo,Guid.Parse("0000ffff-0000-1000-8000-00805f9b34fb"),null, byteData, onCharacteristicChanged);
#endif
            }
        }

        void onCharacteristicChanged(object sender, GattCharacteristicValueChangedEventArgs e)
        {
            var deviceResponse = System.Text.Encoding.Default.GetString(e.Value);
            _allDeviceResponse.Add(deviceResponse);
            InvokeAsync(() => { StateHasChanged(); });
        }
    }
}
