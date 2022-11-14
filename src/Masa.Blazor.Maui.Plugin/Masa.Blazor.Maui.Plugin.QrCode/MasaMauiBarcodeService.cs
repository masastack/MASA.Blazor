using static Masa.Blazor.Maui.Plugin.QrCode.MasaBarcodeReader;

namespace Masa.Blazor.Maui.Plugin.QrCode
{
    // All the code in this file is included in all platforms.
    public static partial class MasaMauiBarcodeService
    {
        public static void ReadBarcode(BarcodeDetected actionBarcodeDetected)
        {
            MasaBarcodeReader barcodeReaderMauiComponent = new MasaBarcodeReader();
            barcodeReaderMauiComponent.OnBarcodeDetected += actionBarcodeDetected;
            Application.Current.MainPage.Navigation.PushModalAsync(barcodeReaderMauiComponent);
        }
    }
}