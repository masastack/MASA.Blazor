using ZXing;
using ZXing.Net.Maui;

namespace Masa.Blazor.Maui.Plugin.QrCode;

public partial class MasaBarcodeReader : ContentPage
{
    public delegate void BarcodeDetected(string barcodeResult);
    public event BarcodeDetected OnBarcodeDetected;
    private bool _hasResult;
    public MasaBarcodeReader()
    {
        InitializeComponent();
        BarcodeReader.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = false,
            Multiple = false
        };
    }
    private void CameraBarcodeReaderView_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        if (!_hasResult)
        {
            Dispatcher.Dispatch(() =>
            {
                if (OnBarcodeDetected != null)
                {
                    // Notify callers that a barcode was read
                    OnBarcodeDetected(e.Results[0].Value);
                    _hasResult = true;
                    CloseReader();
                }
            });
        }

    }

    private void CloseReader()
    {
       Application.Current.MainPage.Navigation.PopModalAsync(true);
    }
    private void ReturnToBlazor_Clicked(object sender, EventArgs e)
    {
        BarcodeReader.IsTorchOn = false;
        CloseReader();
    }
    void TorchButton_Clicked(object sender, EventArgs e)
    {
        BarcodeReader.IsTorchOn = !BarcodeReader.IsTorchOn;
    }
}