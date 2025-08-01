namespace Masa.Blazor;

public class DataTableHeader
{
    public DataTableHeader()
    {
    }

    public DataTableHeader(string text, string value)
    {
        Text = text;
        Value = value;
    }

    public DataTableHeader(string text, string value, bool sortable) : this(text, value)
    {
        Sortable = sortable;
    }

    public DataTableHeader(string text, string value, StringNumber width) : this(text, value)
    {
        Width = width;
    }

    public DataTableHeader(string text, string value, StringNumber width, bool sortable) : this(text, value, width)
    {
        Sortable = sortable;
    }

    /// <summary>
    /// Display a divider at the right slide of the column.
    /// </summary>
    public bool Divider { get; set; }

    /// <summary>
    /// The identifier of the column.
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// The text displayed in the header cell.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Determines whether the column is sortable.
    /// </summary>
    public bool Sortable { get; set; } = true;

    /// <summary>
    /// The alignment of the column.
    /// </summary>
    public DataTableHeaderAlign Align { get; set; } = DataTableHeaderAlign.Start;

    /// <summary>
    /// Determines whether the column is groupable.
    /// </summary>
    public bool Groupable { get; set; } = true;

    /// <summary>
    /// The class of the table header cell.
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    /// The class of the table cell.
    /// </summary>
    public string? CellClass { get; set; }

    /// <summary>
    /// The width of the column.
    /// </summary>
    public StringNumber? Width { get; set; }

    /// <summary>
    /// Determines whether the column is fixed.
    /// </summary>
    public DataTableFixed Fixed { get; set; }

    public DataTableEllipsis? Ellipsis { get; set; }

    /// <summary>
    /// Determines whether the column is resizable.
    /// It's only effective when the ResizeMode is set.
    /// Supported since v1.8.0.
    /// </summary>
    public bool Resizable { get; set; } = true;

    /// <summary>
    /// The real width of the column, but 0 if <see cref="Width"/> is not null.
    /// </summary>
    internal double RealWidth { get; set; }

    internal bool IsFixedShadowColumn { get; set; }

    internal bool HasEllipsis => Ellipsis?.Enabled ?? false;
}

public class DataTableEllipsis
{
    public bool Enabled { get; set; } = true;

    public bool HideTitle { get; set; }
}