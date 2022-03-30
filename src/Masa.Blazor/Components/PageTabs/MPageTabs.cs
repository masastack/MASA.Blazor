using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics;

namespace Masa.Blazor
{
    public class MPageTabs : MTabs, IPageTabs
    {
        [EditorRequired]
        [Parameter]
        public IList<PageTabItem> Items { get; set; }

        [Parameter]
        public string CloseIcon { get; set; } = "mdi-close";

        [Parameter]
        public RenderFragment<PageTabContentContext> TabContent { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        IList<PageTabItem> IPageTabs.ComputedItems => ComputedItems;

        bool IPageTabs.NoActiveItem => NoActiveItem;

        bool IPageTabs.IsActive(PageTabItem item) => IsActive(item);

        void IPageTabs.Close(PageTabItem item) => CloseTab(item);

        Task IPageTabs.HandleOnOnReloadAsync(MouseEventArgs args) => HandleOnOnReloadAsync(args);

        Task IPageTabs.HandleOnCloseLeftAsync(MouseEventArgs args) => HandleOnCloseLeftAsync(args);

        Task IPageTabs.HandleOnCloseRightAsync(MouseEventArgs args) => HandleOnCloseRightAsync(args);

        Task IPageTabs.HandleOnCloseOtherAsync(MouseEventArgs args) => HandleOnCloseOtherAsync(args);

        protected IList<PageTabItem> ComputedItems
        {
            get
            {
                return InternalItems.Where(PageTabItemManager.IsOpened)
                    .OrderBy(item => item.Closable)
                    .ThenBy(PageTabItemManager.OpenedTime)
                    .ToList();
            }
        }

        protected bool NoActiveItem => !ComputedItems.Any(IsActive);

        protected string CurrentUrl => NavigationManager.Uri;

        protected bool IsMenuActive { get; set; }

        protected (double X, double Y) MenuPosition { get; set; }

        protected PageTabItem MenuActiveItem { get; set; }

        protected IList<PageTabItem> InternalItems { get; set; } = new List<PageTabItem>();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            HideSlider = true;
            NavigationManager.LocationChanged += OnLocationChanged;
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            OpenMatchCurrentUrlTab();
        }

        private void OpenMatchCurrentUrlTab()
        {
            var matchCurrentUrlItem = Items.FirstOrDefault(MatchCurrentUrl);
            if (matchCurrentUrlItem != null)
            {
                var activeUrl = (matchCurrentUrlItem.Match == PageTabsMatch.Prefix && matchCurrentUrlItem.Target == PageTabsTarget.Self) ? NavigationManager.ToAbsoluteUri(matchCurrentUrlItem.Url).AbsoluteUri : CurrentUrl;

                var internalItem = InternalItems.FirstOrDefault(item => item.Url == activeUrl);
                if (internalItem == null)
                {
                    internalItem = new PageTabItem(matchCurrentUrlItem.Name, activeUrl, matchCurrentUrlItem.Icon ?? "", matchCurrentUrlItem.Closable, matchCurrentUrlItem.Match, matchCurrentUrlItem.Target);
                    InternalItems.Add(internalItem);
                }

                OpenTab(internalItem);
            }
        }

        private bool MatchCurrentUrl(PageTabItem item)
        {
            var url = NavigationManager.ToAbsoluteUri(item.Url).AbsoluteUri;
            var matched = false;

            switch (item.Match)
            {
                case PageTabsMatch.Prefix:
                    matched = UrlHelper.MatchPrefix(url, CurrentUrl);
                    break;
                case PageTabsMatch.All:
                    matched = UrlHelper.Match(url, CurrentUrl);
                    break;
                default:
                    break;
            }

            return matched;
        }

        private void OpenTab(PageTabItem item)
        {
            if (item != null && !PageTabItemManager.IsOpened(item))
            {
                PageTabItemManager.Open(item);
                InvokeStateHasChanged();
            }
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-page-tabs");
                })
                .Apply("page-item", cssBuilder =>
                {
                    var isActive = (bool)cssBuilder.Data;
                    cssBuilder
                        .Add("m-page-tabs__page-item")
                        .AddIf("m-page-tabs__page-item--is-active", () => isActive);
                }, styleBuilder =>
                {
                    var isActive = (bool)styleBuilder.Data;
                    styleBuilder
                        .AddIf("display:none", () => !isActive);
                });

