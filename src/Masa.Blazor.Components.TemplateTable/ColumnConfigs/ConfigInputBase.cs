namespace Masa.Blazor.Components.TemplateTable.ColumnConfigs;

public abstract class ConfigInputBase<TConfig> : ComponentBase where TConfig : new()
{
    [Parameter] public string? Value { get; set; }

    [Parameter] public EventCallback<string?> ValueChanged { get; set; }

    protected TConfig Config { get; set; } = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (Value is not null)
        {
            var result = JsonSerializer.Deserialize<TConfig>(Value);
            if (result is not null)
            {
                Config = result;
            }
        }
    }

    protected void UpdateValue()
    {
        ValueChanged.InvokeAsync(JsonSerializer.Serialize(Config));
    }
}