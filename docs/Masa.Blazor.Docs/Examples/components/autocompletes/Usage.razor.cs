namespace Masa.Blazor.Docs.Examples.components.autocompletes;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MAutocomplete<string, string, string>))
    {
    }

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MAutocomplete<string, string, string>.Dense), false },
        { nameof(MAutocomplete<string, string, string>.Filled), false },
        { nameof(MAutocomplete<string, string, string>.Rounded), false },
        { nameof(MAutocomplete<string, string, string>.Solo), false },
        { nameof(MAutocomplete<string, string, string>.SoloInverted), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MAutocomplete<string, string, string>.AutoSelectFirst), new CheckboxParameter("false", true) },
        { nameof(MAutocomplete<string, string, string>.Clearable), new CheckboxParameter("false", true) },
        { nameof(MAutocomplete<string, string, string>.Chips), new CheckboxParameter("false", true) },
        { nameof(MAutocomplete<string, string, string>.DeletableChips), new CheckboxParameter("false", true) },
        { nameof(MAutocomplete<string, string, string>.SmallChips), new CheckboxParameter("false", true) },
    };

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
          { nameof(MAutocomplete<string, string, string>.Items), _items },
          { nameof(MAutocomplete<string, string, string>.ItemText), (Func<string,string>)(x=>x) },
          { nameof(MAutocomplete<string, string, string>.ItemValue), (Func<string,string>)(x=>x) },
        };
    }

    private List<string> _items = new List<string>()
    {
        "Alabama", "Alaska", "American Samoa", "Arizona",
        "Arkansas", "California", "Colorado", "Connecticut",
        "Delaware", "District of Columbia", "Federated States of Micronesia",
        "Florida", "Georgia", "Guam", "Hawaii", "Idaho",
        "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky",
        "Louisiana", "Maine", "Marshall Islands", "Maryland",
        "Massachusetts", "Michigan", "Minnesota", "Mississippi",
        "Missouri", "Montana", "Nebraska", "Nevada",
        "New Hampshire", "New Jersey", "New Mexico", "New York",
        "North Carolina", "North Dakota", "Northern Mariana Islands", "Ohio",
        "Oklahoma", "Oregon", "Palau", "Pennsylvania", "Puerto Rico",
        "Rhode Island", "South Carolina", "South Dakota", "Tennessee",
        "Texas", "Utah", "Vermont", "Virgin Island", "Virginia",
        "Washington", "West Virginia", "Wisconsin", "Wyoming"
    };
}
