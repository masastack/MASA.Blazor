using System.Timers;
using Timer = System.Timers.Timer;

namespace Masa.Blazor;

public partial class MCarousel : MWindow, ICarousel
{
    [Parameter]
    public bool Cycle
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    [Parameter]
    public string? DelimiterIcon { get; set; }

    [Parameter]
    [MassApiParameter(500)]
    public StringNumber? Height
    {
        get => GetValue((StringNumber)500);
        set => SetValue(value);
    }

    [Parameter]
    public bool HideDelimiters { get; set; }

    [Parameter]
    public bool HideDelimiterBackground { get; set; }

    [Parameter]
    [MassApiParameter(6000)]
    public int Interval
    {
        get => GetValue(6000);
        set => SetValue(value);
    }

    [Parameter]
    public bool Progress { get; set; }

    [Parameter]
    public string? ProgressColor { get; set; }

    /// <summary>
    /// TODO: enum
    /// </summary>
    [Parameter]
    public string? VerticalDelimiters { get; set; }

    private Timer? _timer;

    public override bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            return !Light;
        }
    }

    public override bool ArrowsVisible => !IsVertical && base.ArrowsVisible;

    private bool IsVertical => VerticalDelimiters is not null;

    public double ProgressValue => Items.Count > 0 ? (InternalIndex + 1d) / Items.Count * 100 : 0;

    public StringNumber? InternalHeight { get; private set; }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        Continuous = true;
        Mandatory = true;
        ShowArrows = true;

        return base.SetParametersAsync(parameters);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        InternalHeight = Height;

        if (_timer == null)
        {
            _timer = new Timer
            {
                Interval = Interval > 0 ? Interval : 6000
            };

            _timer.Elapsed += TimerOnElapsed;
        }

        StartTimeout();
    }

    private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        InvokeAsync(() =>
        {
            Next();
            StateHasChanged();
        });
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Merge(cssBuilder =>
            {
                cssBuilder.Add("m-carousel")
                          .AddIf("m-carousel--hide-delimiter-background", () => HideDelimiterBackground)
                          .AddIf("m-carousel--vertical-delimiters", () => IsVertical);
            }, styleBuilder => { styleBuilder.AddHeight(InternalHeight); })
            .Apply("controls", cssBuilder => { cssBuilder.Add("m-carousel__controls"); }, styleBuilder =>
            {
                styleBuilder
                    .Add(() => $"left: {(VerticalDelimiters == "left" && IsVertical ? "0" : "auto")}")
                    .Add(() => $"right: {(VerticalDelimiters == "right" ? "0" : "auto")}");
            });

        AbstractProvider
            .Apply(typeof(BCarouselDelimiters<>), typeof(BCarouselDelimiters<MCarousel>))
            .Apply(typeof(BCarouselProgress<>), typeof(BCarouselProgress<MCarousel>))
            .Apply(typeof(BItemGroup), typeof(MItemGroup))
            .Apply<BButton, MButton>("controls-item", attrs =>
            {
                attrs[nameof(MButton.Class)] = "m-carousel__controls__item";
                attrs[nameof(MButton.Icon)] = true;
                attrs[nameof(MButton.Small)] = true;
            })
            .Apply<BIcon, MIcon>("controls-item", attrs => { attrs[nameof(MIcon.Size)] = (StringNumber)18; })
            .Apply<BProgressLinear, MProgressLinear>();
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<StringNumber>(nameof(Value), RestartTimeout)
               .Watch<int>(nameof(Interval), RestartTimeout)
               .Watch<StringNumber>(nameof(Height), (val) =>
               {
                   if (string.IsNullOrWhiteSpace(val?.ToString()))
                   {
                       return;
                   }

                   InternalHeight = val;
               })
               .Watch<bool>(nameof(Cycle), val =>
               {
                   if (val)
                   {
                       RestartTimeout();
                   }
                   else
                   {
                       _timer?.Stop();
                   }

                   StateHasChanged();
               });
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Interval < 0)
        {
            Interval = 6000;
        }
    }

    public async Task InternalValueChanged(StringNumber? val)
    {
        await ToggleAsync(val);
    }

    private void RestartTimeout()
    {
        if (_timer is null) return;

        _timer.Stop();
        StateHasChanged();
        StartTimeout();
    }

    private void StartTimeout()
    {
        if (!Cycle) return;

        _timer?.Start();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (_timer is not null)
        {
            _timer.Elapsed -= TimerOnElapsed;
        }
    }
}
