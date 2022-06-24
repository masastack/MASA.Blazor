using BlazorComponent;
using Masa.Blazor.Doc.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Masa.Blazor.Doc.Pages
{
    public partial class Home
    {
        [CascadingParameter]
        public BaseLayout BaseLayout { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; } = default!;

        private int _onBoarding = 0;
        private int _length = 1;

        private StringNumber OnBoarding
        {
            get => _onBoarding;
            set => _onBoarding = value.AsT1;
        }

        private async Task Toggle(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                await JsRuntime.InvokeVoidAsync("window.open", url);
            }
        }

        private string T(string key)
        {
            var content = BaseLayout.T(key);
            return content;
        }
    }
}
