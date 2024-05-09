namespace Masa.Blazor;

public class DataTableHeader
{
    public DataTableHeader()
    {
    }

    public DataTableHeader(string text, string value)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        Value = value ?? throw new ArgumentNullException(nameof(value));
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

    public bool Divider { get; set; }

    public string? Value { get; set; }

    public string? Text { get; set; }

    public bool Sortable { get; set; } = true;

    public DataTableHeaderAlign Align { get; set; } = DataTableHeaderAlign.Start;

    public bool Groupable { get; set; } = true;

    public string? Class { get; set; }

    public string? CellClass { get; set; }

    public StringNumber? Width { get; set; }

    public DataTableFixed Fixed { get; set; }

    /// <summary>
    /// The real width of the column, but 0 if <see cref="Width"/> is not null.
    /// </summary>
    public double RealWidth { get; internal set; }

    public bool IsFixedShadowColumn { get; internal set; }

    public DataTableEllipsis? Ellipsis { get; set; }

    internal bool HasEllipsis => Ellipsis?.Enabled ?? false;
}

public class DataTableEllipsis
{
    public bool Enabled { get; set; } = true;

    public bool HideTitle { get; set; }
}