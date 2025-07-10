using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var hotChocolateDemo = builder.AddProject<Projects.HotChocolateDemo>("hotchocolate-demo");
builder.AddProject<Projects.TemplateTableSample_UI>("ui")
    .WithExternalHttpEndpoints()
    .WithReference(hotChocolateDemo)
    .WaitFor(hotChocolateDemo);

builder.Build().Run();