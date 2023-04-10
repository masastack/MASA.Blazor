using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Masa.Docs.Indexing.Data;
using Microsoft.Extensions.Logging;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.Converters;
using YamlDotNet.Serialization.NamingConventions;

namespace Masa.Docs.Indexing.Parsers;

public class MasaDocParser : IMasaDocParser
{
    private readonly ILogger _logger;

    private readonly List<Record> _emptyResult = new(0);

    private readonly List<Record> _parseResult = new();

    private int _firstHeaderLevel = 1;

    private MarkdownDocument? _document;

    public MasaDocParser(ILogger<MasaDocParser> logger)
    {
        _logger = logger;
    }

    public IEnumerable<Record> ParseDocument(string docContent, string docUrl, string lang, string project)
    {
        _parseResult.Clear();
        var pipeline = new MarkdownPipelineBuilder().UseYamlFrontMatter().UseAdvancedExtensions().Build();
        _document = Markdown.Parse(docContent, pipeline);
        var headers = _document!.Descendants<HeadingBlock>();
        _firstHeaderLevel = !headers.Any() ? 1 : _document!.Descendants<HeadingBlock>().Min(x => x.Level);
        string? mainBody = null;
        Record? lastHeaderRecord = null;
        var position = 0;
        foreach (var block in _document)
        {
            switch (block)
            {
                case YamlFrontMatterBlock yamlBlock:
                    var meta = GetYamlDescription(docContent, yamlBlock);
                    if (string.IsNullOrWhiteSpace(meta?.Title))
                    {
                        _logger.LogError("document : {docUrl} do not contain a title,please check it", docUrl);
                        return _emptyResult;
                    }

                    var content = meta.Content;
                    _firstHeaderLevel = 1;
                    AddRecords(meta.Title, ref content, lang, docUrl, project, 1, ref lastHeaderRecord, ref position);
                    break;
                case HeadingBlock headingBlock:
                    var headerTitle = GetContentFromInline(headingBlock.Inline);
                    //before add header ,check main body has value,then add it to a content record
                    if (!string.IsNullOrWhiteSpace(headerTitle))
                    {
                        AddRecords(headerTitle, ref mainBody, lang, docUrl, project, headingBlock.Level,
                           ref lastHeaderRecord,
                            ref position);
                    }
                    else
                    {
                        _logger.LogError("document : {docUrl} {level} header can not obtain a title, please check it!",
                            docUrl, headingBlock.Level);
                        return _emptyResult;
                    }

                    break;
                case ParagraphBlock paragraphBlock:
                    mainBody += GetContentFromInline(paragraphBlock.Inline);
                    break;
            }
        }

        return _parseResult;
    }

    private void AddRecords(string title, ref string? mainBody, string lang, string docUrl, string project, int headerLevel,
     ref Record? lastHeaderRecord, ref int position)
    {
        Record AddHeaderRecord(string content, int level, string lan, string url, string projectName, int recordPosition, in Record? lastRecord)
        {
            content.AssertNotNullOrEmpty(nameof(content));
            Record record = new Record(lan, url, projectName, position: recordPosition, pageRank: 0);
            if (lastRecord is not null)
            {
                record.ClonePrerecord(lastRecord);
            }
            RecordType recordType;
            level = level - _firstHeaderLevel + 1;
            switch (level)
            {
                case 1:
                    recordType = RecordType.Lvl1;
                    level = 90;
                    record.Hierarchy.Lvl1 = content;
                    break;
                case 2:
                    recordType = RecordType.Lvl2;
                    record.Hierarchy.Lvl2 = content;
                    level = 80;
                    break;
                case 3:
                    recordType = RecordType.Lvl3;
                    record.Hierarchy.Lvl3 = content;
                    level = 70;
                    break;
                case 4:
                    recordType = RecordType.Lvl4;
                    record.Hierarchy.Lvl4 = content;
                    level = 60;
                    break;
                case 5:
                    recordType = RecordType.Lvl5;
                    record.Hierarchy.Lvl5 = content;
                    level = 50;
                    break;
                case 6:
                    recordType = RecordType.Lvl6;
                    record.Hierarchy.Lvl6 = content;
                    level = 40;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level),
                        "now can not support more than six level header");
            }

            record!.Weight!.Level = level;
            record.SetType(recordType);
            record.Anchor = content.ToHashAnchor();
            record.Url = docUrl + "#" + record.Anchor;
            record.UrlWithoutVariables = record.Url;
            return record;
        }

        Record AddMainBodyRecord(ref string? content, int recordPosition, in Record lastRecord)
        {
            content.AssertNotNullOrEmpty(nameof(content));
            lastRecord.AssertNotNullOrEmpty(nameof(lastRecord));

            var record = new Record(lastRecord.Lang, lastRecord.Url, lastRecord.Hierarchy.Lvl0,
                RecordType.Content, position: recordPosition, level: 0,
                pageRank: 0);
            record.ClonePrerecord(lastRecord);
            record.Content = content;
            record.ContentCamel = content;
            content = null;
            return record;
        }

        var lastRecord = lastHeaderRecord ?? AddHeaderRecord(title, headerLevel, lang, docUrl, project, position++, lastHeaderRecord);

        if (!string.IsNullOrEmpty(mainBody))
        {
            _parseResult.Add(AddMainBodyRecord(ref mainBody, position++, lastRecord!));
        }

        if (lastHeaderRecord is not null)
        {
            lastRecord = AddHeaderRecord(title, headerLevel, lang, docUrl, project, position++, lastHeaderRecord);
        }

        lastHeaderRecord = lastRecord;
        _parseResult.Add(lastRecord!);
    }

    private string? GetContentFromInline(ContainerInline? container)
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
                LinkInline linkInline => () =>
                {
                    var content = !string.IsNullOrWhiteSpace(linkInline.Title) ? linkInline.Title : linkInline.Label;
                    if (!string.IsNullOrEmpty(content)) return content;
                    var first = linkInline.FirstChild;
                    content = first?.ToString();
                    return content;
                }
                ,
                null => () => null,
                _ => () => inline.ToString()
            };
            var inlineText = func();
            _logger.LogDebug("======== {type} {newline} {inlineText} {newline}", inline?.GetType(), Environment.NewLine, inlineText, Environment.NewLine);
            result += inlineText;
        }

        return result;
    }

    private DescriptionYaml? GetYamlDescription(in string docContent, in YamlFrontMatterBlock yamlBlock)
    {
        try
        {
            var yamlText = docContent.Substring(yamlBlock.Span.Start, yamlBlock.Span.Length).Trim('-');
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .WithTypeConverter(new DateTimeConverter(
                    formats: new[] { "yyyy/MM/dd hh:mm:ss", "yyyy/MM/dd hh:mm", "yyyy/MM/dd", "yyyy-MM-dd hh:mm", "yyyy-MM-dd hh:mm:ss", "yyyy-MM-dd" })
                )
                .BuildValueDeserializer();
            var meta = Deserializer.FromValueDeserializer(deserializer)
                .Deserialize<DescriptionYaml>(yamlText);
            return meta;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetYamlDescription error");
        }
        return null;
    }
}