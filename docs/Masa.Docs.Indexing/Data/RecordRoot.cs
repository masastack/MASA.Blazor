using Newtonsoft.Json;

namespace Masa.Docs.Indexing.Data
{
    public class RecordRoot
    {
        [JsonProperty("indexName")]
        public string IndexName { get; private set; }

        [JsonProperty("records")]
        public List<Record> Records { get; } = new();

        public RecordRoot(string indexName)
        {
            IndexName = indexName;
        }
    }
}
