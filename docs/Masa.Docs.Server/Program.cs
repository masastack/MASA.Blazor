using Masa.Docs.Core;
using Masa.Docs.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Services;

var builder = WebApplication.CreateBuilder(args);
// https://github.com/dotnet/aspnetcore/issues/38212
builder.WebHost.UseStaticWebAssets();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(options =>
{
    options.RootComponents.MaxJSRootComponents = 500;
    options.RootComponents.RegisterCustomElementsUsedJSCustomElementAttribute();
}).AddHubOptions(options => options.MaximumReceiveMessageSize = 64 * 1024);

builder.Services.AddHealthChecks();

builder.Services.AddScoped<LazyAssemblyLoader>();
builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.Themes.Light.Primary = "#4318FF";
        theme.Themes.Light.Secondary = "#5CBBF6";
        theme.Themes.Light.Accent = "#005CAF";
        theme.Themes.Light.UserDefined?.Add("Tertiary", "#e57373");
    });
}).AddI18nForServer("wwwroot/locale");

builder.Services.AddMasaDocs(builder.Configuration["ASPNETCORE_URLS"]?.Replace("0.0.0.0", "127.0.0.1") ?? "http://localhost:5000");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapHealthChecks("/healthz");

app.UseStaticFiles();

app.UseRouting();

// TODO: crawlService

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
