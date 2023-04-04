using Masa.Docs.Indexing.Data;

namespace Masa.Docs.Indexing.Parsers;

public interface IMasaDocParser
{
    IEnumerable<Record> ParseDocument(string content, string docUrl, string language, string project);
}