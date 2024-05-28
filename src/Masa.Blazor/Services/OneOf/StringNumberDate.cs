using System.Text.RegularExpressions;

namespace BlazorComponent;

[GenerateOneOf]
public partial class StringNumberDate : OneOfBase<string, int, DateTime>
{
    private const string parseRegex = @"^(\d{4})-(\d{1,2})(-(\d{1,2}))?([^\d]+(\d{1,2}))?(:(\d{1,2}))?(:(\d{1,2}))?$";

    public static bool operator ==(StringNumberDate left, StringNumberDate right)
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

    public static bool operator !=(StringNumberDate left, StringNumberDate right)
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

    public int ToInt32() => Match(
        t0 => Convert.ToInt32(t0),
        t1 => t1,
        t2 => 0
    );

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

    public bool ValidateTimestamp() => Match(
        t0 => Regex.Match(t0, parseRegex).Success,
        t1 => true,
        t2 => true
    );

    public DateTime ToDateTime() => Match(
        t0 => Regex.Match(t0, parseRegex).Success ? Convert.ToDateTime(t0) : default,
        t1 => Convert.ToDateTime(t1),
        t2 => t2
    );

    public bool IsTimedless() => Match(
        t0 => false,
        t1 => true,
        t2 => true
    );
}