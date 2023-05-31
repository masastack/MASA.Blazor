namespace Masa.Blazor;

public interface IDescriptionItem
{
    string? Label { get; }

    int Span { get; }

    RenderFragment? ChildContent { get; }
}

public class MDescriptionItem : ComponentBase, IDescriptionItem
{
    [CascadingParameter] private MDescriptions? Descriptions { get; set; }

    [Parameter, EditorRequired] public string Label { get; set; } = null!;

    [Parameter, ApiDefaultValue(1)] public int Span { get; set; } = 1;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        Label.ThrowIfNull(nameof(MDescriptionItem));
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Descriptions?.Register(this);
    }
}
