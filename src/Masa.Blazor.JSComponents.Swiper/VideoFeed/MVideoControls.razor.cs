namespace Masa.Blazor.Components.VideoFeed;

public partial class MVideoControls
{
    [Parameter] public string? Title { get; set; }

    [Parameter] public string? Subtitle { get; set; }

    [Parameter] public bool Fullscreen { get; set; }

    [Parameter] public VideoMetadata VideoMetadata { get; set; }

    [Parameter] public bool IsPlaying { get; set; }

    [Parameter] public Func<double, Task> UpdateVideoTimeInJS { get; set; }

    [Parameter] public EventCallback OnPauseVideo { get; set; }

    [Parameter] public EventCallback OnPlayVideo { get; set; }

    [Parameter] public RenderFragment? ActionsContent { get; set; }

    [Parameter] public bool Available { get; set; }

    [Parameter] public EventCallback<bool> OnCloseFullscreen { get; set; }

    private static readonly Block _block = new("m-video-feed");
    private ModifierBuilder _controlsModifierBuilder = _block.Element("controls").CreateModifierBuilder();
    private ModifierBuilder _playBtnModifierBuilder = _block.Element("play-btn").CreateModifierBuilder();

    private double _currentTime;
    private bool _move;
    private bool _dragging;
    private CancellationTokenSource _cts = new();

    internal void UpdateTimeInternal(double value)
    {
        if (_move || !IsPlaying)
        {
            return;
        }

        _currentTime = value;
        StateHasChanged();
    }

    private async Task OnStart()
    {
        _move = true;
        UpdateDragging(true);
        await OnPauseVideo.InvokeAsync();
    }

    private async Task OnEnd(double value)
    {
        UpdateDragging(false);
        await UpdateVideoTimeInJS(value);
        await OnPlayVideo.InvokeAsync();
        await Task.Delay(500);
        _move = false;
    }

    private void UpdateDragging(bool value)
    {
        if (value)
        {
            Task.Run(async () =>
            {
                await Task.Delay(500, _cts.Token);
                _dragging = true;
                await InvokeAsync(StateHasChanged);
            });
        }
        else
        {
            _cts.Cancel();
            _cts = new();
            _dragging = false;
        }
    }

    private async Task CloseFullscreen()
    {
        await OnCloseFullscreen.InvokeAsync(false);
    }

    private static string DurationToString(double duration)
    {
        var timeSpan = TimeSpan.FromSeconds(duration);
        if (timeSpan.Hours > 0)
        {
            return $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }

        return $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }

}