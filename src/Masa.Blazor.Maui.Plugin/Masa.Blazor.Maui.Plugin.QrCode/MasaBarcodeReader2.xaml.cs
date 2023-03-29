using BarcodeScanner.Mobile;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
namespace Masa.Blazor.Maui.Plugin.QrCode;
public partial class MasaBarcodeReader2 : ContentPage
{
    public delegate void BarcodeDetected(string barcodeResult);
    public event BarcodeDetected OnBarcodeDetected;

    public MasaBarcodeReader2()
	{
		InitializeComponent();
        BarcodeScanner.Mobile.Methods.SetSupportBarcodeFormat(BarcodeFormats.Code39 | BarcodeFormats.QRCode | BarcodeFormats.Code128);
        On<iOS>().SetUseSafeArea(true);
    }

    
    private void CameraView_OnDetected(object sender, OnDetectedEventArg e)
    {
        List<BarcodeResult> obj = e.BarcodeResults;

        string result = string.Empty;
        result = obj[0].DisplayValue;
        //for (int i = 0; i < obj.Count; i++)
        //{
        //    result += $"Type : {obj[i].BarcodeType}, Value : {obj[i].DisplayValue}{Environment.NewLine}";
        //}

        this.Dispatcher.Dispatch(async () =>
        {
            if (OnBarcodeDetected != null)
            {
                // Notify callers that a barcode was read
                OnBarcodeDetected(result);
                Camera.IsScanning = true;
                CloseReader();
            }
        });

        //Canvas.InvalidateSurface();

    }

    private void CloseReader()
    {
        Microsoft.Maui.Controls.Application.Current.MainPage.Navigation.PopModalAsync(true);
    }
    private void ReturnToBlazor_Clicked(object sender, EventArgs e)
    {
        CloseReader();
    }
}