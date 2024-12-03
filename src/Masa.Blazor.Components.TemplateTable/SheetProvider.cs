using GraphQL;

namespace Masa.Blazor.Components.TemplateTable;

public delegate ValueTask<GraphQLResponse<SheetProviderResult>> SheetProvider(SheetProviderRequest request);