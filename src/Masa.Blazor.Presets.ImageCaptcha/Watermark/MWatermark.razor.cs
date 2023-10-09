using System.Reflection;
using Microsoft.Extensions.Logging;
using SkiaSharp;
using SkiaSharp.Views.Blazor;

namespace Masa.Blazor;

public partial class MWatermark : BDomComponentBase
{
    [Inject] private InternalHttpClient HttpClient { get; set; } = null!;

    [Parameter] public string? Image { get; set; }

    [Parameter] public string? Text { get; set; }

    [Parameter] [ApiDefaultValue(16)] public int TextSize { get; set; } = 16;

    [Parameter] [ApiDefaultValue(-22)] public int Rotate { get; set; } = -22;

    [Parameter] [ApiDefaultValue("new SKColor(0, 0, 0, 128)")]
    public SKColor Color { get; set; } = s_defaultSkColor;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public int Left { get; set; }

    [Parameter] public int Top { get; set; }

    [Parameter] public int GapX { get; set; }

    [Parameter] public int GapY { get; set; }

    /// <summary>
    /// Determines whether the watermark is grayscale. Only works when <see cref="Image"/> is not null.
    /// </summary>
    [Parameter] public bool IsGrayscale { get; set; }

    private static readonly SKColor s_defaultSkColor = new(0, 0, 0, 128);

    private string? _base64Uri;
    private string? _backgroundSize;

    private string? _prevImage;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (AnyParameterChanged(
                nameof(Image),
                nameof(Text),
                nameof(TextSize),
                nameof(Rotate),
                nameof(Color),
                nameof(Left),
                nameof(Top),
                nameof(GapX),
                nameof(GapY),
                nameof(IsGrayscale)))
        {
            await UpdateWatermark();
        }
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider.UseBem("m-watermark")
                   .Element("content", styleAction: style =>
                   {
                       style.Add("position: absolute;top: 0;left: 0;width: 100%;height: 100%;pointer-events: none;background-repeat: repeat;")
                            .AddIf($"background-image:url({_base64Uri});background-size:{_backgroundSize};",
                                () => _base64Uri is not null && _backgroundSize is not null);
                   });
    }

    public async Task UpdateWatermark()
    {
        var drawImage = !string.IsNullOrWhiteSpace(Image);
        var drawText = !string.IsNullOrWhiteSpace(Text);

        if (!drawImage && !drawText)
        {
            throw new InvalidOperationException("Image and Text cannot be null at the same time.");
        }

        if (drawImage)
        {
            try
            {
                await DrawImageWatermark();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Draw image watermark failed.");
                drawImage = false;
            }
        }

        if (!drawImage)
        {
            DrawTextWatermark();
        }

        _ = InvokeAsync(StateHasChanged);
    }

    private void DrawTextWatermark()
    {
        var paint = new SKPaint
        {
            TextSize = TextSize,
            Color = Color,
            IsAntialias = true,
            Style = SKPaintStyle.Fill
        };

        SKRect textBounds = new();
        paint.MeasureText(Text, ref textBounds);

        DrawWatermark((int)Math.Ceiling(textBounds.Width) + 1, (int)Math.Ceiling(textBounds.Height) + 1, Text!, paint);
    }

    private async Task DrawImageWatermark()
    {
        if (_prevImage == Image)
        {
            return;
        }

        _prevImage = Image;

        await using var stream = await HttpClient.GetStreamAsync(Image!);

        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);

        using var originalBitmap = SKBitmap.Decode(memoryStream);

        var paint = new SKPaint
        {
            IsAntialias = true,
            FilterQuality = SKFilterQuality.Medium,
            Color = Color
        };

        if (IsGrayscale)
        {
            paint.ColorFilter = SKColorFilter.CreateBlendMode(Color, SKBlendMode.SrcIn);
        }

        DrawWatermark(originalBitmap.Width, originalBitmap.Height, originalBitmap, paint);
    }

    private void DrawWatermark(int originalWidth, int originalHeight, object drawContent, SKPaint paint)
    {
        var gapX = GapX;
        if (!IsDirtyParameter(nameof(GapX)))
        {
            gapX = originalWidth;
        }

        var gapY = GapY;
        if (!IsDirtyParameter(nameof(GapY)))
        {
            gapY = originalHeight;
        }

        var size = Math.Max(originalWidth, originalWidth);

        int canvasWidth = size + gapX;
        int canvasHeight = size + gapY;

        _backgroundSize = $"{canvasWidth}px {canvasHeight}px";

        using var bitmap = new SKBitmap(canvasWidth, canvasHeight);
        using var canvas = new SKCanvas(bitmap);
        canvas.Clear(SKColors.Transparent);

        canvas.Save();

        canvas.RotateDegrees(Rotate, size / 2, size  / 2);

        if (drawContent is string text)
        {
            int x, y;

            if (size == originalWidth)
            {
                x = Left;
            }
            else
            {
                x =  (size + originalWidth) / 2 + Left;
            }

            if (size == originalHeight)
            {
                y = Top;
            }
            else
            {
                y = (size + originalHeight) / 2 + Top;
            }

            canvas.DrawText(text, x, y, paint);
        }
        else if (drawContent is SKBitmap originalBitmap)
        {
            int x, y;

            if (size == originalWidth)
            {
                x = Left;
            }
            else
            {
                x =  (size - originalWidth) / 2 + Left;
            }

            if (size == originalHeight)
            {
                y = Top;
            }
            else
            {
                y = (size - originalHeight) / 2 + Top;
            }

            canvas.DrawBitmap(originalBitmap, x, y, paint);
        }
        else
        {
            throw new NotSupportedException();
        }

        canvas.Restore();

        _base64Uri = $"data:image/png;base64,{ConvertBitmapToBase64(bitmap)}";
    }


    private string ConvertBitmapToBase64(SKBitmap bitmap)
    {
        using var memoryStream = new MemoryStream();
        bitmap.Encode(memoryStream, SKEncodedImageFormat.Png, 100);

        var imageBytes = memoryStream.ToArray();

        return Convert.ToBase64String(imageBytes);
    }
}
