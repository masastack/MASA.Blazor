namespace Masa.Blazor;

public partial class MI18n : MasaComponentBase
{
    [Inject] private I18n I18n { get; set; } = null!;

    [Parameter] public string? Key { get; set; }

    [Parameter] public RenderFragment<int>? PlaceholderContent { get; set; }

    [Parameter] public object[]? Args { get; set; }

    private List<I18nValueSegment> _segments = new();
    private string? _value;

    private string? _prevKey;
    private CultureInfo? _prevCulture;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        I18n.CultureChanged += I18nOnCultureChanged;
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-i18n";
    }

    private void I18nOnCultureChanged(object? sender, EventArgs e)
    {
        InvokeAsync(() =>
        {
            AnalyzeI18nValue();
            StateHasChanged();
        });
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        AnalyzeI18nValue();
    }

    private void AnalyzeI18nValue()
    {
        if (_prevKey == Key && Equals(_prevCulture, I18n.Culture)) return;

        _prevKey = Key;
        _prevCulture = I18n.Culture;

        if (Key != null)
        {
            if (PlaceholderContent is null)
            {
                var args = Args ?? Array.Empty<object>();
                _value = I18n.T(Key, args: args);
            }
            else
            {
                var value = I18n.T(Key);
                _segments = GetSegments(value);
            }
        }
        else
        {
            _segments.Clear();
            _value = null;
        }
    }

    private List<I18nValueSegment> GetSegments(string value)
    {
        var startIndex = 0;

        var segments = new List<I18nValueSegment>();

        var regex = new Regex("{[0-9]+?}");
        var matches = regex.Matches(value);

        foreach (Match match in matches)
        {
            var prev = value[startIndex..match.Index];

            segments.Add(new I18nValueSegment(prev));

            var index = Convert.ToInt32(match.Value.TrimStart('{').TrimEnd('}')); // {0} -> 0, {1} -> 1
            segments.Add(new I18nValueSegment(match.Value, index));

            startIndex = match.Index + match.Value.Length;
        }

        if (value.Length - 1 > startIndex)
        {
            var rest = value[startIndex..];
            segments.Add(new I18nValueSegment(rest));
        }

        return segments;
    }

    private record I18nValueSegment(string Text, int PlaceholderIndex = -1);

    protected override async ValueTask DisposeAsyncCore()
    {
        I18n.CultureChanged -= I18nOnCultureChanged;
        await base.DisposeAsyncCore();
    }
}