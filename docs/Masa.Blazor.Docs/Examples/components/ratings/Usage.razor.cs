namespace Masa.Blazor.Docs.Examples.components.ratings;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MRating.HalfIncrements), new CheckboxParameter("false",true) },
        { nameof(MRating.Hover), new CheckboxParameter("false",true) },
        { nameof(MRating.Readonly), new CheckboxParameter("false",true) },
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MRating.Length), new SliderParameter(5, 0, 15, false) },
        { nameof(MRating.Size), new SliderParameter(64, 0, 100, false) },
        { nameof(MRating.Value), new SliderParameter(3, 0, 15, false) },
    };

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        { nameof(MRating.Color), new SelectParameter(new List<string>() { "primary", "warning", "green", "red", "blue", "error", "teal","red lighten-3" },"primary") },
        { nameof(MRating.BackgroundColor), new SelectParameter(new List<string>() { "grey lighten-2", "warning lighten-1", "green lighten-2", "red lighten-2", "grey", "#eee", "cyan lighten-2", "grey lighten-1" }) },
        { nameof(MRating.EmptyIcon), new SelectParameter(new List<string>() { "mdi-heart-outline", "mdi-star-outline"},"mdi-heart-outline") },
        { nameof(MRating.FullIcon), new SelectParameter(new List<string>() { "mdi-heart", "mdi-star"},"mdi-star") },
        { nameof(MRating.HalfIcon), new SelectParameter(new List<string>() { "mdi-heart-half-full", "mdi-star-half-full"},"mdi-star-half-full") },

    };

    public Usage() : base(typeof(MRating))
    {
    }

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MRating.Length) => (StringNumber)(double)parameter.Value,
            nameof(MRating.Size) => (StringNumber)(double)parameter.Value,
            _ => parameter.Value
        };
    }
}