using Masa.Docs.Indexing;
using Masa.Docs.Indexing.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.Hosting;
using Polly;
using System.Text.RegularExpressions;
using System.Web;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        configuration.Sources.Clear();
        IHostEnvironment env = hostingContext.HostingEnvironment;
        configuration
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
           .AddEnvironmentVariables(x=>x.Prefix= "MASA_")
           ;
        IConfigurationRoot configurationRoot = configuration.Build();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<IIndexBuilder<RecordRoot>, AlgoliaIndexBuilder>();
        var x = hostContext.Configuration.GetSection(AlgoliaOptions.Position);
        services.Configure<AlgoliaOptions>(x);
        AlgoliaOptions options = new AlgoliaOptions();
        x.Bind(options);
        services.AddHttpClient<DocService>(
            client =>
            {
                client.BaseAddress = new Uri(options.DocDomain);
            })
        .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
        {
            TimeSpan.FromSeconds(5),
            TimeSpan.FromSeconds(20),
            TimeSpan.FromSeconds(35)
        }));
        services.AddHostedService<IndexHostedService>();
    })
    .Build();

await host.RunAsync();