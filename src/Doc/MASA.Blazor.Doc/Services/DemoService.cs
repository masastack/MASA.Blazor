using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;
using BlazorComponent.Doc.Extensions;
using BlazorComponent.Doc.Models;
using MASA.Blazor.Doc.Demos.Components.Border.demo;
using MASA.Blazor.Doc.Localization;
using MASA.Blazor.Doc.Pages;
using MASA.Blazor.Doc.Shared;
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
        private readonly ILanguageService _languageService;
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private Uri _baseUrl;

        private string CurrentLanguage => _languageService.CurrentCulture.Name;

        private string CurrentComponentName { get; set; }

        public DemoService(ILanguageService languageService, HttpClient httpClient, NavigationManager navigationManager)
        {
            _languageService = languageService;
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _baseUrl = new Uri("http://localhost:5000");

            _languageService.LanguageChanged += async (sender, args) => await InitializeAsync(args.Name);
        }

        private async Task InitializeAsync(string language)
        {
            _menuCache ??= new ConcurrentCache<string, ValueTask<DemoMenuItemModel[]>>();
            await _menuCache.GetOrAdd(language, async (currentLanguage) =>
            {
                var menuItems =
                    await _httpClient.GetFromJsonAsync<DemoMenuItemModel[]>(new Uri(_baseUrl, $"_content/MASA.Blazor.Doc/meta/menu.{language}.json")
                        .ToString());
                return menuItems;
            });

            _componentCache ??= new ConcurrentCache<string, ValueTask<IDictionary<string, DemoComponentModel>>>();
            await _componentCache.GetOrAdd(language, async (currentLanguage) =>
            {
                var components =
                    await _httpClient.GetFromJsonAsync<DemoComponentModel[]>(new Uri(_baseUrl,
                        $"_content/MASA.Blazor.Doc/meta/components.{language}.json").ToString());
                return components.ToDictionary(x => x.Title.ToLower(), x => x);
            });

            _styleCache ??= new ConcurrentCache<string, ValueTask<IDictionary<string, DemoComponentModel>>>();
            await _styleCache.GetOrAdd(language, async (currentLanguage) =>
            {
                var styles =
                    await _httpClient.GetFromJsonAsync<DemoComponentModel[]>(new Uri(_baseUrl,
                        $"_content/MASA.Blazor.Doc/meta/styles-and-animations/components.{language}.json").ToString());
                return styles.ToDictionary(x => x.Title.ToLower(), x => x);
            });

            _demoMenuCache ??= new ConcurrentCache<string, ValueTask<DemoMenuItemModel[]>>();
            await _demoMenuCache.GetOrAdd(language, async (currentLanguage) =>
            {
                var menuItems =
                    await _httpClient.GetFromJsonAsync<DemoMenuItemModel[]>(new Uri(_baseUrl, $"_content/MASA.Blazor.Doc/meta/demos.{language}.json")
                        .ToString());
                return menuItems;
            });

            _docMenuCache ??= new ConcurrentCache<string, ValueTask<DemoMenuItemModel[]>>();
            await _docMenuCache.GetOrAdd(language, async (currentLanguage) =>
            {
                var menuItems =
                    await _httpClient.GetFromJsonAsync<DemoMenuItemModel[]>(new Uri(_baseUrl, $"_content/MASA.Blazor.Doc/meta/docs.{language}.json")
                        .ToString());
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
                await _httpClient.GetFromJsonAsync<string[]>(new Uri(_baseUrl, $"_content/MASA.Blazor.Doc/meta/demoTypes.json").ToString());
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
                ? ((await component).TryGetValue(componentName.ToLower(),out var componetModel)?componetModel:null)
                : null;
        }

        public async Task<DemoComponentModel> GetStyleAsync(string componentName)
        {
            CurrentComponentName = componentName;
            await InitializeAsync(CurrentLanguage);
            return _styleCache.TryGetValue(CurrentLanguage, out var component)
                ? (await component)[componentName.ToLower()]
                : null;
        }

        public async Task<List<ContentsItem>> GetTitlesAsync(string currentUrl)
        {
            var menuItems = await GetMenuAsync();
            var current = menuItems.SelectMany(r => r.Children).FirstOrDefault(r => r.Url != null && currentUrl.Contains(r.Url));

            var contents = current?.Contents;
            var componentName = currentUrl.Split('/')[^1];

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
                var components = await GetComponentAsync(componentName);
                if (components is null) components = await GetStyleAsync(componentName);
                var demoList = components.DemoList?.OrderBy(r => r.Order).ThenBy(r => r.Name);

                contents = new List<ContentsItem>();

                List<ContentsItem> propsList = new();
                List<ContentsItem> eventsList = new();
                List<ContentsItem> contentsList = new();
                List<ContentsItem> miscList = new();

                foreach (var demo in demoList)
                {
                    var href = $"/#section-" + HashHelper.Hash(demo.Title);
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

                if(string.IsNullOrEmpty(components.ApiDoc) == false)
                {
                    contents.Add(ContentsItem.GenerateApi(CurrentLanguage));
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
                    return new[] {prev, next};
                }
            }

            return new DemoMenuItemModel[] {null, null};
        }

        public async Task<Recommend[]> GetRecommend()
        {
            return await _httpClient.GetFromJsonAsync<Recommend[]>(new Uri(_baseUrl,
                $"_content/MASA.Blazor.Doc/data/recommend.{CurrentLanguage}.json").ToString());
        }

        public async Task<Product[]> GetProduct()
        {
            return await _httpClient.GetFromJsonAsync<Product[]>(new Uri(_baseUrl, $"_content/MASA.Blazor.Doc/data/products.json").ToString());
        }

        public async Task<MoreProps[]> GetMore()
        {
            return await _httpClient.GetFromJsonAsync<MoreProps[]>(new Uri(_baseUrl,
                $"_content/MASA.Blazor.Doc/data/more-list.{CurrentLanguage}.json").ToString());
        }
    }
}