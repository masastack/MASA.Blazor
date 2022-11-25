using Masa.Blazor.Presets;

namespace Masa.Docs.Shared.Examples.components.image_captcha
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        public Usage() : base(typeof(PImageCaptcha)) { }

        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<Index>(0);
            builder.CloseComponent();
        };
    }
}
