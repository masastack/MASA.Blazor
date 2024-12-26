using Masa.Blazor.Components.TemplateTable.Actions;

namespace Masa.Blazor.Components.TemplateTable;

public class RowActionBase : IComponent
{
    [CascadingParameter] private RowActionsContext? RowActions { get; set; }

    [Parameter] public string? Icon { get; set; }

    [Parameter] [EditorRequired] public string? Text { get; set; }

    [Parameter] public bool Menu { get; set; }

    [Parameter] public int Index { get; set; }

    public virtual EventCallback<IReadOnlyDictionary<string, JsonElement>> OnClick { get; set; }

    private bool _registered;

    protected internal virtual string? InternalId { get; set; }

    public void Attach(RenderHandle renderHandle)
    {
    }

    public async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        if (!_registered && RowActions is not null)
        {
            _registered = true;

            await RowActions.RegisterActionAsync(this);
        }
    }
}

public class RowAction : RowActionBase
{
    [Parameter]
    [EditorRequired]
    public override EventCallback<IReadOnlyDictionary<string, JsonElement>> OnClick { get; set; }
}

public class RowDetailAction : RowActionBase
{
    internal const string DefaultIcon = "mdi-chevron-right";
    internal const string DefaultText = "Detail";
    internal const string DefaultId = "_internal_detail";

    public RowDetailAction()
    {
        Icon = DefaultIcon;
        Text = DefaultText;
    }

    protected internal override string InternalId => DefaultId;
}