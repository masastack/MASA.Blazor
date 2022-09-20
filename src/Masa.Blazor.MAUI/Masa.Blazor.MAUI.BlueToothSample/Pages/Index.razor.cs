
using Masa.Blazor.MAUI.Bluetooth;
using Microsoft.AspNetCore.Components;

namespace MauiBlueToothDemo.Pages
{
    public partial class Index
    {
        private bool ShowProgress { get; set; }
        private List<string> BluetoothDeviceList { get; set; } = new();
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
    }
}
