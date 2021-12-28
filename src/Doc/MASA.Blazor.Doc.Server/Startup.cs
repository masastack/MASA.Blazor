using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Reflection;
using BlazorComponent.Components;
using MASA.Blazor.Doc.Utils;
using MASA.Blazor.Doc.Middleware;
using MASA.Blazor.Doc.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MASA.Blazor.Doc.Server
{
    public class Startup
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddMasaBlazor();

            services.AddHttpContextAccessor();

            services.AddHttpClient<DemoService>(c => c.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.129 Safari/537.36 Edg/81.0.416.68"));

            services.AddMasaBlazorDocs();

            I18nHelper.AddLang();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                _httpClient.BaseAddress = new Uri(Configuration["ASPNETCORE_URLS"]);
                app.UseDeveloperExceptionPage();
            }
            else
            {
                _httpClient.BaseAddress = new Uri("http://127.0.0.1:5000");
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<CookieMiddleware>();

            app.UseRequestLocalization(opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("zh-CN"),
                    new CultureInfo("en-US")
                };

                opts.SupportedCultures = supportedCultures;
                opts.SupportedUICultures = supportedCultures;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
