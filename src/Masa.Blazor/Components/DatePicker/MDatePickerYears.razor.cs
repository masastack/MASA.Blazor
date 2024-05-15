namespace Masa.Blazor.Components.DatePicker;

public partial class MDatePickerYears : MasaComponentBase
{
    [Inject] private IntersectJSModule IntersectJSModule { get; set; } = null!;

    [Parameter] public string? Color { get; set; }

    [Parameter] public DateOnly? Min { get; set; }

    [Parameter] public DateOnly? Max { get; set; }

    [Parameter] public int Value { get; set; }

    [Parameter] public EventCallback<int> OnInput { get; set; }

    [Parameter] public Func<DateOnly, string>? Format { get; set; }

    [Parameter] public CultureInfo Locale { get; set; } = null!;

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
        if (OnInput.HasDelegate)
        {
            await OnInput.InvokeAsync(year);
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
            var handle = DotNetObjectReference.Create(new IntersectInvoker(OnIntersectAsync));
            await IntersectJSModule.ObserverAsync(Ref, handle);
        }
    }

    private async Task OnIntersectAsync(IntersectEventArgs arg)
    {
        await Js.InvokeVoidAsync(JsInteropConstants.ScrollToActiveElement, Ref);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        await IntersectJSModule.UnobserveAsync(Ref);
    }
}