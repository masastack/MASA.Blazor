using Algolia.Search.Clients;
using Algolia.Search.Models.Settings;
using Masa.Docs.Indexing.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using Masa.Docs.Indexing.Parsers;

namespace Masa.Docs.Indexing
{
    /// <summary>
    /// builder algolia index from local markdown files
    /// </summary>
    public class AlgoliaIndexBuilder : IIndexBuilder<RecordRoot>
    {
        private readonly ILogger _logger;

        private readonly IMasaDocParser _docParser;

        private readonly SearchClient _searchClient;

        private readonly AlgoliaOptions _algoliaOption;

        private readonly List<string> _totalFiles = new();

        public List<string> TotalFiles
        {
            get
            {
                if (!_totalFiles.Any())
                    SetAllFile();
                return _totalFiles;
            }
        }

        public AlgoliaIndexBuilder(
             ILogger<AlgoliaIndexBuilder> logger, IOptions<AlgoliaOptions> algoliaOption, IMasaDocParser docParser)
        {
            _logger = logger;
            _docParser = docParser;
            _algoliaOption = algoliaOption.Value;
            if (!_totalFiles.Any())
                SetAllFile();
            _searchClient = new SearchClient(_algoliaOption.ApplicationId, _algoliaOption.AlgoliaApiKey);
        }

        private IndexSettings GetIndexSetting()
        {
            return new IndexSettings()
            {
                CustomRanking = new List<string>
                {
                    "desc(weight.pageRank)",
                    "desc(weight.level)",
                    "asc(weight.position)",
                },
                Ranking = new List<string> { "words", "filters", "typo", "attribute", "proximity", "exact", "custom", },
                HighlightPreTag = @"<span class=""algolia-docsearch-suggestion--highlight"">",
                HighlightPostTag = "</span>",
                SearchableAttributes = new List<string>() {
                    "unordered(hierarchy.lvl1)",
                    "unordered(hierarchy.lvl2)",
                    "unordered(hierarchy.lvl3)",
                    "unordered(hierarchy.lvl4)",
                    "unordered(hierarchy.lvl5)",
                    "unordered(hierarchy.lvl6)",
                    "content",
                },
                AttributesToHighlight = new List<string> { "hierarchy", "content" },
                AttributesToSnippet = new List<string> { "content:10", },
                CamelCaseAttributes = new List<string> { "hierarchy", "hierarchy_radio", "content" },
                Distinct = true,
                AttributeForDistinct = "url",
                AllowTyposOnNumericTokens = false,
                MinProximity = 1,
                IgnorePlurals = true,
                AdvancedSyntax = true,
                MinWordSizefor1Typo = 3,
                MinWordSizefor2Typos = 7,
                RemoveWordsIfNoResults = "allOptional",
                AttributesToRetrieve = new List<string>
                {
                    "hierarchy",
                    "content",
                    "anchor",
                    "url",
                    "url_without_anchor",
                    "type",
                },
                AttributesForFaceting = new List<string> { $"filterOnly({nameof(Record.Lang).ToLower()})", "type" },
            };
        }

        public async Task CreateIndexAsync(IEnumerable<RecordRoot> records, CancellationToken ct = default)
        {
            try
            {
                var indexNames = records.Select(x => x.IndexName).Distinct();
                foreach (var indexName in indexNames)
                {
                    var index = _searchClient.InitIndex(indexName);
                    await index.SetSettingsAsync(GetIndexSetting(), ct: ct);
                    var allRecords = records.Where(x => x.IndexName == indexName).SelectMany(x => x.Records);
                    _logger.LogInformation($"clear old objects in  {indexName}.");
                    await index.ClearObjectsAsync(ct: ct);
                    _logger.LogInformation($"create {allRecords.Count()} objects in  {indexName}.");
                    await index.SaveObjectsAsync(allRecords, ct: ct);
                }
                _logger.LogInformation("Index creation complete.");
            }
            catch (Exception e)
            {
                _logger.LogError("{e.Message}", e.Message);
                throw;
            }
        }

        private void SetAllFile()
        {
            _totalFiles.Clear();
            foreach (var projectItem in _algoliaOption.Projects!)
            {
                var projectPhysicalPath = Path.Combine(_algoliaOption.RootDocsPath, projectItem.Value);
                _totalFiles.AddRange(Directory.GetFiles(projectPhysicalPath, "??-??.md", SearchOption.AllDirectories));
            }
        }

        public IEnumerable<RecordRoot> GenerateRecords()
        {
            var result = new List<RecordRoot>();
            string baseUri = _algoliaOption.DocDomain;

            foreach (var projectItem in _algoliaOption.Projects!)
            {
                var project = projectItem.Key;
                var projectPhysicalPath = Path.Combine(_algoliaOption.RootDocsPath, projectItem.Value);
                var projectFiles = TotalFiles.Where(x => x.StartsWith(projectPhysicalPath));
                foreach (var fileName in projectFiles)
                {
                    string? language = null;
                    var lowerFileName = fileName.ToLower();
                    if (lowerFileName.EndsWith("zh-cn.md"))
                    {
                        language = "zh";
                    }
                    else if (lowerFileName.EndsWith("en-us.md"))
                    {
                        language = "en";
                    }

                    if (language == null) continue;
                    var dir = Path.GetDirectoryName(fileName);
                    if (dir == null) continue;
                    var url = dir.Replace(projectPhysicalPath, "").Replace(Path.DirectorySeparatorChar, '/');
                    var lowerProject = project.ToLower();
                    var projectUrl = lowerProject + url;
                    if (_algoliaOption.ExcludedUrls?.Any(u => u.EndsWith(projectUrl)) is true)
                    {
                        continue;
                    }

                    var docUrl = baseUri + projectUrl.TrimStart('/');
                    var content = File.ReadAllText(fileName, Encoding.UTF8);
                    var indexName = $"{_algoliaOption.IndexPrefix}{language}_{lowerProject}";
                    var records = _docParser.ParseDocument(content, docUrl, language, project);
                    if (records.Any())
                    {
                        RecordRoot recordRoot = new(indexName);
                        recordRoot.Records.AddRange(records);
                        result.Add(recordRoot);
                    }
                }
            }
            return result;
        }
    }
}
