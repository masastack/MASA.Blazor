namespace Masa.Blazor;

public class EChartsEventArgs
{
    /// <summary>
    /// The component name clicked,
    /// component type, could be 'series'、'markLine'、'markPoint'、'timeLine', etc..
    /// </summary>
    public string? ComponentType { get; set; }

    /// <summary>
    /// series type, could be 'line'、'bar'、'pie', etc.. Works when componentType is 'series'.
    /// </summary>
    public string? SeriesType { get; set; }

    /// <summary>
    /// the index in option.series. Works when componentType is 'series'.
    /// </summary>
    public int SeriesIndex { get; set; }

    /// <summary>
    /// series name, works when componentType is 'series'.
    /// </summary>
    public string? SeriesName { get; set; }

    /// <summary>
    /// name of data (categories).
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// the index in 'data' array.
    /// </summary>
    public int DataIndex { get; set; }

    /// <summary>
    /// incoming raw data item
    /// </summary>
    public object? Data { get; set; }

    /// <summary>
    /// charts like 'sankey' and 'graph' included nodeData and edgeData as the same time.
    /// dataType can be 'node' or 'edge', indicates whether the current click is on node or edge.
    /// most of charts have one kind of data, the dataType is meaningless
    /// </summary>
    public string? DataType { get; set; }

    /// <summary>
    /// incoming data value
    /// </summary>
    public object[]? Value { get; set; }

    /// <summary>
    /// olor of the shape, works when componentType is 'series'.
    /// </summary>
    public string? Color { get; set; }
}
