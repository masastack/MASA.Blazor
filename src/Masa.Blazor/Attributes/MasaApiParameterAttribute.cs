namespace Masa.Blazor.Attributes;

public sealed class MasaApiParameterAttribute : Attribute
{
    public object? DefaultValue { get; }

    public bool Ignored { get; set; }

    public string? ReleasedIn { get; set; }

    public MasaApiParameterAttribute(object defaultValue)
    {
        DefaultValue = defaultValue;
    }

    public MasaApiParameterAttribute(object defaultValue, string releasedIn)
    {
        DefaultValue = defaultValue;
        ReleasedIn = releasedIn;
    }

    public MasaApiParameterAttribute()
    {
    }
}
