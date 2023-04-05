using Masa.Docs.Indexing.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Masa.Docs.Indexing
{
    public sealed class IndexingHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IIndexBuilder<RecordRoot> _indexBuilder;

        public IndexingHostedService(
            ILogger<IndexingHostedService> logger,
            IHostApplicationLifetime appLifetime,
            IIndexBuilder<RecordRoot> indexBuilder)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _indexBuilder = indexBuilder;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("1. StartAsync has been called.");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("3. StopAsync has been called.");
            return Task.CompletedTask;
        }

        private async void OnStarted()
        {
            _logger.LogInformation("2. OnStarted has been called.");
            var records = _indexBuilder.GenerateRecords();
            await _indexBuilder.CreateIndexAsync(records);
            _appLifetime.StopApplication();
        }
    }
}
