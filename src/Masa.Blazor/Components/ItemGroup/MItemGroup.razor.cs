using Masa.Blazor.Components.ItemGroup;
using Masa.Blazor.Mixins;

namespace Masa.Blazor;

public partial class MItemGroup : MItemGroupBase, IThemeable
{
    public MItemGroup(): base(GroupType.ItemGroup)
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
        var valueExists = internalValues.Contains(value);

        if (Multiple)
        {
            if (valueExists)
            {
                internalValues.Remove(value);
                ToggleButUnselect = true;
            }
            else
            {
                if (Max == null || internalValues.Count < Max.TryGetNumber().number)
                {
                    internalValues.Add(value);
                }
            }
        }
        else
        {
            if (valueExists)
            {
                if (!Mandatory)
                {
                    internalValues.Remove(value);
                }
            }
            else
            {
                internalValues.Clear();
                internalValues.Add(value);
            }
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
    
    private static Block _block = new("m-item-group");

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _block.Name;
        yield return CssClassUtils.GetTheme(IsDark, IndependentTheme);
    }
}