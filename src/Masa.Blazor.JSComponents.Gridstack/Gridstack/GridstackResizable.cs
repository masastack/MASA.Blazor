using System.Text.Json.Serialization;

namespace Masa.Blazor;

public class GridstackResizable
{
    public GridstackResizable(bool autoHide = true, GridstackResizeHandle handles = GridstackResizeHandle.SouthEast)
    {
        AutoHide = autoHide;
        Handles = handles;
    }

    public bool AutoHide { get; set; }

    [JsonIgnore]
    public GridstackResizeHandle Handles { get; set; }

    [JsonPropertyName("handles")]
    public string ComputedHandles
    {
        get
        {
            return Handles switch
            {
                GridstackResizeHandle.SouthEast => "se",
                GridstackResizeHandle.SouthWest => "sw",
                GridstackResizeHandle.NorthEast => "ne",
                GridstackResizeHandle.NorthWest => "nw",
                GridstackResizeHandle.South => "s",
                GridstackResizeHandle.West => "w",
                GridstackResizeHandle.East => "e",
                GridstackResizeHandle.North => "n",
                GridstackResizeHandle.All => "all",
                _ => "se"
            };
        }
    }
}

public enum GridstackResizeHandle
{
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest,
    All
}