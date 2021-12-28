using MASA.Blazor.Doc.Models;
using MASA.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Doc.Components
{
    public partial class BottomNav : IDisposable
    {
        private List<DemoMenuItemModel> _menuItems = new();
        private DemoMenuItemModel _prevItem = new();
        private DemoMenuItemModel _nextItem = new();

        [Inject]
        public DemoService DemoService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (_menuItems.Count == 0)
            {
                var menus = await DemoService.GetMenuAsync();
                VisitMenuItems(menus, ref _menuItems);
            }

            NavigationManager.LocationChanged += OnLocationChanged;
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

            var currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
            var currentIndex = _menuItems.FindIndex(item => item.Url == currentUrl);

            var prevIndex = currentIndex - 1;
            var nextIndex = currentIndex + 1;

            _prevItem = prevIndex >= 0 ? _menuItems[prevIndex] : new DemoMenuItemModel();
            _nextItem = nextIndex >= 0 && nextIndex <= _menuItems.Count - 1 ? _menuItems[nextIndex] : new DemoMenuItemModel();

            await InvokeAsync(StateHasChanged);
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
                if (!string.IsNullOrEmpty(menu.Url))
                {
                    menuItems.Add(menu);
                }

                if (menu.Children != null && menu.Children.Length > 0)
                {
                    VisitMenuItems(menu.Children, ref menuItems);
                }
            }
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}
