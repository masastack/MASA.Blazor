using Masa.Blazor.Mixins;

namespace Masa.Blazor;

public partial class MExpansionPanel : MGroupItem<MExpansionPanels>
{
    public MExpansionPanel() : base(GroupType.ExpansionPanels, true)
    {
    }

    [Parameter] public bool Readonly { get; set; }

    public bool NextActive => ItemGroup != null && ItemGroup.NextActiveKeys.Contains(Value);

    public bool IsDisabled => ItemGroup != null && (ItemGroup.Disabled || Disabled);

    public bool IsReadonly => ItemGroup != null && (ItemGroup.Readonly || Readonly);

    public bool Booted => IsBooted;

    public async Task Toggle()
    {
        await ToggleAsync();
    }

    private Block _block = new("m-expansion-panel");

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ActiveClass ??= "m-item--active";
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block
            .Modifier(NextActive)
            .And("disabled", IsDisabled)
            .And("active", InternalIsActive)
            .AddClass(ComputedActiveClass, InternalIsActive)
            .GenerateCssClasses();
    }
}