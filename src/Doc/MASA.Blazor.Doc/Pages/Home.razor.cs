using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using BlazorComponent.Components;
using MASA.Blazor.Doc.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MASA.Blazor.Doc.Pages
{
    public partial class Home
    {
        private int _onboarding = 0;
        private int _length = 1;

        [CascadingParameter]
        public BaseLayout BaseLayout { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; } = default!;

        [Inject]
        public NavigationManager Navigation { get; set; }

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
