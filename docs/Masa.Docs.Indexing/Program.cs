using Masa.Docs.Indexing;
using Masa.Docs.Indexing.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        configuration.Sources.Clear();
        IHostEnvironment env = hostingContext.HostingEnvironment;
        configuration
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
           .AddEnvironmentVariables(x => x.Prefix = "MASA_")
           ;
        IConfigurationRoot configurationRoot = configuration.Build();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<IIndexBuilder<RecordRoot>, AlgoliaIndexBuilder>();
        services.Configure<AlgoliaOptions>(hostContext.Configuration.GetSection(AlgoliaOptions.Position));
        services.AddHostedService<IndexingHostedService>();
    })
    .Build();

await host.RunAsync();