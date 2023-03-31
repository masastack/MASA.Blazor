using System.Globalization;
using Algolia.Search.Clients;
using Algolia.Search.Models.Settings;
using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Masa.Docs.Indexing.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.Converters;
using YamlDotNet.Serialization.NamingConventions;

namespace Masa.Docs.Indexing
{
    /// <summary>
    /// builder algolia index from local markdown files
    /// </summary>
    public class AlgoliaIndexBuilder : IIndexBuilder<RecordRoot>
    {
        private readonly ILogger _logger;

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
             ILogger<AlgoliaIndexBuilder> logger, IOptions<AlgoliaOptions> algoliaOption)
        {
            _logger = logger;
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

        public async Task<bool> CreateIndexAsync(IEnumerable<RecordRoot> records, CancellationToken ct = default)
        {
            try
            {
                _logger.LogInformation($"Create index with {records.Count()} pages.");
                var indexNames = records.Select(x => x.IndexName).Distinct();
                foreach (var indexName in indexNames)
                {
                    var index = _searchClient.InitIndex(indexName);
                    await index.SetSettingsAsync(GetIndexSetting(), ct: ct);
                    var allRecords = records.Where(x => x.IndexName == indexName).SelectMany(x => x.Records);
                    await index.ClearObjectsAsync(ct: ct);
                    await index.SaveObjectsAsync(allRecords, ct: ct);
                }
                _logger.LogInformation($"Index creation complete.");
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "CreateIndexAsync got an error: {0}!", e.Message);
                return false;
            }
        }

