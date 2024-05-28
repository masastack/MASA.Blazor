namespace BlazorComponent;

[GenerateOneOf]
public partial class StringBoolean : OneOfBase<string, bool>
{
    public static bool operator ==(StringBoolean? left, StringBoolean? right)
    {
        if (Equals(left, right))
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Value == right.Value;
    }

    public static bool operator !=(StringBoolean? left, StringBoolean? right)
    {
        if (Equals(left, right))
        {
            return false;
        }

        if (left is null || right is null)
        {
            return true;
        }

        return left.Value != right.Value;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string? ToString()
    {
        return Convert.ToString(Value);
    }

    public bool IsString() => Match(
        t0 => true,
        t1 => false
    );
}
