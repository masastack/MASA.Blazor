using System.Text.Json.Serialization;
using Masa.Blazor.JSInterop;

namespace Masa.Blazor.JSComponents.DriverJS;

public record Popover
{
    public Popover(string? Title,
        string? Description,
        PopoverSide Side,
        PopoverAlign Align,
        string? PopoverClass)
    {
        this.Title = Title;
        this.Description = Description;
        this.Side = Side;
        this.Align = Align;
        this.PopoverClass = PopoverClass;
    }

    public Popover(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public string? Title { get; init; }
    public string? Description { get; init; }

    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    public PopoverSide Side { get; init; }

    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    public PopoverAlign Align { get; init; }

    public string? PopoverClass { get; init; }

    public void Deconstruct(out string? Title, out string? Description, out PopoverSide Side, out PopoverAlign Align,
        out string? PopoverClass)
    {
        Title = this.Title;
        Description = this.Description;
        Side = this.Side;
        Align = this.Align;
        PopoverClass = this.PopoverClass;
    }
}

public enum PopoverSide
{
    Top,
    Right,
    Bottom,
    Left
}

public enum PopoverAlign
{
    Start,
    Center,
    End
}