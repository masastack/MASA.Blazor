using Masa.Blazor.Doc.Services;

namespace Masa.Blazor.Doc.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private CrawlService _crawlService;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddMasaBlazor().AddI18nForServer("wwwroot/locale");

            services.AddMasaBlazorDocs(Configuration["ASPNETCORE_URLS"]?.Replace("0.0.0.0", "127.0.0.1") ?? "http://localhost:5000");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime, CrawlService crawlService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            _crawlService = crawlService;

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapGet("/robots.txt", context => _crawlService.GetRobotsTxt(context));
                endpoints.MapGet("/sitemap.xml", context => _crawlService.GetSitemap(context));
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
