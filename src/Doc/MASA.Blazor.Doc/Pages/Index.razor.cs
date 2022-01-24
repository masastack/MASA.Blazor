using System;
using System.Globalization;
using System.Threading.Tasks;
using MASA.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor.Doc.Pages
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
