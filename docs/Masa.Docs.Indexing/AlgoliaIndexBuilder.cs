using Algolia.Search.Clients;
using Algolia.Search.Models.Settings;
using AngleSharp.Html.Parser;
using Markdig;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Masa.Docs.Indexing.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masa.Docs.Indexing
{
    /// <summary>
    /// 读取本地md文件来生成索引
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class AlgoliaIndexBuilder : IIndexBuilder<RecordRoot>
    {
        private readonly ILogger _logger;
        private readonly SearchClient _searchClient;
        public const string ALGOLIA_APIKEY = "MASA_ALGOLIA_APIKEY";
        public const string ALGOLIA_APPID = "MASA_ALGOLIA_APPID";
        public const string ALGOLIA_INDEXPREFIX = "MASA_ALGOLIA_INDEXPREFIX";
        public const string ROOT_DOCS_PATH = "MASA_ROOT_DOCS_PATH";
        private readonly AlgoliaOptions _algoliaOption;
        public AlgoliaIndexBuilder(
             ILogger<AlgoliaIndexBuilder> logger, IOptions<AlgoliaOptions> algoliaOption)
        {
            _algoliaOption = GetRealAlgoliaOptions(algoliaOption.Value);
            _searchClient = new SearchClient(_algoliaOption.ApplicationId, _algoliaOption.ALGOLIA_APIKEY);

            _logger = logger;
        }
        private AlgoliaOptions GetRealAlgoliaOptions(AlgoliaOptions algoliaOptions)
        {
            var algolia_apikey = Environment.GetEnvironmentVariable(ALGOLIA_APIKEY, EnvironmentVariableTarget.Process);
            algoliaOptions.ALGOLIA_APIKEY = algolia_apikey;

            var algolia_appid = Environment.GetEnvironmentVariable(ALGOLIA_APPID, EnvironmentVariableTarget.Process);
            if (algolia_appid is not null) algoliaOptions.ApplicationId = algolia_appid;

            var algolia_indexprefix = Environment.GetEnvironmentVariable(ALGOLIA_INDEXPREFIX, EnvironmentVariableTarget.Process);
            if (algolia_indexprefix is not null) algoliaOptions.IndexPrefix = algolia_indexprefix;

            var root_docs_path = Environment.GetEnvironmentVariable(ROOT_DOCS_PATH, EnvironmentVariableTarget.Process);
            if (root_docs_path is not null) algoliaOptions.RootDocsPath = root_docs_path;

            algoliaOptions.ALGOLIA_APIKEY.AssertParamNotNull(ALGOLIA_APIKEY);
            algoliaOptions.ApplicationId.AssertParamNotNull(nameof(algoliaOptions.ApplicationId));
            algoliaOptions.Projects.AssertParamNotNull(nameof(algoliaOptions.Projects));
            AssertDirectoryExist(algoliaOptions.RootDocsPath);
            return algoliaOptions;
        }
        private void AssertDirectoryExist(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"can not found directory `{path}`,please check config");
            }
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
                Ranking = new() { "words", "filters", "typo", "attribute", "proximity", "exact", "custom", },
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
                AttributesToHighlight = new() { "hierarchy", "content" },
                AttributesToSnippet = new() { "content:10", },
                CamelCaseAttributes = new() { "hierarchy", "hierarchy_radio", "content" },
                Distinct = true,
                AttributeForDistinct = "url",
                AllowTyposOnNumericTokens = false,
                MinProximity = 1,
                IgnorePlurals = true,
                AdvancedSyntax = true,
                MinWordSizefor1Typo = 3,
                MinWordSizefor2Typos = 7,
                RemoveWordsIfNoResults = "allOptional",
                AttributesToRetrieve = new()
                {
                    "hierarchy",
                    "content",
                    "anchor",
                    "url",
                    "url_without_anchor",
                    "type",
                },
                AttributesForFaceting = new() { $"filterOnly({nameof(Record.Lang).ToLower()})", "type" },
            };
        }

        public async Task<bool> CreateIndexAsync(IEnumerable<RecordRoot>? datas = null)
        {
            datas = datas ?? GenerateRecords();

            if (datas != null)
            {
                _logger.LogInformation($"to create index with {datas.Count()} pages");
                var indexNames = datas.Select(x => x.IndexName).Distinct().ToList();
                foreach (var indexName in indexNames)
                {
                    var index = _searchClient.InitIndex(indexName);
                    await index.SetSettingsAsync(GetIndexSetting());
                    var allRecords = datas.Where(x => x.IndexName == indexName).SelectMany(x => x.Records);
                    await index.SaveObjectsAsync(allRecords);
                }
                _logger.LogInformation($"created index complete");
                return true;
            }
            return false;
        }


        private IEnumerable<Record> PraseDocument(string content, string docUrl, string lang, string project)
        {
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var md = Markdown.Parse(content, pipeline);
            return GetRecords(md, lang, docUrl, project);
        }

        private IEnumerable<Record> GetRecords(MarkdownDocument document, string lang, string docUrl, string project)
        {
            void AddContentToRecord(string lang, string docUrl, string project, int position, in List<Record> result, in Record lastRecord, ref string? content)
            {
                if (content != null && lastRecord != null)
                {
                    var record = new Record(lang, docUrl, project, RecordType.Content, position: position, level: 0, pageRank: 0);
                    record.ClonePrerecord(lastRecord);
                    record.Content = content;
                    record.ContentCamel = content;
                    content = null;
                    result.Add(record);
                }
            }

            Record? AddHeaderRecord(string lang, string docUrl, string project, int position, in HeadingBlock block, uint firstHeadLevel, in Record? lastRecord = null)
            {
                RecordType recordType = RecordType.Lvl1;
                int level = 90;
                switch (block.Level - firstHeadLevel + 1)
                {
                    case 1:
                        recordType = RecordType.Lvl1;
                        level = 90;
                        break;
                    case 2:
                        recordType = RecordType.Lvl2;
                        level = 80;
                        break;
                    case 3:
                        recordType = RecordType.Lvl3;
                        level = 70;
                        break;
                    case 4:
                        recordType = RecordType.Lvl4;
                        level = 60;
                        break;
                    case 5:
                        recordType = RecordType.Lvl5;
                        level = 50;
                        break;
                    case 6:
                        recordType = RecordType.Lvl6;
                        level = 40;
                        break;
                    default:
                        return null;
                }

                Record record = new Record(lang, docUrl, project, recordType, position: position, level: level, pageRank: 0);
                if (lastRecord is not null)
                {
                    record.ClonePrerecord(lastRecord);
                }
                var content = block.Inline?.FirstChild?.ToString();
                if (content != null)
                {
                    switch (recordType)
                    {
                        case RecordType.Lvl1:
                            record.Hierarchy.Lvl1 = content;
                            break;
                        case RecordType.Lvl2:
                            record.Hierarchy.Lvl2 = content;
                            break;
                        case RecordType.Lvl3:
                            record.Hierarchy.Lvl3 = content;
                            break;
                        case RecordType.Lvl4:
                            record.Hierarchy.Lvl4 = content;
                            break;
                        case RecordType.Lvl5:
                            record.Hierarchy.Lvl5 = content;
                            break;
                        case RecordType.Lvl6:
                            record.Hierarchy.Lvl6 = content;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    record.Anchor = content.HashToAnchorString();
                    record.Url = docUrl + "#" + record.Anchor;
                    record.UrlWithoutVariables = record.Url;
                }
                return record;
            }

            int position = 0;
            var result = new List<Record>();
            Record? record = null;
            string? content = null;
            (uint firstHeadLevel, bool setedValue) = (1, false);
            foreach (var node in document.AsEnumerable())
            {
                if (node is HeadingBlock block)
                {
                    if (!setedValue)
                    {
                        firstHeadLevel = (uint)block.Level;
                        setedValue = true;
                    }
                    var lastRecord = record;
                    //add content to record when meet a new head and will not excute on the first step
                    if (lastRecord is not null && content is not null)
                    {
                        AddContentToRecord(lang, docUrl, project, position, result, lastRecord, ref content);
                        position += 1;
                    }
                    record = AddHeaderRecord(lang, docUrl, project, position, block, firstHeadLevel, lastRecord);
                    if (record is not null)
                    {
                        result.Add(record!);
                        position += 1;
                    }
                }
                if (node is ParagraphBlock paragraph)
                {
                    content += paragraph.Inline?.FirstChild?.ToString();
                }
            }
            var lastPostion = result.LastOrDefault()?.Weight?.Position;
            if (lastPostion is not null)
            {
                position = lastPostion.Value + 1;
            }
            else
            {
                position += 1;
            }
            AddContentToRecord(lang, docUrl, project, position, result, record!, ref content);
            if (record is not null) result.Add(record!);
            return result;
        }

        public IEnumerable<RecordRoot> GenerateRecords()
        {
            var result = new List<RecordRoot>();
            Uri baseUri = new Uri(_algoliaOption.DocDomain);
            foreach (var projectItem in _algoliaOption.Projects!)
            {
                var project = projectItem.Key;
                var urls = new HashSet<string>();
                var projectPhysicalPath = Path.Combine(_algoliaOption.RootDocsPath, projectItem.Value);
                var totalFiles = Directory.GetFiles(projectPhysicalPath, "??-??.md", SearchOption.AllDirectories);
                foreach (var fileName in totalFiles)
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
                    if (language != null)
                    {
                        var dir = Path.GetDirectoryName(fileName);
                        if (dir == null) continue;
                        var url = dir.Replace(projectPhysicalPath, "").Replace(Path.DirectorySeparatorChar, '/');
                        var lowerProjcet = project.ToLower();
                        var uri = new Uri(baseUri, lowerProjcet + url);
                        var docUrl = uri.ToString();
                        var content = File.ReadAllText(fileName, Encoding.UTF8);
                        var indexName = $"{_algoliaOption.IndexPrefix}{lowerProjcet}";
                        RecordRoot recordRoot = new(indexName);
                        var records = PraseDocument(content, docUrl, language, project);
                        if (records.Any())
                        {
                            recordRoot.Records.AddRange(records);
                            result.Add(recordRoot);
                        }
                    }
                }
            }
            return result;
        }
    }
}
