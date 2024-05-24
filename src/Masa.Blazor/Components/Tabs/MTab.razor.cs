using Masa.Blazor.Components.ItemGroup;
using Masa.Blazor.Mixins;

namespace Masa.Blazor;

public partial class MTab : MRoutableGroupItem<MItemGroupBase>
{
    public MTab() : base(GroupType.SlideGroup)
    {
    }

    [CascadingParameter] public MTabs? Tabs { get; set; }

    [Parameter] [MasaApiParameter(true)] public bool Ripple { get; set; } = true;

    protected override bool HasRoutableAncestor => Tabs?.Routable is true;

    protected override bool IsRoutable => Href != null && HasRoutableAncestor;

    protected override bool AfterHandleEventShouldRender() => false;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Attributes["ripple"] = !Disabled && Ripple;
    }

    private Block _block = new("m-tab");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier(Disabled)
            .And("active", InternalIsActive)
            .AddClass(ComputedActiveClass, InternalIsActive)
            .GenerateCssClasses();
    }

    private async Task HandleOnClick(MouseEventArgs args)
    {
        if (!IsRoutable)
        {
            await ToggleAsync();
        }

        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(args);
        }
    }
}