namespace Masa.Blazor;

[GenerateOneOf]
public partial class BooleanNumber : OneOfBase<bool, int>
{
    public bool IsBoolean() => Match(
        t0 => true,
        t1 => false
    );

    public bool IsNumber() => Match(
        t0 => false,
        t1 => true
    );
}