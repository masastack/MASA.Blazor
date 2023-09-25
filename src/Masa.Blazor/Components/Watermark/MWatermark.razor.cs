using SkiaSharp;

namespace Masa.Blazor;

public partial class MWatermark : BDomComponentBase
{
    private string Base64Uri { get; set; }

    private string _text;

    [Parameter] public bool Image { get; set; }

    [Parameter] public int BackgroundSizeX { get; set; } = 320;

    [Parameter] public int BackgroundSizeY { get; set; } = 222;

    [Parameter]
    public string Text
    {
        get => _text;
        set
        {
            if (string.IsNullOrWhiteSpace(value) && value == _text)
            {
                return;
            }

            _text = value;

            SetBase64Uri(_text);
        }
    }

    [Parameter] public int TextSize { get; set; } = 16;

    public void SetBase64Uri(string text, string color = "#ccc")
    {
        if (Image)
        {
            Base64Uri = text;
        }
        else
        {
            using var bitmap = new SKBitmap(120, 160);
            using var canvas = new SKCanvas(bitmap);
            canvas.Clear(SKColors.Transparent); // 设置透明背景

            using var paint = new SKPaint();
            paint.TextSize = TextSize;
            paint.Color = SKColor.Parse(color);
            paint.IsAntialias = true;
            paint.Style = SKPaintStyle.Fill;

            canvas.RotateDegrees((float)(-22 * Math.PI / 18));
            canvas.DrawText(text, 0, 160, paint);

            Base64Uri = $"data:image/png;base64,{ConvertBitmapToBase64(bitmap)}";
        }

        _ = InvokeAsync(StateHasChanged);
    }


    private string ConvertBitmapToBase64(SKBitmap bitmap)
    {
        using var memoryStream = new MemoryStream();
        bitmap.Encode(memoryStream, SKEncodedImageFormat.Png, 100);

        var imageBytes = memoryStream.ToArray();

        return Convert.ToBase64String(imageBytes);
    }
}