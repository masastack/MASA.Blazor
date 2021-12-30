using System.Globalization;
using System.Net.Http.Json;
using System.Reflection;
using MASA.Blazor.Doc.Models;
using MASA.Blazor.Doc.Models.Extensions;
using MASA.Blazor.Doc.Pages;
using MASA.Blazor.Doc.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace MASA.Blazor.Doc.Services
{
    public class DemoService
    {
        private static ConcurrentCache<string, ValueTask<IDictionary<string, DemoComponentModel>>> _componentCache;
        private static ConcurrentCache<string, ValueTask<IDictionary<string, DemoComponentModel>>> _styleCache;

        private static ConcurrentCache<string, ValueTask<DemoMenuItemModel[]>> _menuCache;
        private static ConcurrentCache<string, ValueTask<DemoMenuItemModel[]>> _demoMenuCache;
        private static ConcurrentCache<string, ValueTask<DemoMenuItemModel[]>> _docMenuCache;
        private static ConcurrentCache<string, RenderFragment> _showCaseCache;
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        private string CurrentLanguage { get; set; } = "zh-CN";

        private string CurrentComponentName { get; set; }

        public DemoService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress ??= new Uri("http://127.0.0.1:5000");
            _navigationManager = navigationManager;
        }

        public void ChangeLanguage(string language)
        {
            CurrentLanguage = language;
        }

        private async Task InitializeAsync(string language)
        {
            _menuCache ??= new ConcurrentCache<string, ValueTask<DemoMenuItemModel[]>>();
            await _menuCache.GetOrAdd(language, async (currentLanguage) =>
            {
                var menuItems =
                    await _httpClient.GetFromJsonAsync<DemoMenuItemModel[]>($"_content/MASA.Blazor.Doc/meta/menu.{language}.json");
                return menuItems;
            });

            _componentCache ??= new ConcurrentCache<string, ValueTask<IDictionary<string, DemoComponentModel>>>();
            await _componentCache.GetOrAdd(language, async (currentLanguage) =>
            {
                var components =
                    await _httpClient.GetFromJsonAsync<DemoComponentModel[]>($"_content/MASA.Blazor.Doc/meta/components/components.{language}.json");
                return components.ToDictionary(x => x.Title.StructureUrl(), x => x);
            });

            _styleCache ??= new ConcurrentCache<string, ValueTask<IDictionary<string, DemoComponentModel>>>();
            await _styleCache.GetOrAdd(language, async (currentLanguage) =>
            {
                var styles =
                    await _httpClient.GetFromJsonAsync<DemoComponentModel[]>(
                        $"_content/MASA.Blazor.Doc/meta/stylesandanimations/components.{language}.json");
                return styles.ToDictionary(x => x.Title.StructureUrl(), x => x);
            });

            _demoMenuCache ??= new ConcurrentCache<string, ValueTask<DemoMenuItemModel[]>>();
            await _demoMenuCache.GetOrAdd(language, async (currentLanguage) =>
            {
                var menuItems =
                    await _httpClient.GetFromJsonAsync<DemoMenuItemModel[]>($"_content/MASA.Blazor.Doc/meta/demos.{language}.json");
                return menuItems;
            });

            _docMenuCache ??= new ConcurrentCache<string, ValueTask<DemoMenuItemModel[]>>();
            await _docMenuCache.GetOrAdd(language, async (currentLanguage) =>
            {
                var menuItems =
                    await _httpClient.GetFromJsonAsync<DemoMenuItemModel[]>($"_content/MASA.Blazor.Doc/meta/docs.{language}.json");
                return menuItems;
            });
        }

        public Task<ApiModel> GetApiAsync(string apiUrl)
        {
            return _httpClient.GetFromJsonAsync<ApiModel>(apiUrl);
        }

        public Task<DocFileModel> GetDocFileAsync(string docUrl)
        {
            return _httpClient.GetFromJsonAsync<DocFileModel>(docUrl);
        }

        public async Task InitializeDemos()
        {
            _showCaseCache ??= new ConcurrentCache<string, RenderFragment>();
            var demoTypes =
                await _httpClient.GetFromJsonAsync<string[]>($"_content/MASA.Blazor.Doc/meta/components/demoTypes.json");
            foreach (var type in demoTypes)
            {
                GetShowCase(type);
            }
        }

        public async Task<DemoComponentModel> GetComponentAsync(string componentName)
        {
            CurrentComponentName = componentName;
            await InitializeAsync(CurrentLanguage);
            return _componentCache.TryGetValue(CurrentLanguage, out var component)
                ? ((await component).TryGetValue(componentName.ToLower(), out var componetModel) ? componetModel : null)
                : null;
        }

        public async Task<DemoComponentModel> GetStyleAsync(string componentName)
        {
            CurrentComponentName = componentName;
            await InitializeAsync(CurrentLanguage);
            return _styleCache.TryGetValue(CurrentLanguage, out var component)
                ? (await component).TryGetValue(componentName.ToLower(), out var style) ? style : null
                : null;
        }

        public async Task<List<ContentsItem>> GetTitlesAsync(string currentUrl)
        {
            var menuItems = await GetMenuAsync();
            var current = menuItems
                .SelectMany(r => r.Children)
                .FirstOrDefault(r => r.Url != null && currentUrl.Contains(r.Url, StringComparison.OrdinalIgnoreCase));

            var contents = current?.Contents;
            var componentName = currentUrl.Split('/')[^1];
            if (string.IsNullOrEmpty(componentName))
            {
                componentName = currentUrl.Split('/')[^2];
            }

            if (componentName.Contains('#'))
            {
                componentName = componentName.Split("#")[0];
            }

            if (componentName.Contains('?'))
            {
                componentName = componentName.Split("?")[0];
            }

            if (contents == null && componentName != null)
            {
                var component = await GetComponentAsync(componentName);
                if (component is null) component = await GetStyleAsync(componentName);
                if (component == null) return new List<ContentsItem>();

                var demoList = component.DemoList?.OrderBy(r => r.Order).ThenBy(r => r.Name);

                contents = new List<ContentsItem>();

                List<ContentsItem> propsList = new();
                List<ContentsItem> eventsList = new();
                List<ContentsItem> contentsList = new();
                List<ContentsItem> miscList = new();

                foreach (var demo in demoList)
                {
                    var href = demo.Title.HashSection();
                    if (demo.Title.Equals("Usage", StringComparison.OrdinalIgnoreCase) || demo.Title == "使用")
                    {
                        contents.Add(new ContentsItem(demo.Title, href, 2));
                    }
                    else if (demo.Group == DemoGroup.Props)
                    {
                        propsList.Add(new ContentsItem(demo.Title, href, 4));
                    }
                    else if (demo.Group == DemoGroup.Events)
                    {
                        eventsList.Add(new ContentsItem(demo.Title, href, 4));
                    }
                    else if (demo.Group == DemoGroup.Contents)
                    {
                        contentsList.Add(new ContentsItem(demo.Title, href, 4));
                    }
                    else if (demo.Group == DemoGroup.Misc)
                    {
                        miscList.Add(new ContentsItem(demo.Title, href, 4));
                    }
                }

                if (component.OtherDocs != null)
                {
                    foreach (var (title, _) in component.OtherDocs)
                    {
                        var href = title.HashSection();
                        contents.Add(new ContentsItem(title, href, 2));
                    }
                }

                if (propsList.Any() || miscList.Any())
                {
                    contents.Add(ContentsItem.GenerateExample(CurrentLanguage));
                }

                if (propsList.Any())
                {
                    contents.Add(ContentsItem.GenerateProps(CurrentLanguage));
                    contents.AddRange(propsList);
                }

                if (eventsList.Any())
                {
                    contents.Add(ContentsItem.GenerateEvents(CurrentLanguage));
                    contents.AddRange(eventsList);
                }

                if (contentsList.Any())
                {
                    contents.Add(ContentsItem.GenerateContents(CurrentLanguage));
                    contents.AddRange(contentsList);
                }

                if (miscList.Any())
                {
                    contents.Add(ContentsItem.GenerateMisc(CurrentLanguage));
                    contents.AddRange(miscList);
                }
            }

            return contents;
        }

        public async Task<DemoMenuItemModel[]> GetMenuAsync()
        {
            await InitializeAsync(CurrentLanguage);
            return _menuCache.TryGetValue(CurrentLanguage, out var menuItems) ? await menuItems : Array.Empty<DemoMenuItemModel>();
        }

        public async ValueTask<DemoMenuItemModel[]> GetCurrentMenuItems()
        {
            var menuItems = await GetMenuAsync();
            var currentSubmenuUrl = GetCurrentSubMenuUrl();
            return menuItems.FirstOrDefault(x => x.Url == currentSubmenuUrl)?.Children ?? Array.Empty<DemoMenuItemModel>();
        }

        public string GetCurrentSubMenuUrl()
        {
            var currentUrl = _navigationManager.ToBaseRelativePath(_navigationManager.Uri);
            var originalUrl = currentUrl.IndexOf('/') > 0 ? currentUrl.Substring(currentUrl.IndexOf('/') + 1) : currentUrl;
            return string.IsNullOrEmpty(originalUrl) ? "/" : originalUrl.Split('/')[0];
        }

        public string GetCurrentUrl()
        {
            var currentUrl = _navigationManager.ToBaseRelativePath(_navigationManager.Uri);
            return currentUrl;
        }

        public RenderFragment GetShowCase(string type)
        {
            _showCaseCache ??= new ConcurrentCache<string, RenderFragment>();
            return _showCaseCache.GetOrAdd(type, t =>
            {
                var showCase = Type.GetType($"{Assembly.GetExecutingAssembly().GetName().Name}.{type}");

                void ShowCase(RenderTreeBuilder builder)
                {
                    if (showCase != null)
                    {
                        builder.OpenComponent(0, showCase);
                        builder.CloseComponent();
                    }
                }

                return ShowCase;
            });
        }

        public async Task<DemoMenuItemModel[]> GetPrevNextMenu(string type, string currentTitle)
        {
            await InitializeAsync(CurrentLanguage);
            var items = Array.Empty<DemoMenuItemModel>();

            if (type.ToLowerInvariant() == "docs")
            {
                items = _docMenuCache.TryGetValue(CurrentLanguage, out var menuItems)
                    ? (await menuItems).OrderBy(x => x.Order).ToArray()
                    : Array.Empty<DemoMenuItemModel>();
                currentTitle = $"docs/{currentTitle}";
            }
            else
            {
                items = _demoMenuCache.TryGetValue(CurrentLanguage, out var menuItems)
                    ? (await menuItems)
                    .OrderBy(x => x.Order)
                    .SelectMany(x => x.Children)
                    .ToArray()
                    : Array.Empty<DemoMenuItemModel>();

                currentTitle = $"components/{currentTitle}";
            }

            for (var i = 0; i < items.Length; i++)
            {
                if (currentTitle.Equals(items[i].Url, StringComparison.InvariantCultureIgnoreCase))
                {
                    var prev = i == 0 ? null : items[i - 1];
                    var next = i == items.Length - 1 ? null : items[i + 1];
                    return new[] { prev, next };
                }
            }

            return new DemoMenuItemModel[] { null, null };
        }
    }
}