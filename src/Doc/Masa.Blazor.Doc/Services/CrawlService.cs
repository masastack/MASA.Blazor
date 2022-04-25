using Masa.Blazor.Doc.Models;
using Masa.Blazor.Doc.Utils;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Masa.Blazor.Doc.Services
{
    public class CrawlService
    {
        private readonly DemoService _demoService;
        private ConcurrentCache<string, ValueTask<string>> _robotsCache;
        private readonly HttpClient _httpClient;
        private const string RobotTxtCache = nameof(RobotTxtCache);

        public CrawlService(HttpClient httpClient, DemoService demoService)
        {
            _demoService = demoService;
            _httpClient = httpClient;
            _httpClient.BaseAddress ??= new Uri("http://127.0.0.1:5000");
            _robotsCache = new ConcurrentCache<string, ValueTask<string>>();
        }

        public async Task GetRobotsTxt(HttpContext context)
        {
            var txt = await _robotsCache.GetOrAdd(RobotTxtCache, async(key) =>
            {
                var robotsTxt= await _httpClient.GetStringAsync($"_content/Masa.Blazor.Doc/robots.txt");
                return robotsTxt;
            });

            await context.Response.WriteAsync(txt);
        }

        public async Task GetSitemap(HttpContext context)
        {
            var host = $"{context.Request.Scheme}://{context.Request.Host.Value}";

            var menuList = await _demoService.GetMenuAsync();

            var urlSet = new Urlset();

            var urls = new List<Url>();

            var hash = new HashSet<string>();

            foreach (var menu in menuList)
            {
                AppendMenuUrl(menu, host, urls, hash);
            }

            urlSet.Urls = urls;

            var xmlSerializer = new XmlSerializer(typeof(Urlset));

            context.Response.Headers.Add("Content-Type", "application/xml");
            using (StringWriter textWriter = new ())
            {
                xmlSerializer.Serialize(textWriter, urlSet);
                var xmlText = textWriter.ToString();
                await context.Response.WriteAsync(xmlText);
            }
        }

        private static void AppendMenuUrl(DemoMenuItemModel menu, string host, List<Url> urls, HashSet<string> hash)
        {
            if (!hash.Contains(menu.Url))
            {
                urls.Add(new Url()
                {
                    loc = host + "/" + menu.Url
                });

                hash.Add(menu.Url);
            }

            if (menu.Children != null)
            {
                foreach (var child in menu.Children)
                {
                    AppendMenuUrl(child, host, urls, hash);
                }
            }
        }
    }
}
