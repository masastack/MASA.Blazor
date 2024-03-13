namespace Masa.Blazor;

public class DrawflowNode<TData>
{
    public string? Class { get; set; }

    public string? Html { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public int PosX { get; set; }

    public int PosY { get; set; }

    public TData? Data { get; set; }
}
