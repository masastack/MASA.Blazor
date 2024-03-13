namespace Masa.Blazor;

public record DataTableHeaderSelectContext(bool Value, bool Indeterminate, EventCallback<bool> ValueChanged);
