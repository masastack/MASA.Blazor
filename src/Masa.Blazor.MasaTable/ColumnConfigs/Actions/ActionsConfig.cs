namespace Masa.Blazor.MasaTable.ColumnConfigs;

public class ActionsConfig
{
    public string? UpdateIcon { get; set; } = "mdi-pencil-outline";

    public string? DeleteIcon { get; set; } = "mdi-delete-outline";

    public string? Action1Icon { get; set; } = "mdi-numeric-1";

    public string? Action2Icon { get; set; } = "mdi-numeric-2";

    public string? UpdateTooltip { get; set; } = "Update";

    public string? DeleteTooltip { get; set; } = "Delete";

    public string? Action1Tooltip { get; set; }

    public string? Action2Tooltip { get; set; }
}