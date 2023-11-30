using BlazorComponent;
using Masa.Blazor;
using Masa.Blazor.SsrPlayground;
using Masa.Blazor.SsrPlayground.Components;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.WebHost.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
}

// Add services to the container.
builder.Services.AddRazorComponents()
       .AddInteractiveServerComponents();

builder.Services.AddMasaBlazor(opts =>
{
    opts.Locale = new Locale("zh-CN", "en-US");
    opts.RTL = true;
    opts.ConfigureTheme(t => t.Dark = true); 
});

// builder.Services.AddCascadingValue("Test", sp => "Hello from CascadingValue");

// builder.Services.AddCascadingValue(sp =>
// {
//     var source = new CascadingValueSource<TestModel>(new TestModel() { Name = "Hello form CascadingValueSource" }, isFixed: false);
//     Task.Run(async () =>
//     {
//         Console.Out.WriteLine("CascadingValueSource: Waiting 1 second...");
//         await Task.Delay(1000);
//         await source.NotifyChangedAsync(new TestModel() { Name = "Hello from CascadingValueSource after 1 second" });
//     });
//     return source;
// });

var testModeSource = new CascadingValueSource<TestModel>(new TestModel() { Name = "Hello form CascadingValueSource" }, isFixed: false);
builder.Services.AddScoped<CascadingValueSource<TestModel>>(_ => testModeSource);

builder.Services.AddCascadingValue(_ => testModeSource);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
