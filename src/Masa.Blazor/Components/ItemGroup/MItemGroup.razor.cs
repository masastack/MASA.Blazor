using Masa.Blazor.Components.ItemGroup;
using Masa.Blazor.Mixins;

namespace Masa.Blazor;

public partial class MItemGroup : MItemGroupBase, IThemeable
{
    public MItemGroup(): base(GroupType.ItemGroup)
    {
    }

    [Parameter] public StringNumber? Max { get; set; }

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
    
    private static Block _block = new("m-item-group");

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _block.Name;
        yield return CssClassUtils.GetTheme(ComputedTheme);
    }
}