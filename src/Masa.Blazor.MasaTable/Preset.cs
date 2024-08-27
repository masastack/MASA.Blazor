namespace Masa.Blazor.MasaTable;

internal static class Preset
{
    internal static IReadOnlyDictionary<ColumnType, string> ColumnTypeIcons { get; } =
        new Dictionary<ColumnType, string>
        {
            [ColumnType.Checkbox] = "mdi-checkbox-marked-outline",
            [ColumnType.CreatedAt] = "mdi-calendar-clock",
            [ColumnType.Date] = "mdi-calendar-blank-outline",
            [ColumnType.Email] = "mdi-email-outline",
            [ColumnType.Image] = "mdi-image-outline",
            [ColumnType.Link] = "mdi-link-variant",
            [ColumnType.MultiSelect] = "mdi-format-list-checkbox",
            [ColumnType.Number] = "mdi-numeric",
            [ColumnType.Phone] = "mdi-phone-outline",
            [ColumnType.Progress] = "mdi-calendar-clock",
            [ColumnType.Rate] = "mdi-star-outline",
            [ColumnType.Select] = "mdi-checkbox-marked-circle-outline",
            [ColumnType.Text] = "mdi-text-long",
        };
}