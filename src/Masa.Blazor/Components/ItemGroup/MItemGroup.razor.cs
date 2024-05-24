using Masa.Blazor.Components.ItemGroup;

namespace Masa.Blazor;

public partial class MItemGroup : MItemGroupBase, IThemeable
{
    public MItemGroup() : base(GroupType.ItemGroup)
    {
    }

    public MItemGroup(GroupType groupType) : base(groupType)
    {
    }

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public StringNumber? Max { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    public virtual bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            if (Light)
            {
                return false;
            }

            return CascadingIsDark;
        }
    }

    protected override List<StringNumber?> UpdateInternalValues(StringNumber? value)
    {
        var internalValues = InternalValues.ToList();

        if (internalValues.Contains(value))
        {
            internalValues.Remove(value);
        }
        else
        {
            if (!Multiple)
            {
                internalValues.Clear();
            }

            if (Max == null || internalValues.Count < Max.TryGetNumber().number)
            {
                internalValues.Add(value);
            }
        }

        if (Mandatory && internalValues.Count == 0)
        {
            internalValues.Add(value);
        }

        return internalValues;
    }

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif
    
    private Block _block = new("m-item-group");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.AddTheme(IsDark, IndependentTheme).GenerateCssClasses();
    }
}