            AbstractProvider
                .ApplyPageTabsDefault()
                .Merge(typeof(BTab), typeof(MTab), attrs =>
                {
                    var item = (PageTabItem)attrs.Data;
                    attrs["oncontextmenu"] = CreateEventCallback<MouseEventArgs>(async args => await ShowMenuAsync(args.ClientX, args.ClientY, item));
                    attrs["__internal_preventDefault_oncontextmenu"] = true;
                })
                .Apply(typeof(BMenu), typeof(MMenu), attrs =>
                {
                    attrs[nameof(BMenu.Value)] = IsMenuActive;
                    attrs[nameof(MMenu.ValueChanged)] = CreateEventCallback<bool>(val =>
                    {
                        IsMenuActive = val;
                    });
                    attrs[nameof(BMenu.PositionX)] = MenuPosition.X;
                    attrs[nameof(BMenu.PositionY)] = MenuPosition.Y;
                    attrs[nameof(MMenu.ContentClass)] = "m-page-tabs__content";
                })
                .Apply(typeof(BIcon), typeof(MIcon), attrs =>
                {
                    var item = (PageTabItem)attrs.Data;
                    attrs[nameof(MIcon.OnClick)] = CreateEventCallback<MouseEventArgs>(args => CloseTab(item));
                    attrs["__internal_stopPropagation_onclick"] = true;
                })
                .Apply(typeof(BList), typeof(MList), attrs =>
                {
                    attrs[nameof(MList.Dense)] = true;
                })
                .Apply(typeof(BListItem), typeof(MListItem), attrs =>
                {
                    attrs[nameof(MList.Dense)] = true;
                });
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                //By default,the tab matched current url should be opened
                OpenMatchCurrentUrlTab();
            }
        }

        protected async Task ShowMenuAsync(double clientX, double clientY, PageTabItem item)
        {
            //We will change this when menu been refactored
            MenuActiveItem = item;
            IsMenuActive = false;
            await Task.Delay(BROWSER_RENDER_INTERVAL);

            MenuPosition = (clientX, clientY);
            IsMenuActive = true;
        }

        protected void CloseTab(PageTabItem item)
        {
            Debug.Assert(item != null);

            if (!item.Closable)
            {
                return;
            }

            PageTabItemManager.Close(item);
            if (IsActive(item))
            {
                //Active item has been closed,goto last or default
                var lastItem = ComputedItems.LastOrDefault();
                NavigationManager.NavigateTo(lastItem?.Url ?? "");
            }
        }

        protected bool IsActive(PageTabItem item)
        {
            return item.Url == CurrentUrl || (item.Match == PageTabsMatch.Prefix && item.Target == PageTabsTarget.Self && UrlHelper.MatchPrefix(item.Url, CurrentUrl));
        }

        protected Task HandleOnOnReloadAsync(MouseEventArgs args)
        {
            PageTabItemManager.Reload(MenuActiveItem);
            NavigationManager.NavigateTo(MenuActiveItem.Url);

            return Task.CompletedTask;
        }

        protected Task HandleOnCloseLeftAsync(MouseEventArgs args)
        {
            var startIndex = 0;
            var endIndex = ComputedItems.IndexOf(MenuActiveItem);

            if (endIndex > startIndex)
            {
                var items = ComputedItems.Take(endIndex);
                foreach (var item in items)
                {
                    CloseTab(item);
                }
            }

            return Task.CompletedTask;
        }

        protected Task HandleOnCloseRightAsync(MouseEventArgs args)
        {
            var startIndex = ComputedItems.IndexOf(MenuActiveItem) + 1;
            var endIndex = ComputedItems.Count;

            if (endIndex > startIndex)
            {
                var items = ComputedItems.Skip(startIndex).Take(endIndex - startIndex);
                foreach (var item in items)
                {
                    CloseTab(item);
                }
            }

            return Task.CompletedTask;
        }

        protected async Task HandleOnCloseOtherAsync(MouseEventArgs args)
        {
            await HandleOnCloseLeftAsync(args);
            await HandleOnCloseRightAsync(args);
        }

        protected override void Dispose(bool disposing)
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
            base.Dispose(disposing);
        }
    }
}
