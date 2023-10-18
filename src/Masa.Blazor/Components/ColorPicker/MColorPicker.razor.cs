using System.Drawing;

namespace Masa.Blazor;

public partial class MColorPicker
{
    [Parameter] [ApiDefaultValue(150)] public StringNumber CanvasHeight { get; set; } = 150;

    [Parameter] public bool Disabled { get; set; }

    [Parameter] [ApiDefaultValue(10)] public StringNumber DotSize { get; set; } = 10;

    [Parameter] public bool Flat { get; set; }

    [Parameter] public bool HideCanvas { get; set; }

    [Parameter] public bool HideSliders { get; set; }

    [Parameter] public bool HideInputs { get; set; }

    [Parameter] public bool HideModeSwitch { get; set; }

    // parameter mode

    [Parameter] public bool ShowAlpha { get; set; }

    [Parameter] public bool ShowSwatches { get; set; }

    [Parameter] public string[][] Swatches { get; set; }

    [Parameter] [ApiDefaultValue(150)] public StringNumber SwatchesMaxHeight { get; set; } = 150;

    [Parameter] public Color Value { get; set; }

    [Parameter] public EventCallback<Color> ValueChanged { get; set; }

    [Parameter] [ApiDefaultValue(300)] public StringNumber? Width { get; set; } = 300;

    protected Color InternalValue = Color.FromArgb(1, 255, 0, 0);

    private bool HideAlpha
    {
        get
        {
            if (Value.IsEmpty)
            {
                return false;
            }

            return !HasAlpha;
        }
    }

    private bool HasAlpha => ShowAlpha || Value.A != 255;
}
