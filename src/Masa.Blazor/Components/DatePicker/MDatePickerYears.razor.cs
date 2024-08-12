
namespace Masa.Blazor.Components.DatePicker;

public partial class MDatePickerYears : MasaComponentBase
{
    [Inject] private IntersectJSModule IntersectJSModule { get; set; } = null!;

    [Parameter] public string? Color { get; set; }

    [Parameter] public DateOnly? Min { get; set; }

    [Parameter] public DateOnly? Max { get; set; }

    [Parameter] public int Value { get; set; }

    // for issue #2097
    [Parameter] public DateOnly TableDate { get; set; }

    [Parameter] public EventCallback<int> OnInput { get; set; }

    [Parameter] public EventCallback<int> OnYearClick { get; set; }

    [Parameter] public Func<DateOnly, string>? Format { get; set; }

    [Parameter] public CultureInfo Locale { get; set; } = null!;

    [Parameter] public DatePickerType Type { get; set; }

    private Func<DateOnly, string> Formatter
    {
        get
        {
            if (Format != null)
            {
                return Format;
            }


            return DateFormatters.Year(Locale);
        }
    }

    private async Task HandleOnYearItemClickAsync(int year)
    {
        await OnInput.InvokeAsync(year);
        await OnYearClick.InvokeAsync(year);

        if (Type == DatePickerType.Year)
        {
            _ = ScrollToActiveElementAsync(true);
        }
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-date-picker-years";
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            var handle = DotNetObjectReference.Create(new IntersectInvoker(_ => ScrollToActiveElementAsync()));
            await IntersectJSModule.ObserverAsync(Ref, handle);
        }
    }
    
    private async Task ScrollToActiveElementAsync(bool smooth = false)
    {
        await Js.InvokeVoidAsync(JsInteropConstants.ScrollToActiveElement, Ref, ".active", "center", smooth);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        await IntersectJSModule.UnobserveAsync(Ref);
    }
}