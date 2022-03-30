using Masa.Blazor.Doc.Models;
using Masa.Blazor.Doc.Models.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Net.Http.Json;
using System.Reflection;

namespace Masa.Blazor.Doc.Services
{
    public class DemoService
    {
        private static ConcurrentCache<string, ValueTask<IDictionary<string, DemoComponentModel>>> _componentCache;
        private static ConcurrentCache<string, ValueTask<IDictionary<string, DemoComponentModel>>> _styleCache;

        private static ConcurrentCache<string, ValueTask<DemoMenuItemModel[]>> _menuCache;
        private static ConcurrentCache<string, RenderFragment> _showCaseCache;
        private ConcurrentCache<string, ValueTask<string[]>> _demoTypesCache;
        private ConcurrentCache<string, ValueTask<DocFileModel>> _docFileCache;
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        private string CurrentLanguage { get; set; } = "zh-CN";

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

        public Task<ApiModel> GetApiAsync(string apiUrl)
        {
            return _httpClient.GetFromJsonAsync<ApiModel>(apiUrl);
        }

        public async Task<DocFileModel> GetDocFileAsync(string docUrl)
        {
            _docFileCache ??= new ConcurrentCache<string, ValueTask<DocFileModel>>();
            var docFileTask = await _docFileCache.GetOrAdd(docUrl, async (currentDocUrl) =>
               {
                   var docFile = await _httpClient.GetFromJsonAsync<DocFileModel>(currentDocUrl);
                   return docFile;
               });

            return docFileTask;
        }

        public async Task InitializeDemos()
        {
            _showCaseCache ??= new ConcurrentCache<string, RenderFragment>();

            _demoTypesCache ??= new ConcurrentCache<string, ValueTask<string[]>>();
            await _demoTypesCache.GetOrAdd(CurrentLanguage, async (currentLanguage) =>
            {
                var demoTypes =
                await _httpClient.GetFromJsonAsync<string[]>($"_content/Masa.Blazor.Doc/meta/components/demoTypes.json");

                return demoTypes;
            });

            var demoTypes = _demoTypesCache.TryGetValue("any", out var demosTypesTask) ? await demosTypesTask : Array.Empty<string>();
            foreach (var type in demoTypes)
            {
                GetShowCase(type);
            }
        }

        public async Task<DemoComponentModel> GetComponentAsync(string componentName)
        {
            _componentCache ??= new ConcurrentCache<string, ValueTask<IDictionary<string, DemoComponentModel>>>();
            await _componentCache.GetOrAdd(CurrentLanguage, async (currentLanguage) =>
            {
                var components =
                    await _httpClient.GetFromJsonAsync<DemoComponentModel[]>($"_content/Masa.Blazor.Doc/meta/components/components.{CurrentLanguage}.json");
                return components.ToDictionary(x => x.Title.StructureUrl(), x => x);
            });

            return _componentCache.TryGetValue(CurrentLanguage, out var component)
                ? ((await component).TryGetValue(componentName.ToLower(), out var componetModel) ? componetModel : null)
                : null;
        }

        public async Task<DemoComponentModel> GetStyleAsync(string componentName)
        {
            _styleCache ??= new ConcurrentCache<string, ValueTask<IDictionary<string, DemoComponentModel>>>();
            await _styleCache.GetOrAdd(CurrentLanguage, async (currentLanguage) =>
            {
                var styles =
                    await _httpClient.GetFromJsonAsync<DemoComponentModel[]>(
                        $"_content/Masa.Blazor.Doc/meta/stylesandanimations/components.{CurrentLanguage}.json");
                return styles.ToDictionary(x => x.Title.StructureUrl(), x => x);
            });

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
            _menuCache ??= new ConcurrentCache<string, ValueTask<DemoMenuItemModel[]>>();
            await _menuCache.GetOrAdd(CurrentLanguage, async (currentLanguage) =>
            {
                var menuItems =
                    await _httpClient.GetFromJsonAsync<DemoMenuItemModel[]>($"_content/Masa.Blazor.Doc/meta/menu.{CurrentLanguage}.json");
                return menuItems;
            });

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
    }
}