using TemplateTableSample.UI.Components;
using TemplateTableSample.UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMasaBlazor()
    .AddGraphQLClientForTemplateTable("http://localhost:5297/graphql")
    .WithHotChocolate();

builder.Services.AddHttpClient<SheetService>(c => c.BaseAddress = new Uri("http://hotchocolate-demo"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.MapStaticAssets();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();