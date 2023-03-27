using System.Text.Json.Serialization;

namespace Masa.Docs.Indexing.Data
{
    public class RecordRoot
    {
        [JsonPropertyName("indexName")]
        public string IndexName { get; private set; }

        [JsonPropertyName("records")]
        public List<Record> Records { get; } = new();

        public RecordRoot(string indexName)
        {
            IndexName = indexName;
        }
    }
}
