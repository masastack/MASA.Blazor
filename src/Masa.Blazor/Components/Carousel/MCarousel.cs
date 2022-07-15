using System.Timers;
using Timer = System.Timers.Timer;

namespace Masa.Blazor.Components.Carousel;

public partial class MCarousel : MWindow
{
    [Parameter]
    public bool Continuous { get; set; } = true;

    [Parameter]
    public bool Cycle
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    [Parameter]
    public string DelimiterIcon { get; set; }

    [Parameter]
    public StringNumber Height
    {
        get => GetValue(500);
        set => SetValue(value);
    }

    [Parameter]
    public bool HideDelimiters { get; set; }

    [Parameter]
    public bool HideDelimiterBackground { get; set; }

    [Parameter]
    public int Interval
    {
        get => GetValue(6000);
        set => SetValue(value);
    }

    [Parameter]
    public bool Progress { get; set; }

    [Parameter]
    public string ProgressColor { get; set; }

    /// <summary>
    /// TODO: enum
    /// </summary>
    [Parameter]
    public string VerticalDelimiters { get; set; }

    private bool IsVertical => VerticalDelimiters is not null;

    public override bool ArrowsVisible => !IsVertical && base.ArrowsVisible;

    private StringNumber InternalHeight { get; set; }
    private int SlideTimeout { get; set; }

    private Timer Timer { get; set; }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        Mandatory = true;
        ShowArrows = true;

        return base.SetParametersAsync(parameters);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        InternalHeight = Height;

        if (Timer == null)
        {
            Timer = new Timer
            {
                Interval = Interval > 0 ? Interval : 6000
            };

            Timer.Elapsed += TimerOnElapsed;
        }

        StartTimeout();
    }

    private void TimerOnElapsed(object sender, ElapsedEventArgs e)
    {
        Next();
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider.Merge(cssBuilder =>
        {
            cssBuilder.Add("m-carousel")
                      .AddIf("m-carousel--hide-delimiter-background", () => HideDelimiterBackground)
                      .AddIf("m-carousel--vertical-delimiters", () => IsVertical);
        });
    }

    protected override void OnWatcherInitialized()
    {
        base.OnWatcherInitialized();

        Watcher.Watch<int>(nameof(Interval), RestartTimeout)
               .Watch<StringNumber>(nameof(Height), (val, oldVal) =>
               {
                   Console.WriteLine($"val:{val} oldVal:{oldVal}");
                   if (string.IsNullOrWhiteSpace(val.ToString()))
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
                       Timer.Stop();
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

    private void RestartTimeout()
    {
        Timer.Stop();
        StateHasChanged();
        Timer.Start();
    }

    private void StartTimeout()
    {
        if (!Cycle) return;

        Timer.Start();
    }
}
