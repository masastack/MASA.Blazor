namespace BlazorComponent;

[GenerateOneOf]
public partial class StringNumber : OneOfBase<string, int, double>
{
    public (bool isNumber, double number) TryGetNumber() =>
        Match(
            s => (double.TryParse(s, out var n), n),
            i => (true, i),
            d => (true, d)
        );

    public int ToInt32() => Match(
        t0 =>
        {
            string[] strs = t0.Split("px");
            return int.TryParse(strs[0], out var val) ? val : 0;
        },
        t1 => t1,
        t2 => Convert.ToInt32(t2)
    );

    public double ToDouble() => Match(
        t0 =>
        {
            string[] strs = t0.Split("px");
            return double.TryParse(strs[0], out var val) ? val : 0D;
        },
        t1 => Convert.ToDouble(t1),
        t2 => t2
    );

    public override string? ToString()
    {
        return Value?.ToString();
    }

    public static bool operator ==(StringNumber? left, StringNumber? right)
    {
        // if left is not null but its value is null.
        // for example `left == null` should be true
        if (right is null && left is { IsT0: true, AsT0: null })
        {
            return true;
        }

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

    public static bool operator !=(StringNumber? left, StringNumber? right)
    {
        // if left is not null but its value is null.
        // for example `left != null` should be false
        if (right is null && left is { IsT0: true, AsT0: null })
        {
            return false;
        }

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