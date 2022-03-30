using Masa.Blazor.Doc.Services;
using System.Globalization;

namespace Masa.Blazor.Doc.Pages
{
    public partial class Index
    {
        private Recommend[] _recommends = { };

        private Product[] _products = { };

        private MoreProps[] _moreArticles = { };

        private async void HandleLanguageChanged(object _, CultureInfo culture)
        {
            await InvokeAsync(StateHasChanged);
        }
    }
}
