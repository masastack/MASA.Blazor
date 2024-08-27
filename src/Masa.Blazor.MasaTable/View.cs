namespace Masa.Blazor.MasaTable;

public class View
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public ViewType Type { get; set; }

    public List<Guid> Rows { get; set; } = [];

    public List<ViewColumn> Columns { get; set; } = [];
}

public class ViewColumn(Column column)
{
    public string Id => Column.Id;

    public int Width { get; set; }

    public bool Hidden { get; set; }

    internal Column Column { get; } = column;
    
    public ViewColumn ShallowClone()
    {
        return new ViewColumn(Column)
        {
            Width = Width,
            Hidden = Hidden
        };
    }
}

public enum ViewType
{
    Grid = 0,
}