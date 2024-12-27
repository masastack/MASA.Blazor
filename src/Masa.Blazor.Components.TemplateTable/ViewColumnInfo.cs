namespace Masa.Blazor.Components.TemplateTable;

public class ViewColumnInfo : ViewColumn
{
    public ViewColumnInfo()
    {
        if (Column?.Id == Preset.ActionsColumnId)
        {
            Fixed = ColumnFixed.Right;
        }
        else if (Column?.Id == Preset.RowSelectColumnId)
        {
            Fixed = ColumnFixed.Left;
        }
    }

    [JsonIgnore] internal ColumnInfo Column { get; set; }

    internal static ViewColumnInfo From(ViewColumn viewColumn, ColumnInfo column)
    {
        return new ViewColumnInfo
        {
            ColumnId = viewColumn.ColumnId,
            Width = viewColumn.Width,
            Hidden = viewColumn.Hidden,
            Fixed = viewColumn.Fixed,
            Column = column
        };
    }

    internal static ViewColumnInfo From(string columnId, bool hidden, ColumnInfo column)
    {
        return new ViewColumnInfo
        {
            ColumnId = columnId,
            Hidden = hidden,
            Column = column,
        };
    }
}