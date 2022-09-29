using CoreBluetooth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Blazor.Maui.Plugin.Bluetooth
{
    partial class BluetoothDevice
    {
        private CBPeripheral _peripheral;
        private RemoteGattServer _gatt;
        private BluetoothDevice(CBPeripheral peripheral)
        {
            _peripheral = peripheral;
        }

        public static implicit operator BluetoothDevice(CBPeripheral peripheral)
        {
            return peripheral == null ? null : new BluetoothDevice(peripheral);
        }

        public static implicit operator CBPeripheral(BluetoothDevice device)
        {
            return device._peripheral;
        }


        string GetId()
        {
            return _peripheral.Identifier.ToString();
        }

        string GetName()
        {
            return _peripheral.Name;
        }

        RemoteGattServer GetGatt()
        {
            if (_gatt == null)
            {
                _gatt = new RemoteGattServer(this);
            }

            return _gatt;
        }
    }
}
