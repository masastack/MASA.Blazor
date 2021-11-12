using System;
using System.Globalization;
using System.Threading.Tasks;
using MASA.Blazor.Doc.Localization;
using MASA.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor.Doc.Pages
{
    public partial class Index : IDisposable
    {
        private Recommend[] _recommends = { };

        private Product[] _products = { };

        private MoreProps[] _moreArticles = { };

        [Inject] private ILanguageService Language { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Language.LanguageChanged += HandleLanguageChanged;
        }

        private async void HandleLanguageChanged(object _, CultureInfo culture)
        {
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            Language.LanguageChanged -= HandleLanguageChanged;
        }
    }
}
