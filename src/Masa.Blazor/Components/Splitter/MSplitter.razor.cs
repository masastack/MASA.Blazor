using System.Collections.ObjectModel;

namespace Masa.Blazor;

public partial class MSplitter
{
    [CascadingParameter(Name = "IsDark")]
    public bool CascadingIsDark { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool Vertical { get; set; }

    private readonly DelayTask _delayTask = new();
    private readonly Collection<MSplitterPane> _panes = new();

    private List<double> _paneSizes = new();
    private BoundingClientRect? _containerRect;
    private bool _mousedown;
    private int _activeIndex;

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .UseBem("m-splitter", css =>
            {
                css.Modifiers(m =>
                    m.Modifier("horizontal", !Vertical).Add(Vertical)).AddTheme(CascadingIsDark);
            })
            .Element("pane", css =>
            {
                if (css.Data is not int index) return;

                var pane = _panes[index];
                css.Add(pane.Class);
            }, style =>
            {
                if (style.Data is not int index) return;

                var pane = _panes[index];
                var size = _paneSizes[index] + "%";

                if (Vertical)
                {
                    style.AddHeight(size);
                }
                else
                {
                    style.AddWidth(size);
                }

                style.Add(pane.Style);
            })
            .Element("bar");
    }

    internal async Task RegisterAsync(MSplitterPane pane)
    {
        _panes.Add(pane);

        await _delayTask.Run(() =>
        {
            _paneSizes = _panes.Select(_ => 100d / _panes.Count).ToList();
            return InvokeAsync(StateHasChanged);
        });
    }

    private async Task BindEvents()
    {
        await Js.AddHtmlElementEventListener<MouseEventArgs>("document", "mousemove", HandleMousemove, new EventListenerOptions()
        {
            Passive = false
        });

        await Js.AddHtmlElementEventListener<MouseEventArgs>("document", "mouseup", HandleMouseup, new EventListenerOptions()
        {
            Passive = false
        });
    }

    private async Task HandleMousedown(MouseEventArgs args, int index)
    {
        await BindEvents();

        _mousedown = true;
        _activeIndex = index;
        _containerRect = await Js.InvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, Ref);
    }

    private async Task HandleMousemove(MouseEventArgs args)
    {
        if (_containerRect is null || !_mousedown)
        {
            return;
        }

        Console.Out.WriteLine("rect width = {0}", args.ClientX);

        var x = args.ClientX - _containerRect.Left;
        var y = args.ClientY - _containerRect.Top;
        Console.Out.WriteLine("x = {0}", x);
        Console.Out.WriteLine("y = {0}", y);
        Console.Out.WriteLine("_activeIndex = {0}", _activeIndex);

        var currentIndex = _activeIndex;
        var nextIndex = _activeIndex + 1;

        double frontPaneSize = 0;
        if (currentIndex > 0)
        {
            frontPaneSize = _paneSizes.Take(currentIndex).Sum();
        }

        var prevPaneSize = _paneSizes[currentIndex];
        var prevNextPaneSize = _paneSizes[nextIndex];

        _paneSizes[currentIndex] = Math.Round(x / _containerRect.Width * 100, 2);
        _paneSizes[nextIndex] = (prevPaneSize + prevNextPaneSize) - _paneSizes[currentIndex];

        Console.Out.WriteLine("sizes = {0}", string.Join(",", _paneSizes));

        StateHasChanged();

        // _paneSizes[0]++;
        // _paneSizes[1]--;
    }

    private async Task HandleMouseup(MouseEventArgs args)
    {
        if (!_mousedown)
        {
            return;
        }

        _mousedown = false;
        Console.Out.WriteLine("Mouseup");
    }
}
