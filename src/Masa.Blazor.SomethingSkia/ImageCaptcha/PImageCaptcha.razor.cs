using SkiaSharp;

namespace Masa.Blazor.Presets
{
    public partial class PImageCaptcha
    {
        [Inject]
        public virtual IJSRuntime Js { get; set; } = null!;

        [Parameter]
        public StringNumber? Height { get; set; }

        [Parameter]
        [MassApiParameter(80)]
        public int CaptchaWidth { get; set; } = 80;

        [Parameter]
        public int CaptchaHeight { get; set; }

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Clearable { get; set; }

        [Parameter]
        [MassApiParameter("$clear")]
        public string? ClearIcon { get; set; } = "$clear";

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public bool Solo { get; set; }

        [Parameter]
        public bool Filled { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool Rounded { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public string? Value { get; set; }

        [Parameter]
        public EventCallback<string?> ValueChanged { get; set; }

        [Parameter]
        public string? Placeholder { get; set; }

        [Parameter]
        public string? VerifyCode { get; set; }

        [Parameter]
        public string? ErrorMessage { get; set; }

        [Parameter, EditorRequired]
        public Func<Task<string>> OnRefresh { get; set; } = null!;

        [Parameter]
        public string? TextFieldStyle { get; set; }

        [Parameter]
        public string? ImageStyle { get; set; }

        [Parameter]
        public string? TextFieldClass { get; set; }

        [Parameter]
        public string? ImageClass { get; set; }

        private string? _captchaCode;

        private readonly Random _random = new();

        private string? _imageUrl;
        private CaptchaGenerator? _generator;
        private MTextField<string>? _textField;
        private IEnumerable<Func<string, StringBoolean>>? _rules;

        public string? CaptchaCode
        {
            get => _captchaCode;
            set
            {
                if (_captchaCode == value) return;

                _captchaCode = value ?? "";

                GenerateImage();
            }
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            OnRefresh.ThrowIfNull("ImageCaptcha");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await RefreshCode();

                _rules = new List<Func<string, StringBoolean>>()
                {
                    v =>
                    {
                        if (string.IsNullOrEmpty(VerifyCode))
                        {
                            return v == CaptchaCode ? true : ErrorMessage;
                        }

                        return v == VerifyCode ? true : ErrorMessage;
                    }
                };

                if (CaptchaHeight == 0)
                {
                    var textFieldRect =
                        await Js.InvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, _textField!.InputSlotElement);
                    CaptchaHeight = (int)textFieldRect.Height;
                }

                _generator = new CaptchaGenerator(CaptchaWidth, CaptchaHeight);

                await GenerateImage();
            }
        }

        public Task OnClickAsync()
        {
            return RefreshCode();
        }

        public async Task RefreshCode()
        {
            CaptchaCode = await OnRefresh.Invoke();
        }

        public Task GenerateImage()
        {
            if (!string.IsNullOrWhiteSpace(CaptchaCode) && _generator != null)
            {
                var bgColor = new SKColor((byte)_random.Next(90, 130), (byte)_random.Next(90, 130), (byte)_random.Next(90, 130));

                var paintColor = new SKColor((byte)_random.Next(200, 256), (byte)_random.Next(200, 256), (byte)_random.Next(200, 256));

                var noisePoint = new SKColor((byte)_random.Next(130, 200), (byte)_random.Next(130, 200), (byte)_random.Next(130, 200));

                _generator.BackgroundColor = bgColor;
                _generator.PaintColor = paintColor;
                _generator.NoisePointColor = noisePoint;
                _generator.ImageHeight = CaptchaHeight;
                _generator.ImageWidth = CaptchaWidth;

                var byteArr = _generator.GenerateImageAsByteArray(CaptchaCode);
                string imageBase64Data = Convert.ToBase64String(byteArr);
                _imageUrl = string.Format("data:image/jpeg;base64,{0}", imageBase64Data);

                StateHasChanged();
            }

            return Task.CompletedTask;
        }

        [MasaApiPublicMethod]
        public async Task FocusAsync()
        {
            if (_textField is null) return;

            await _textField.FocusAsync();
        }

        [MasaApiPublicMethod]
        public async Task BlurAsync()
        {
            if (_textField is null) return;

            await _textField.BlurAsync();
        }
    }
}
