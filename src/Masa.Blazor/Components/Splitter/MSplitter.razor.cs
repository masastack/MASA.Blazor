using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Components.Forms;

namespace Masa.Blazor;

public partial class MSplitter
{
    [CascadingParameter(Name = "IsDark")]
    public bool CascadingIsDark { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool Vertical { get; set; }

    private readonly DelayTask _delayTask = new();
    private readonly Collection<MSplitterPane> _panes = new();

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
                // TODO: pane
                
                if (style.Data is not int index) return;

                var pane = _panes[index];
                var size = pane.Size + "%";

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
            var initSize = 100d / _panes.Count;

            _panes.ForEach(p => { p.Size = initSize; });

            return InvokeAsync(StateHasChanged);
        });
    }

    private async Task BindEvents()
    {
        await Js.AddHtmlElementEventListener<MouseEventArgs>("document", "mousemove", HandleMousemove, new EventListenerOptions()
        {
            Passive = false
        }, new EventListenerExtras(0, throttle: 16));

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

        var x = args.ClientX - _containerRect.Left;
        var y = args.ClientY - _containerRect.Top;
        Console.Out.WriteLine("x = {0}", x);
        Console.Out.WriteLine("y = {0}", y);
        Console.Out.WriteLine("_activeIndex = {0}", _activeIndex);

        double dragDistance = 0;

        if (Vertical)
        {
        }
        else
        {
            dragDistance = x;
        }

        // TODO: RTL

        var dragPercentage2 = dragDistance * 100 / _containerRect.Width;

        var prevPanesSize = _panes.Take(_activeIndex).Sum(u => u.Size);
        var nextPanesSize = _panes.Skip(_activeIndex + 2).Sum(u => u.Size);
        var secondNextPanesSize = _panes.Skip(_activeIndex + 2).Sum(u => u.Size);

        var minDrag = prevPanesSize;
        var maxDrag = 100 - nextPanesSize;

        var dragPercentage = Math.Max(Math.Min(dragPercentage2, maxDrag), minDrag);


        var currentIndex = _activeIndex;
        var nextIndex = _activeIndex + 1;
        var paneBefore = _panes[currentIndex];
        var paneAfter = _panes[nextIndex];

        var paneBeforeMaxReached = paneBefore.Max < 100 && (dragPercentage >= paneBefore.Max + prevPanesSize);
        var paneAfterMaxReached = paneAfter.Max < 100 && (dragPercentage <= 100 - (paneAfter.Max + secondNextPanesSize));

        if (paneBeforeMaxReached || paneAfterMaxReached)
        {
            if (paneBeforeMaxReached)
            {
                paneBefore.Size = paneBefore.Max;
                paneAfter.Size = Math.Max(100 - paneBefore.Max - prevPanesSize - nextPanesSize, 0);
            }
            else
            {
                paneBefore.Size = Math.Max(100 - paneAfter.Max - prevPanesSize - secondNextPanesSize, 0);
                paneAfter.Size = paneAfter.Max;
            }
        }
        else
        {
            paneBefore.Size = Math.Min(Math.Max(dragPercentage - prevPanesSize, paneBefore.Min), paneBefore.Max);
            paneAfter.Size = Math.Min(Math.Max(100 - dragPercentage - nextPanesSize, paneAfter.Min), paneAfter.Max);
        }

        Console.Out.WriteLine("sizes = {0}", string.Join(",", _panes.Select(u => u.Size)));
        StateHasChanged();
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
