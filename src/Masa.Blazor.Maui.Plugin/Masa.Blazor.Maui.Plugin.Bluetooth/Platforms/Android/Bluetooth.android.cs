using Android.App;
using Android.Bluetooth.LE;
using Android.Bluetooth;
using Android.Content.PM;
using Android.Content;
using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    partial class Bluetooth
    {
        internal static BluetoothManager _manager =
            (BluetoothManager)Android.App.Application.Context.GetSystemService(Android.App.Application.BluetoothService);

        private static readonly EventWaitHandle s_handle = new EventWaitHandle(false, EventResetMode.AutoReset);
        internal static Android.Bluetooth.BluetoothDevice s_device;
    }
}
