using Masa.Blazor.Components.TemplateTable.DetailDialogs;

namespace Masa.Blazor.Components.TemplateTable.Actions;

public record DetailActionData(EventCallback<List<DetailItem>> OnClick, List<DetailItem>? DetailItems);