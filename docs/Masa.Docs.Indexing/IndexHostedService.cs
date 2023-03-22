using AngleSharp.Dom;
using Masa.Docs.Indexing.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Masa.Docs.Indexing
{
    public sealed class IndexHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IIndexBuilder<RecordRoot> _indexBuilder;
        private readonly AlgoliaOptions _algoliaOption;
        private readonly DocService _docClient;

        public IndexHostedService(
            ILogger<IndexHostedService> logger,
            IHostApplicationLifetime appLifetime,
            IIndexBuilder<RecordRoot> indexBuilder,
            IOptions<AlgoliaOptions> algoliaOption,
            DocService docClient)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _algoliaOption = algoliaOption.Value;
            _algoliaOption.RootDocsPath.AssertParamNotNull(nameof(AlgoliaOptions.RootDocsPath));
            _algoliaOption.DocDomain.AssertParamNotNull(nameof(AlgoliaOptions.DocDomain));
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);
            _indexBuilder = indexBuilder;
            _docClient = docClient;
        }

        private Process _WebSiteProcess = new Process();


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("1. StartAsync has been called.");
            //StartDocServer();
            return Task.CompletedTask;
        }


        //private Task StartCommand(string cmd)
        //{
        //    var startInf = GetStartInfo(cmd);
        //    var p = Process.Start(startInf);
        //    if (p == null)
        //    {
        //        throw new Exception("启动命令失败: " + cmd);
        //    }
        //    return p.WaitForExitAsync();
        //}

        //private ProcessStartInfo GetStartInfo(string cmd)
        //{
        //    var StartInfo = new ProcessStartInfo()
        //    {
        //        FileName = "/bin/bash",
        //        Arguments = $"-c \"{cmd}\"",
        //        RedirectStandardOutput = true,
        //        RedirectStandardError = true,
        //        UseShellExecute = false,
        //        CreateNoWindow = true
        //    };
        //    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        //    {
        //        StartInfo.FileName = "cmd";
        //        StartInfo.Arguments = $"/c \"{cmd}\"";
        //    }
        //    return StartInfo;
        //}

        //private void StartDocServer()
        //{
        //    var serverPath = Path.Combine(_algoliaOption.RootDocsPath, "Masa.Docs.WebAssembly/Masa.Docs.WebAssembly.csproj");
        //    var cmd = $"dotnet run --project {serverPath}";
        //    //go to start website
        //    _WebSiteProcess.StartInfo = GetStartInfo(cmd);
        //    _WebSiteProcess.ErrorDataReceived += (sender, e) =>
        //    {
        //        if (sender is Process process)
        //        {
        //            _logger.LogError(process.StandardError.ReadToEnd());
        //        }
        //    };
        //    _WebSiteProcess.Exited += (sender, e) =>
        //    {
        //        if (sender is Process process)
        //        {
        //            if (process.ExitCode != 0)
        //            {
        //                _logger.LogError(process.StandardError.ReadToEnd());
        //                _logger.LogInformation(process.StandardOutput.ReadToEnd());

        //            }
        //            process.Dispose();
        //        }
        //    };
        //    _WebSiteProcess.Start();
        //}
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("4. StopAsync has been called.");
            return Task.CompletedTask;
        }

        private async void OnStarted()
        {
            await _indexBuilder.CreateIndexAsync();
            _appLifetime.StopApplication();
            _logger.LogInformation("2. OnStarted has been called.");
        }

        private void OnStopping()
        {
            _logger.LogInformation("3. OnStopping has been called.");
        }

        private void OnStopped()
        {
            _logger.LogInformation("5. OnStopped has been called.");
        }

    }
}