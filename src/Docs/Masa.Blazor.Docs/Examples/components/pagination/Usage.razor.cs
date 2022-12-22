namespace Masa.Blazor.Docs.Examples.components.pagination;

public class Usage : Masa.Blazor.Docs.Components.Usage
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
