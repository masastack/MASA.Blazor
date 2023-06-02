namespace Masa.Blazor;

public class MDescriptionsItem : ComponentBase, IDescriptionsItem
{
    [CascadingParameter] private MDescriptions? Descriptions { get; set; }

    [Parameter, EditorRequired] public string Label { get; set; } = null!;

    [Parameter, ApiDefaultValue(1)] public int Span { get; set; } = 1;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public string? LabelStyle { get; set; }

    [Parameter] public string? LabelClass { get; set; }

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Style { get; set; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        Label.ThrowIfNull(nameof(MDescriptionsItem));
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Descriptions?.Register(this);
    }
}
