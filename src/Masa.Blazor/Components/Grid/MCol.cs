using Masa.Blazor.Extensions;

namespace Masa.Blazor;

public class MCol : Container
{
    [Parameter] [MasaApiParameter("div")] public string Tag { get; set; } = "div";

    [Parameter] public StringNumber? Sm { get; set; }

    [Parameter] public StringNumber? Md { get; set; }

    [Parameter] public StringNumber? Lg { get; set; }

    [Parameter] public StringNumber? Xl { get; set; }

    /// <summary>
    /// 'auto', 'start', 'end', 'center', 'baseline', 'stretch'
    /// </summary>
    [Parameter]
    public StringEnum<AlignTypes>? Align { get; set; }

    [Parameter] public StringNumber? OrderLg { get; set; }

    [Parameter] public StringNumber? OrderMd { get; set; }

    [Parameter] public StringNumber? OrderSm { get; set; }

    [Parameter] public StringNumber? OrderXl { get; set; }

    [Parameter] public StringNumber? OffsetLg { get; set; }

    [Parameter] public StringNumber? OffsetMd { get; set; }

    [Parameter] public StringNumber? OffsetSm { get; set; }

    [Parameter] public StringNumber? OffsetXl { get; set; }

    [Parameter] public StringNumber? Flex { get; set; }

    [Parameter] public StringNumber? Cols { get; set; }

    [Parameter] public StringNumber? Order { get; set; }

    [Parameter] public StringNumber? Offset { get; set; }

    private static readonly Regex FlexStyleRegex = new(@"^(\d+(?:\.\d+)?)(px|em|rem|%)?$", RegexOptions.Compiled);

    protected override string TagName => Tag;

    protected override IEnumerable<string> BuildComponentClass()
    {
        if (Cols == null || (Sm == null && Md == null && Lg == null && Xl == null))
        {
            yield return "col";
        }

        if (Cols != null)
        {
            yield return $"col-{Cols.Value}";
        }

        if (Sm != null)
        {
            yield return $"col-sm-{Sm.Value}";
        }

        if (Md != null)
        {
            yield return $"col-md-{Md.Value}";
        }

        if (Lg != null)
        {
            yield return $"col-lg-{Lg.Value}";
        }

        if (Xl != null)
        {
            yield return $"col-xl-{Xl.Value}";
        }

        if (Offset != null)
        {
            yield return $"offset-{Offset.Value}";
        }

        if (OffsetLg != null)
        {
            yield return $"offset-lg-{OffsetLg}";
        }

        if (OffsetMd != null)
        {
            yield return $"offset-md-{OffsetMd}";
        }

        if (OffsetSm != null)
        {
            yield return $"offset-sm-{OffsetSm}";
        }

        if (OffsetXl != null)
        {
            yield return $"offset-xl-{OffsetXl}";
        }

        if (Order != null)
        {
            yield return $"order-{Order.Value}";
        }

        if (OrderLg != null)
        {
            yield return $"order-lg-{OrderLg}";
        }

        if (OrderMd != null)
        {
            yield return $"order-md-{OrderMd}";
        }

        if (OrderSm != null)
        {
            yield return $"order-sm-{OrderSm}";
        }

        if (OrderXl != null)
        {
            yield return $"order-xl-{OrderXl}";
        }

        if (Align != null)
        {
            yield return Align.ToString(() => $"align-self-{Align}",
                ("align-self-auto", AlignTypes.Auto),
                ("align-self-start", AlignTypes.Start),
                ("align-self-center", AlignTypes.Center),
                ("align-self-end", AlignTypes.End),
                ("align-self-baseline", AlignTypes.Baseline),
                ("align-self-stretch", AlignTypes.Stretch));
        }

        if (Flex != null)
        {
            yield return SetHostFlexStyle();
        }
    }

    private string SetHostFlexStyle()
    {
        return Flex!.Match(
            str => FlexStyleRegex.IsMatch(str) ? $"flex: 0 0 {str}" : $"flex: {str}",
            num => $"flex: {num} {num} auto",
            _ => string.Empty);
    }
}