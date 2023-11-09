using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor;

public class MInputsFilter : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] [MassApiParameter(true)] public bool Dense { get; set; } = true;

    [Parameter] [MassApiParameter(true)] public StringBoolean? HideDetails { get; set; } = true;

    [Parameter] public EventCallback<InputsFilterFieldChangedEventArgs> OnFieldChanged { get; set; }

    private readonly List<IFilterInput> _inputs = new();

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<MInputsFilter>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<MInputsFilter>.Value), this);
        builder.AddAttribute(2, nameof(CascadingValue<MInputsFilter>.IsFixed), true);
        builder.AddAttribute(3, nameof(ChildContent), ChildContent);
        builder.CloseComponent();
    }

    internal Task FieldChange(string fieldName, bool isClear = false)
        => OnFieldChanged.InvokeAsync(new InputsFilterFieldChangedEventArgs(fieldName, isClear));

    internal void RegisterInput(IFilterInput input)
    {
        if (_inputs.Contains(input))
        {
            return;
        }

        _inputs.Add(input);
    }

    /// <summary>
    /// Reset the value of all inputs.
    /// </summary>
    [MasaApiPublicMethod]
    public void ResetInputs()
    {
        foreach (var input in _inputs)
        {
            input.ResetFilter();
        }
    }
}
