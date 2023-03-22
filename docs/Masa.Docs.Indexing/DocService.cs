using AngleSharp.Css.Values;
using Masa.Docs.Indexing.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Masa.Docs.Indexing
{
    public class DocService : IDisposable
    {
        public const string ALGOLIA_APIKEY = "ALGOLIA_APIKEY";

        private readonly HttpClient _httpClient;
        private readonly AlgoliaOptions _algoliaOption;
        private readonly ILogger<DocService> _logger = null!;
        private readonly string? _algoliaApikey;

        public DocService(HttpClient httpClient, ILogger<DocService> logger, IOptions<AlgoliaOptions> algoliaOption)
        {
            _httpClient = httpClient;
            _logger = logger;
            _algoliaOption = algoliaOption.Value;
            _algoliaApikey = Environment.GetEnvironmentVariable(ALGOLIA_APIKEY, EnvironmentVariableTarget.Process);
            _algoliaApikey.AssertParamNotNull(ALGOLIA_APIKEY);
            _algoliaOption.ApplicationId.AssertParamNotNull(nameof(_algoliaOption.ApplicationId));
            _algoliaOption.Projects.AssertParamNotNull(nameof(_algoliaOption.Projects));
            AssertDirectoryExist(_algoliaOption.RootDocsPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual Dictionary<string, IEnumerable<string>> GetProjectUrlsFromLocalFile()
        {
            Dictionary<string, IEnumerable<string>> result = new();
            Uri baseUri = new Uri(_algoliaOption.DocDomain);
            foreach (var projectItem in _algoliaOption.Projects!)
            {
                var project = projectItem.Key;
                var urls = new HashSet<string>();
                var projectPhysicalPath = Path.Combine(_algoliaOption.RootDocsPath, projectItem.Value);
                var totalFiles = Directory.GetFiles(projectPhysicalPath, "??-??.md", SearchOption.AllDirectories);
                foreach (var fileName in totalFiles)
                {
                    if (fileName.EndsWith("zh-cn.md", StringComparison.OrdinalIgnoreCase)
                        || fileName.EndsWith("en-us.md", StringComparison.OrdinalIgnoreCase))
                    {
                        var dir = Path.GetDirectoryName(fileName);
                        if (dir == null) continue;
                        var url = dir.Replace(projectPhysicalPath, "").Replace(Path.DirectorySeparatorChar, '/');
                        var uri = new Uri(baseUri, project + url);
                        urls.Add(uri.ToString());
                    }
                }
                result.Add(project, urls);
            }
            return result;
        }

        public virtual Task CrawlDocsSite(string url, string indexName)
        {
            throw new NotImplementedException();
        }

        public virtual async Task CrawlDocsSites()
        {
            var result = GetProjectUrlsFromLocalFile();
            if (result == null) return;
            foreach (var projectItem in result)
            {
                await CrawlDocsSites(projectItem.Value, projectItem.Key);
                break;
            }
        }
        public virtual async Task CrawlDocsSites(IEnumerable<string> urls, string indexName)
        {
            foreach (var url in urls)
            {
                await CrawlDocsSite(url, indexName);
                break;
            }
        }
        public void AssertDirectoryExist(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"can not found directory `{path}`,please check config");
            }
        }



        public async Task WaitForStartWebSite()
        {
            try
            {
                // Make HTTP GET request
                // Parse JSON response deserialize into Todo type
                //wait 1 minutes
                await Task.Delay(60 * 1000);
                var httpResponse = await _httpClient.GetAsync(_algoliaOption.DocDomain);
                httpResponse.EnsureSuccessStatusCode();
                _logger.LogInformation("local doc site started.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting something fun to say: {Error}", ex);
            }
        }


        public void Dispose() => _httpClient?.Dispose();
    }
}
