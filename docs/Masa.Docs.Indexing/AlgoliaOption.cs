namespace Masa.Docs.Indexing
{
    public class AlgoliaOptions
    {
        public const string Position = "Algolia";

        public string RootDocsPath { get; set; } = string.Empty;

        public int BatchSize { get; set; } = 2000;

        public string ApplicationId { get; set; } = string.Empty;

        public string IndexPrefix { get; set; } = "blazor-masastack_";

        public Dictionary<string, string>? Projects { get; set; } = null;

        public string DocDomain { get; set; } = string.Empty;

        public string? AlgoliaApiKey { get; set; } = null;

        public IEnumerable<string>? ExcludedUrls { get; set; } = null;
    }
}
