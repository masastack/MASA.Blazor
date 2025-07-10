namespace Masa.Blazor.Components.TemplateTable.ColumnConfigs;

public abstract class ConfigInputBase<TConfig> : ComponentBase where TConfig : new()
{
    [Parameter] public string? Value { get; set; }

    [Parameter] public EventCallback<object> OnUpdate { get; set; }

    protected TConfig Config { get; set; } = new();

    private string? _prevValue;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (_prevValue != Value)
        {
            _prevValue = Value;

            if (Value is null)
            {
                Config = new();
            }
            else
            {
                var result = JsonSerializer.Deserialize<TConfig>(Value);
                if (result is not null)
                {
                    Config = result;
                }
            }
        }
    }

    protected void UpdateValue()
    {
        OnUpdate.InvokeAsync(Config);
    }
}