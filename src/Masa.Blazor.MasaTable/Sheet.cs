using System.Text.Json.Serialization;

namespace Masa.Blazor.MasaTable;

public class Sheet
{
    private Guid _prevActiveViewId;

    /// <summary>
    /// All columns without state.
    /// </summary>
    public List<Column> Columns { get; set; } = [];

    /// <summary>
    /// The identifier of the active view.
    /// </summary>
    public Guid ActiveViewId { get; set; }

    /// <summary>
    /// The identifier of the sheet.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// All views of the sheet.
    /// </summary>
    public List<View> Views { get; set; } = [];

    public static Sheet CreateDefault()
    {
        var defaultView = new View()
        {
            Id = Guid.NewGuid(),
            Name = "Default",
        };

        return new Sheet()
        {
            Id = Guid.NewGuid(),
            ActiveViewId = defaultView.Id,
            Views = [defaultView],
            Columns = []
        };
    }

    [JsonIgnore] public View? ActiveView => Views.FirstOrDefault(v => v.Id == ActiveViewId);
}