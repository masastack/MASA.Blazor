using Microsoft.AspNetCore.Components.Web;

namespace Masa.Docs.Shared.Examples.pagination;

public class Usage : Masa.Docs.Shared.Components.Usage
{
    public Usage() : base(typeof(MPagination))
    {
    }

    private int _value = 1;

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MPagination.Value), _value },
            { nameof(MPagination.Length), 6 },
            { nameof(MBottomSheet.ValueChanged), EventCallback.Factory.Create<int>(this, val =>
            {
                _value = val;
            }) },
        };
    }
}
