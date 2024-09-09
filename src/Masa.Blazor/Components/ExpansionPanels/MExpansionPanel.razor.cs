using Masa.Blazor.Components.ItemGroup;

namespace Masa.Blazor;

public partial class MExpansionPanel : MGroupableBase<MExpansionPanels>
{
    public MExpansionPanel() : base(GroupType.ExpansionPanels, true)
    {
    }

    [Parameter] public bool Readonly { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedOn = "v1.6.0")]
    public string? Title { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedOn = "v1.6.0")]
    public string? Text { get; set; }
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    public bool NextActive => ItemGroup != null && ItemGroup.NextActiveKeys.Contains(Value);

    public bool IsDisabled => ItemGroup != null && (ItemGroup.Disabled || Disabled);

    public bool IsReadonly => ItemGroup != null && (ItemGroup.Readonly || Readonly);

    public bool Booted => IsBooted;

    public async Task Toggle()
    {
        await ToggleAsync();
    }

    private static Block _block = new("m-expansion-panel");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder
            .Add(NextActive)
            .Add("disabled", IsDisabled)
            .Add("active", InternalIsActive)
            .AddClass(ComputedActiveClass, InternalIsActive)
            .Build();
    }
}