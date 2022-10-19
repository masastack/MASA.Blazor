using ZXing.Net.Maui;

namespace Masa.Blazor.Maui.Plugin.QrCode;

public partial class MasaBarcodeReader : ContentPage
{
    public delegate void BarcodeDetected(string barcodeResult);
    public event BarcodeDetected OnBarcodeDetected;
    private bool HasResult;
    public MasaBarcodeReader()
    {
        InitializeComponent();
        barcodeReader.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true
        };
    }
    private void CameraBarcodeReaderView_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        if (!HasResult)
        {
            Dispatcher.Dispatch(() =>
            {
                if (OnBarcodeDetected != null)
                {
                    // Notify callers that a barcode was read
                    OnBarcodeDetected(e.Results[0].Value);
                    HasResult = true;
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
        CloseReader();
    }
}