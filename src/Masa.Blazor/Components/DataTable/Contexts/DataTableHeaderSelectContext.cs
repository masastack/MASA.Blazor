namespace Masa.Blazor;

public record DataTableHeaderSelectContext(bool Value, EventCallback<bool> OnToggle, bool Indeterminate);
