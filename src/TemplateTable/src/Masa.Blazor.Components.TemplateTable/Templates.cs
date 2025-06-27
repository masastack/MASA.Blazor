namespace Masa.Blazor.Components.TemplateTable;

public static class Templates
{
    public static RenderFragment GenTypeIcon(ColumnType columnType, bool small = false) => builder =>
    {
        builder.OpenComponent<MIcon>(0);
        builder.AddAttribute(1, nameof(MIcon.Small), small);
        builder.AddAttribute(2, nameof(MIcon.Class), "mr-1");
        builder.AddAttribute(3, nameof(MIcon.Style), "color: inherit;");
        builder.AddAttribute(4, "ChildContent",
            (RenderFragment)(c => c.AddContent(0, Preset.GetColumnTypeIcon(columnType))));
        builder.CloseComponent();
    };
}