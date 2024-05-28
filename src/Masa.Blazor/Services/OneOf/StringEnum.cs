namespace BlazorComponent;

[GenerateOneOf]
public partial class StringEnum<T> : OneOfBase<string, T> where T : Enum
{
    public override string? ToString()
    {
        return Value?.ToString();
    }

    public static bool operator ==(StringEnum<T>? left, StringEnum<T>? right)
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

    public static bool operator !=(StringEnum<T>? left, StringEnum<T>? right)
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
}