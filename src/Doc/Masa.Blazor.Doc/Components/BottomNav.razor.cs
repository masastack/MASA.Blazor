using BlazorComponent.I18n;
using Masa.Blazor.Doc.Models;
using Masa.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Masa.Blazor.Doc.Components
{
    public partial class BottomNav : IDisposable
    {
        private List<DemoMenuItemModel> _menuItems = new();
        private DemoMenuItemModel _prevItem = new();
        private DemoMenuItemModel _nextItem = new();
        private int _currentIndex;

        [Inject]
        public DemoService DemoService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public I18nConfig I18nConfig { get; set; }

        [CascadingParameter]
        public bool IsChinese { get; set; }

        protected override void OnInitialized()
        {
            NavigationManager.LocationChanged += OnLocationChanged;
        }

        protected override async Task OnParametersSetAsync()
        {
            base.OnParametersSet();
            DemoService.ChangeLanguage(I18nConfig.Language ?? CultureInfo.CurrentCulture.Name);
            var menus = await DemoService.GetMenuAsync();
            _menuItems.Clear();

            VisitMenuItems(menus, ref _menuItems);
            await UpdatePrevAndNextAsync();
        }

        private void OnLocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            _ = UpdatePrevAndNextAsync();
        }

        private async Task UpdatePrevAndNextAsync()
        {
            if (_menuItems.Count == 0)
            {
                return;
            }

            var currentIndex = _currentIndex;

            var currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
            _currentIndex = _menuItems.FindIndex(item => item.Url == currentUrl);

            //CurrentIndex may be updated by OnParametersSetAsync
            if (currentIndex != _currentIndex)
            {
                UpdatePrevAndNextItem();
                await InvokeAsync(StateHasChanged);
            }
        }

        private void UpdatePrevAndNextItem()
        {
            var prevIndex = _currentIndex - 1;
            var nextIndex = _currentIndex + 1;

            _prevItem = prevIndex >= 0 ? _menuItems[prevIndex] : new DemoMenuItemModel();
            _nextItem = nextIndex >= 0 && nextIndex <= _menuItems.Count - 1 ? _menuItems[nextIndex] : new DemoMenuItemModel();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await UpdatePrevAndNextAsync();
            }
        }

        private void VisitMenuItems(DemoMenuItemModel[] menus, ref List<DemoMenuItemModel> menuItems)
        {
            foreach (var menu in menus)
            {
                if (menu.Children != null && menu.Children.Length > 0)
                {
                    VisitMenuItems(menu.Children, ref menuItems);
                }
                else
                {
                    menuItems.Add(menu);
                }
            }
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}
