using System.Text.Json.Serialization;

namespace Masa.Docs.Indexing.Data
{
    public class RecordRoot
    {
        public RecordRoot(string indexName)
        {
            IndexName = indexName;
        }

        [JsonPropertyName("indexName")]
        public string IndexName { get; set; }

        [JsonPropertyName("records")]
        public List<Record> Records { get; } = new ();
    }
}
