using BlazorComponent;
using Masa.Blazor.Doc.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Masa.Blazor.Doc.Pages
{
    public partial class Home
    {
        private int _onboarding = 0;
        private int _length = 1;

        [CascadingParameter]
        public BaseLayout BaseLayout { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; } = default!;

        [CascadingParameter(Name = "Lang")]
        public bool IsChinese { get; set; }

        public StringNumber OnBoarding
        {
            get => _onboarding;
            set => _onboarding = value.AsT1;
        }

        private async Task Toggle(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                await JSRuntime.InvokeVoidAsync("window.open", url);
            }
        }

        public string T(string key)
        {
            var content = BaseLayout.T(key);
            return content;
        }
    }
}
