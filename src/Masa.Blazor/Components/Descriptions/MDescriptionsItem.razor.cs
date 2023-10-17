﻿namespace Masa.Blazor;

public class MDescriptionsItem : ComponentBase, IDescriptionsItem, IAsyncDisposable
{
    [CascadingParameter] private MDescriptions? Descriptions { get; set; }

    [Parameter, EditorRequired] public string Label { get; set; } = null!;

    [Parameter, ApiDefaultValue(1)] public int Span { get; set; } = 1;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public string? LabelStyle { get; set; }

    [Parameter] public string? LabelClass { get; set; }

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Style { get; set; }

    private bool _renderFromAncestor;
    private bool _registered;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        Label.ThrowIfNull(nameof(MDescriptionsItem));
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (Descriptions != null)
        {
            await Descriptions.Register(this);
            _registered = true;
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (_renderFromAncestor)
        {
            _renderFromAncestor = false;
            return;
        }

        if (_registered)
        {
            Descriptions?.UpdateChild(this);
        }
    }

    // inherit
    public void RenderFromAncestor()
    {
        _renderFromAncestor = true;
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (Descriptions != null)
        {
            await Descriptions.Unregister(this);
        }
    }
}
