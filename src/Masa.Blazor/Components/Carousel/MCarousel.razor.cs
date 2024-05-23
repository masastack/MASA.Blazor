using System.Timers;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;
using Timer = System.Timers.Timer;

namespace Masa.Blazor;

public partial class MCarousel : MWindow
{
    [Parameter]
    public bool Cycle
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    [Parameter]
    [MasaApiParameter("$delimiter")]
    public string? DelimiterIcon { get; set; } = "$delimiter";

    [Parameter]
    [MasaApiParameter(500)]
    public StringNumber? Height
    {
        get => GetValue((StringNumber)500);
        set => SetValue(value);
    }

    [Parameter] public bool HideDelimiters { get; set; }

    [Parameter] public bool HideDelimiterBackground { get; set; }

    [Parameter]
    [MasaApiParameter(6000)]
    public int Interval
    {
        get => GetValue(6000);
        set => SetValue(value);
    }

    [Parameter] public bool Progress { get; set; }

    [Parameter] public string? ProgressColor { get; set; }

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

    private Block _block = new("m-carousel");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            _block.Modifier(HideDelimiterBackground)
                .And("vertical-delimiters", IsVertical)
                .GenerateCssClasses()
        );
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return base.BuildComponentStyle().Concat(
            StyleBuilder.Create().AddHeight(InternalHeight).GenerateCssStyles()
        );
    }

    private string GetControlsClass() => _block.Element("controls").Build();

    private string GetControlsStyle() => StyleBuilder.Create()
        .Add("left", VerticalDelimiters == "left" && IsVertical ? "0" : "auto")
        .Add("right", VerticalDelimiters == "right" ? "0" : "auto")
        .Build();

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

    protected override ValueTask DisposeAsyncCore()
    {
        if (_timer is not null)
        {
            _timer.Elapsed -= TimerOnElapsed;
        }

        return base.DisposeAsyncCore();
    }
}