namespace Masa.Blazor.Components.TemplateTable;

internal static class Preset
{
    internal static string ActionsColumnId { get; } = "__internal_actions";
    
    internal static ColumnInfo CreateActionsColumn()
    {
        return new ColumnInfo()
        {
            Id = ActionsColumnId,
            Name = "Actions",
            Type = ColumnType.Actions
        };
    }

    internal static ViewColumnInfo CreateActionsViewColumn()
    {
        return new ViewColumnInfo()
        {
            ColumnId = ActionsColumnId,
            Column = CreateActionsColumn()
        };
    }

    internal static IReadOnlyDictionary<ColumnType, string> ColumnTypeIcons { get; } =
        new Dictionary<ColumnType, string>
        {
            [ColumnType.Checkbox] = "mdi-checkbox-marked-outline",
            [ColumnType.Date] = "mdi-calendar-blank-outline",
            [ColumnType.Email] = "mdi-email-outline",
            [ColumnType.Image] = "mdi-image-outline",
            [ColumnType.Link] = "mdi-link-variant",
            [ColumnType.MultiSelect] = "mdi-format-list-checkbox",
            [ColumnType.Number] = "mdi-numeric",
            [ColumnType.Phone] = "mdi-phone-outline",
            [ColumnType.Progress] = "mdi-calendar-clock",
            [ColumnType.Rating] = "mdi-star-outline",
            [ColumnType.Select] = "mdi-checkbox-marked-circle-outline",
            [ColumnType.Text] = "mdi-text-long",
            [ColumnType.Actions] = "mdi-dots-horizontal",
            [ColumnType.Custom] = "mdi-cog-outline"
        };
}