using Masa.Blazor.Doc.Models;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Masa.Blazor.Doc.Services
{
    public class CrawlService
    {
        private readonly DemoService _demoService;
        public CrawlService(DemoService demoService)
        {
            _demoService = demoService;
        }

        public async Task GetRobotsTxt(HttpContext context)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var txtPath = Path.Combine(assemblyPath, "wwwroot", "robots.txt");

            if (File.Exists(txtPath))
            {
                var robotsTxt = File.ReadAllText(txtPath);
                await context.Response.WriteAsync(robotsTxt);
            }

            await context.Response.WriteAsync("");
        }

        public async Task GetSitemap(HttpContext context)
        {
            var host = $"{context.Request.Scheme}://{context.Request.Host.Value}";

            var menuList = await _demoService.GetMenuAsync();

            StringBuilder sb = new StringBuilder(5000);

            var hash = new HashSet<string>();

            foreach (var menu in menuList)
            {
                AppendMenuUrl(menu, sb, host, hash);
            }

            await context.Response.WriteAsync(sb.ToString());
        }

        private static void AppendMenuUrl(DemoMenuItemModel menu, StringBuilder sb, string host, HashSet<string> hash)
        {
            if (!hash.Contains(menu.Url))
            {
                sb.AppendLine(host + "/" + menu.Url);

                hash.Add(menu.Url);
            }

            if (menu.Children != null)
            {
                foreach (var child in menu.Children)
                {
                    AppendMenuUrl(child, sb, host, hash);
                }
            }

            return;
        }
    }
}
