using Masa.Docs.Indexing;
using Masa.Docs.Indexing.Configurations;
using Masa.Docs.Indexing.Data;
using Masa.Docs.Indexing.Parsers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(cfg =>
    {
        cfg.ClearProviders();
        cfg.AddConsole();
        cfg.AddDebug();
    })
    .ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        configuration.Sources.Clear();
        IHostEnvironment env = hostingContext.HostingEnvironment;
        configuration
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
           .AddEnvironmentVariables(x => x.Prefix = "MASA_");
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddOptions();
        services.AddSingleton<IIndexBuilder<RecordRoot>, AlgoliaIndexBuilder>();
        services.Configure<AlgoliaOptions>(hostContext.Configuration.GetSection(AlgoliaOptions.Position));
        services.AddSingleton<IMasaDocParser, MasaDocParser>();
        services.AddSingleton<IConfigureOptions<AlgoliaOptions>, AlgoliaFromEnvironmentConfiguration>();
        services.AddHostedService<IndexingHostedService>();
    })
    .Build();

await host.RunAsync();