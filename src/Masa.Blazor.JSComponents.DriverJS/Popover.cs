using System.Text.Json.Serialization;
using Masa.Blazor.JSInterop;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.JSComponents.DriverJS;

public class Popover
{
    public Popover(string? title,
        string? description,
        PopoverSide side,
        PopoverAlign align,
        string? popoverClass)
    {
        Title = title;
        Description = description;
        Side = side;
        Align = align;
        PopoverClass = popoverClass;
    }

    public Popover(string title, string description)
    {
        Title = title;
        Description = description;
    }

    internal Popover()
    {
    }

    [Parameter] public string? Title { get; set; }

    [Parameter] public string? Description { get; set; }

    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    [Parameter] public PopoverSide Side { get; set; }

    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    [Parameter] public PopoverAlign Align { get; set; }

    [Parameter] public string? PopoverClass { get; set; }

    /// <summary>
    /// Array of buttons to show in the popover.
    /// When highlighting a single element, there
    /// are no buttons by default. When showing
    /// a tour, the default buttons are "next",
    /// "previous" and "close".
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public PopoverButton[]? ShowButtons { get; set; }

    /// <summary>
    /// An array of buttons to disable. This is
    /// useful when you want to show some of the
    /// buttons, but disable some of them.
    /// </summary>
    [JsonIgnore]
    [Parameter]
    public PopoverButton[]? DisableButtons { get; set; }

    [Parameter]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? NextBtnText { get; set; }

    [Parameter]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? PrevBtnText { get; set; }

    [Parameter]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? DoneBtnText { get; set; }

    [JsonPropertyName("showButtons")]
    public string[]? ShowButtonsInternal =>
        ShowButtons?.Select(button => button.ToString().ToLowerInvariant()).ToArray();

    [JsonPropertyName("disableButtons")]
    public string[]? DisableButtonsInternal =>
        DisableButtons?.Select(button => button.ToString().ToLowerInvariant()).ToArray();
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

public enum PopoverButton
{
    /// <summary>
    /// The next button.
    /// </summary>
    Next,

    /// <summary>
    /// The previous button.
    /// </summary>
    Previous,

    /// <summary>
    /// The close icon.
    /// </summary>
    Close
}