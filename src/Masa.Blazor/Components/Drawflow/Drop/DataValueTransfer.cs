namespace Masa.Blazor;

public class DataValueTransfer : DataTransfer
{
    /// <summary>
    /// Built-in data
    /// </summary>
    public DataTransferData Data { get; set; } = null!;
}

public class DataTransferData
{
    [Parameter] public string? Value { get; set; }

    [Parameter] public double OffsetX { get; set; }

    [Parameter] public double OffsetY { get; set; }
}
