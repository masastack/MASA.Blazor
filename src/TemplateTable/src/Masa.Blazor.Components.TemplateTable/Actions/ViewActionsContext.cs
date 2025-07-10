namespace Masa.Blazor.Components.TemplateTable.Actions;

public record ViewActionsContext
{
    internal ViewActionsContext(IDictionary<Row, bool> selection)
    {
        var selectedRows = selection.Where(pair => pair.Value).Select(pair => pair.Key).ToList();
        SelectedRows = selectedRows;
        SelectedKeys = selectedRows.Select(row => row.Key).ToList();
        HasSelectedRows = selectedRows.Count != 0;
    }

    internal ViewActionsContext()
    {
    }

    public IReadOnlyCollection<Row> SelectedRows { get; } = [];

    public bool HasSelectedRows { get; private set; }

    public List<string> SelectedKeys { get; private set; } = [];
}