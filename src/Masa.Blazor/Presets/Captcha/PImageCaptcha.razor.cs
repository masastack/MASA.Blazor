using Microsoft.AspNetCore.Components.Web;
using SkiaSharp;

namespace Masa.Blazor.Presets
{
    public partial class PImageCaptcha
    {
        [Inject]
        public virtual IJSRuntime Js { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public int CaptchaWidth { get; set; } = 80;

        [Parameter]
        public int CaptchaHeight { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Clearable { get; set; }

        [Parameter]
        public string ClearIcon { get; set; } = "mdi-close";

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
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if(_value != value)
                {
                    _value = value;
                    if (ValueChanged.HasDelegate)
                    {
                        ValueChanged.InvokeAsync();
                    }
                }

            }
        }


        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public string CaptchaCode
        {
            get
            {
                return _captchaCode;
            }
            set
            {
                if (_captchaCode != value)
                {
                    _captchaCode = value;
                    
                    GenerateImage();
                }
            }
        }

        [Parameter]
        public string VerifyCode { get; set; }

        [Parameter]
        public string ErrorMessage { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> Refresh { get; set; }

        private string _value;
        
        private string _captchaCode;

        private IEnumerable<Func<string, StringBoolean>> rules;

        private string ImageUrl { get; set; }

        private readonly Random random = new();

        private CaptchaGenerator generator;

        private MTextField<string> TextFieldElement;

        public async Task OnClickAsync(MouseEventArgs args)
        {
            if (Refresh.HasDelegate)
            {
                await Refresh.InvokeAsync(args);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                rules = new List<Func<string, StringBoolean>>()
                {
                    v =>
                    {
                        if (string.IsNullOrEmpty(VerifyCode))
                        {
                            return v == CaptchaCode? true: ErrorMessage;
                        }
                        else
                        {
                            return v == VerifyCode? true: ErrorMessage;
                        }
                    }
                };

                if(CaptchaHeight == 0)
                {
                    var textFieldRect = await Js.InvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, TextFieldElement.InputSlotElement);
                    CaptchaHeight = (int)textFieldRect.Height;
                    generator = new CaptchaGenerator(CaptchaWidth, CaptchaHeight);
                }

                await GenerateImage();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public Task GenerateImage()
        {
            if (!string.IsNullOrWhiteSpace(CaptchaCode) && generator != null)
            {
                var bgColor = new SKColor((byte)random.Next(90, 130), (byte)random.Next(90, 130), (byte)random.Next(90, 130));

                var paintColor = new SKColor((byte)random.Next(200, 256), (byte)random.Next(200, 256), (byte)random.Next(200, 256));

                var noisePoint = new SKColor((byte)random.Next(130, 200), (byte)random.Next(130, 200), (byte)random.Next(130, 200));

                generator.BackgroundColor = bgColor;
                generator.PaintColor = paintColor;
                generator.NoisePointColor = noisePoint;
                generator.ImageHeight = CaptchaHeight;
                generator.ImageWidth = CaptchaWidth;

                var byteArr = generator.GenerateImageAsByteArray(CaptchaCode);
                string imageBase64Data2 = Convert.ToBase64String(byteArr);
                ImageUrl = string.Format("data:image/jpeg;base64,{0}", imageBase64Data2);
            }

            InvokeAsync(StateHasChanged);

            return Task.CompletedTask;
        }
    }
}
