using Masa.Blazor.SsrPlayground.Components;
using Masa.Blazor.SsrPlayground.Services;

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
    opts.ConfigureSsr(ssr =>
    {
        ssr.Left = 256;
        ssr.Top = 64;
    });
});

builder.Services.AddScoped<AppState>();
builder.Services.AddSingleton<NotificationMockService>();

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
