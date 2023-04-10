using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Masa.Docs.Indexing.Configurations
{
    public class AlgoliaFromEnvironmentConfiguration : IConfigureOptions<AlgoliaOptions>
    {
        public const string ALGOLIA_API_KEY = "MASA_ALGOLIA_API_KEY";

        public const string ALGOLIA_APP_ID = "MASA_ALGOLIA_APP_ID";

        public const string ALGOLIA_INDEX_PREFIX = "MASA_ALGOLIA_INDEX_PREFIX";

        public const string ROOT_DOCS_PATH = "MASA_ROOT_DOCS_PATH";

        public const string DOC_DOMAIN = "MASA_DOC_DOMAIN";

        public const string MASA_DOC_EXCLUDE_URLS = "MASA_DOC_EXCLUDE_URLS";

        private readonly ILogger<AlgoliaFromEnvironmentConfiguration> _logger;

        public AlgoliaFromEnvironmentConfiguration(ILogger<AlgoliaFromEnvironmentConfiguration> logger)
        {
            _logger = logger;
        }

        public void Configure(AlgoliaOptions options)
        {
            TrySetOptionFromEnvironmentVariables(options);

            void AssertDirectoryExist(string path)
            {
                if (!Directory.Exists(path))
                {
                    throw new DirectoryNotFoundException($"Can not found directory `{path}`, please check config.");
                }
            }

            options.Projects.AssertNotNullOrEmpty(nameof(options.Projects));
            foreach (var item in options.Projects!)
            {
                item.Value.AssertNotNullOrEmpty($"Project.{item.Key}");
            }
            options.AlgoliaApiKey.AssertNotNullOrEmpty(ALGOLIA_API_KEY);
            options.ApplicationId.AssertNotNullOrEmpty(nameof(options.ApplicationId));
            AssertDirectoryExist(options.RootDocsPath);
            if (!options.DocDomain.StartsWith("https://") && !options.DocDomain.StartsWith("http://") && !options.DocDomain.StartsWith("/"))
            {
                throw new ArgumentException("Unknown doc domain format,it can start with (http:// || https:// || /), please check it.");
            }
            if (!options.DocDomain.EndsWith("/"))
            {
                options.DocDomain += "/";
            }
        }

        public void TrySetOptionFromEnvironmentVariables(AlgoliaOptions algoliaOptions)
        {
            void SetPropertyFromEnvironment(string envName, string propertyOrFieldName, string? delimiter = null)
            {
                var value = Environment.GetEnvironmentVariable(envName, EnvironmentVariableTarget.Process);
                _logger.LogInformation("env: {0}, has value: {1}", envName, !string.IsNullOrEmpty(value));
                if (value is null) return;
                if (delimiter != null)
                {
                    var valueSet = value.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).AsEnumerable();
                    algoliaOptions.SetPropertyValue(propertyOrFieldName, valueSet);
                }
                else
                {
                    algoliaOptions.SetPropertyValue(propertyOrFieldName, value);
                }
            }

            SetPropertyFromEnvironment(ALGOLIA_APP_ID, nameof(AlgoliaOptions.ApplicationId));
            SetPropertyFromEnvironment(ALGOLIA_API_KEY, nameof(AlgoliaOptions.AlgoliaApiKey));
            SetPropertyFromEnvironment(ALGOLIA_INDEX_PREFIX, nameof(AlgoliaOptions.IndexPrefix));
            SetPropertyFromEnvironment(ROOT_DOCS_PATH, nameof(AlgoliaOptions.RootDocsPath));
            SetPropertyFromEnvironment(DOC_DOMAIN, nameof(AlgoliaOptions.DocDomain));
            SetPropertyFromEnvironment(MASA_DOC_EXCLUDE_URLS, nameof(AlgoliaOptions.ExcludedUrls), "||");
        }
    }
}
