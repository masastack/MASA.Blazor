namespace Masa.Blazor;

public class MDefaultsProvider : ComponentBase, IDefaultsProvider
{
    [CascadingParameter] protected IDefaultsProvider? DefaultsProvider { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public IDictionary<string, IDictionary<string, object?>?>? Defaults { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        MergeDefaults();
    }

    protected virtual void MergeDefaults()
    {
        if (DefaultsProvider?.Defaults == null || Defaults == null) return;

        MergeDictionaryTo(DefaultsProvider.Defaults, Defaults);
    }

    protected static void MergeDictionaryTo(IDictionary<string, IDictionary<string, object?>?> defaultsFrom,
        IDictionary<string, IDictionary<string, object?>?> defaultsTo)
    {
        defaultsFrom.ForEach(item =>
        {
            var (component, cascadingParameters) = item;

            if (cascadingParameters is null) return;

            if (defaultsTo.TryGetValue(component, out var currentParameters))
            {
                if (currentParameters == null)
                {
                    return;
                }

                cascadingParameters.ForEach(parameter =>
                {
                    if (currentParameters.ContainsKey(parameter.Key)) return;

                    currentParameters.Add(parameter.Key, parameter.Value);
                });
            }
            else
            {
                defaultsTo.Add(component, cascadingParameters);
            }
        });
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<IDefaultsProvider>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<IDefaultsProvider>.IsFixed), true);
        builder.AddAttribute(2, nameof(CascadingValue<IDefaultsProvider>.Value), this);
        builder.AddAttribute(3, nameof(CascadingValue<IDefaultsProvider>.ChildContent), ChildContent);
        builder.CloseComponent();
    }
}