        private IEnumerable<Record> ParseDocument(string docContent, string docUrl, string lang, string project)
        {
            var pipeline = new MarkdownPipelineBuilder().UseYamlFrontMatter().UseAdvancedExtensions().Build();
            var document = Markdown.Parse(docContent, pipeline);

            void AddContentToRecord(string lan, string url, string projectName, int position, in List<Record> result, in Record lastRecord, ref string? content)
            {
                if (string.IsNullOrEmpty(content))
                {
                    content = null;
                    return;
                }
                var record = new Record(lan, url, projectName, RecordType.Content, position: position, level: 0, pageRank: 0);
                record.ClonePrerecord(lastRecord);
                record.Content = content;
                record.ContentCamel = content;
                content = null;
                result.Add(record);
            }

            string? GetContentFromInline(ContainerInline? container)
            {
                if (container == null) return null;
                foreach (var htmlInline in container.FindDescendants<HtmlInline>())
                {
                    htmlInline.Remove();
                }
                foreach (var inline in container.FindDescendants<LineBreakInline>())
                {
                    inline.Remove();
                }

                if (container.FirstChild is null) return null;
                string? result = null;
                foreach (var inline in container)
                {
                    Func<string?> func = inline switch
                    {
                        CodeInline code => () => code.DelimiterCount == 1 ? code.Content : null,//only get the ` code inline
                        EmphasisInline emphasisInline => () => emphasisInline.FirstChild?.ToString(),
                        LiteralInline literalInline => () => literalInline.Content.ToString(),
                        LinkInline linkInline => () => string.IsNullOrWhiteSpace(linkInline.Title) ? linkInline.Url : linkInline.Title,
                        null => () => null,
                        _ => () => inline.ToString()
                    };
                    var inlineText = func();
                    _logger.LogDebug(
                        $"======== {inline?.GetType()} {Environment.NewLine} {inlineText} {Environment.NewLine}");
                    result += inlineText;
                }

                return result;
            }

            Record? AddHeaderRecord(string lan, string url, string projectName, int position, in HeadingBlock block, uint firstLevel, in Record? lastRecord = null)
            {
                RecordType recordType;
                int level;
                switch (block.Level - firstLevel + 1)
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
                Record record = new Record(lan, url, projectName, recordType, position: position, level: level, pageRank: 0);
                if (lastRecord is not null)
                {
                    record.ClonePrerecord(lastRecord);
                }
                var content = GetContentFromInline(block.Inline);
                if (!string.IsNullOrWhiteSpace(content))
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

            Record AddYamlHeaderRecord(string lan, string projectName, int position, string content)
            {
                var recordType = RecordType.Lvl1;
                var level = 90;
                Record record = new Record(lan, docUrl, projectName, recordType, position: position, level: level, pageRank: 0);
                record.Hierarchy.Lvl1 = content;
                record.Anchor = content.HashToAnchorString();
                record.Url = docUrl + "#" + record.Anchor;
                record.UrlWithoutVariables = record.Url;
                return record;
            }

            int position = 0;
            var result = new List<Record>();
            Record? record = null;
            string? content = null;
            (uint firstHeadLevel, bool isSet) = (1, false);
            foreach (var node in document.AsEnumerable())
            {
                Record? lastRecord;
                switch (node)
                {
                    case YamlFrontMatterBlock yamlBlock:
                        var yamlText = docContent.Substring(yamlBlock.Span.Start, yamlBlock.Span.Length).Trim('-');
                        var deserializer = new DeserializerBuilder()
                            .WithNamingConvention(CamelCaseNamingConvention.Instance)
                            .WithTypeConverter(new DateTimeConverter(
                                formats: new[] { "yyyy/MM/dd hh:mm:ss", "yyyy/MM/dd hh:mm", "yyyy/MM/dd" })
                            )
                            .BuildValueDeserializer();
                        var meta = Deserializer.FromValueDeserializer(deserializer)
                            .Deserialize<DescriptionYaml>(yamlText);
                        if (!isSet)
                        {
                            firstHeadLevel = 1;
                            isSet = true;
                        }
                        if (string.IsNullOrWhiteSpace(meta.Title))
                        {
                            throw new Exception($"document : {docUrl} do not contain a title,please check it");
                        }
                        record = AddYamlHeaderRecord(lang, project, position, meta.Title!);
                        result.Add(record);
                        lastRecord = record;
                        position += 1;
                        if (!string.IsNullOrEmpty(meta.Desc))
                        {
                            var descContent = meta.Desc;
                            AddContentToRecord(lang, docUrl, project, position, result, lastRecord, ref descContent);
                            position += 1;
                        }
                        break;
                    case HeadingBlock block:
                        if (!isSet)
                        {
                            firstHeadLevel = (uint)block.Level;
                            isSet = true;
                        }
                        lastRecord = record;
                        //add content to record when meet a new head and will not do it at the first step
                        if (lastRecord is not null && !string.IsNullOrEmpty(content))
                        {
                            AddContentToRecord(lang, docUrl, project, position, result, lastRecord, ref content);
                            position += 1;
                        }
                        record = AddHeaderRecord(lang, docUrl, project, position, block, firstHeadLevel, lastRecord);
                        if (record is not null)
                        {
                            result.Add(record);
                            position += 1;
                        }
                        break;
                    case ParagraphBlock paragraph:
                        content += GetContentFromInline(paragraph.Inline);
                        break;
                }

                _logger.LogDebug(string.Format("nodeType:{0}\t{1}\t{2}\n{3}\n", node.GetType().Name, docUrl,
                    position, content));
            }
            var lastPosition = result.LastOrDefault()?.Weight.Position;
            if (lastPosition is not null)
            {
                position = lastPosition.Value + 1;
            }
            else
            {
                position += 1;
            }
            if (!string.IsNullOrEmpty(content) && record is not null)
            {
                AddContentToRecord(lang, docUrl, project, position, result, record!, ref content);
                result.Add(record);
            }
            return result;
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
            Uri baseUri = new Uri(_algoliaOption.DocDomain);
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
                    var uri = new Uri(baseUri, projectUrl);
                    var docUrl = uri.ToString();
                    var content = File.ReadAllText(fileName, Encoding.UTF8);
                    var indexName = $"{_algoliaOption.IndexPrefix}{language}_{lowerProject}";
                    RecordRoot recordRoot = new(indexName);
                    var records = ParseDocument(content, docUrl, language, project);
                    recordRoot.Records.AddRange(records);
                    result.Add(recordRoot);
                }
            }
            return result;
        }
    }
}
