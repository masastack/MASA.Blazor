using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Components.Forms;

namespace Masa.Blazor;

public partial class MSplitter
{
    [CascadingParameter(Name = "IsDark")]
    public bool CascadingIsDark { get; set; }

    [Parameter] [ApiDefaultValue(8)] public int BarSize { get; set; } = 8;

    [Parameter] public RenderFragment? BarContent { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] [ApiDefaultValue(true)] public bool PushOtherPanes { get; set; } = true;

    [Parameter] public bool Row { get; set; }

    private readonly DelayTask _delayTask = new();
    private readonly Collection<MSplitterPane> _panes = new();

    private BoundingClientRect? _containerRect;
    private bool _mousedown;
    private int _activeIndex;
    private bool _eventsBound;

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .UseBem("m-splitter", css =>
            {
                css.Modifiers(m =>
                    m.Modifier("column", !Row).Add(Row)).AddTheme(CascadingIsDark);
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
                var size = Math.Round(pane.InternalSize, 2, MidpointRounding.ToZero) + "%";

                if (Row)
                {
                    style.AddHeight(size);
                }
                else
                {
                    style.AddWidth(size);
                }

                style.Add(pane.Style);
            })
            .Element("bar", _ => { }, style =>
            {
                if (Row)
                {
                    style.AddMinHeight(BarSize);
                }
                else
                {
                    style.AddMinWidth(BarSize);
                }
            });
    }

    internal async Task RegisterAsync(MSplitterPane pane)
    {
        var index = _panes.Count;
        pane.InternalIndex = index;

        _panes.Add(pane);

        await _delayTask.Run(() =>
        {
            InitialPanesSizing();

            return InvokeAsync(StateHasChanged);
        });
    }

    internal async Task UnregisterAsync(MSplitterPane pane)
    {
        _panes.Remove(pane);
        StateHasChanged();
        await Task.CompletedTask;
    }

    private void InitialPanesSizing()
    {
        var leftToAllocate = 100d;
        List<MSplitterPane> ungrowable = new();
        List<MSplitterPane> unshrinkable = new();
        var definedSizes = 0;

        foreach (var pane in _panes)
        {
            leftToAllocate -= pane.Size;

            if (pane.Size != 0)
            {
                definedSizes++;
                pane.InternalSize = pane.Size;
            }

            if (pane.Size >= pane.Max) ungrowable.Add(pane);
            if (pane.Size <= pane.Min) unshrinkable.Add(pane);
        }

        var leftToAllocate2 = 100d;
        if (leftToAllocate > 0.1)
        {
            foreach (var pane in _panes)
            {
                if (pane.Size == 0)
                {
                    pane.InternalSize = Math.Max(Math.Min(leftToAllocate / (_panes.Count - definedSizes), pane.Max), pane.Min);
                    leftToAllocate2 -= pane.InternalSize;
                }
            }

            if (leftToAllocate2 > 0.1)
            {
                ReadjustSizes(leftToAllocate, ungrowable, unshrinkable);
            }
        }
    }

    private void ReadjustSizes(double leftToAllocate, List<MSplitterPane> ungrowable, List<MSplitterPane> unshrinkable)
    {
        double equalSizeToAllocate;
        if (leftToAllocate > 0)
        {
            equalSizeToAllocate = leftToAllocate / (_panes.Count - ungrowable.Count);
        }
        else
        {
            equalSizeToAllocate = leftToAllocate / (_panes.Count - unshrinkable.Count);
        }

        foreach (var pane in _panes)
        {
            if (leftToAllocate > 0 && !ungrowable.Contains(pane))
            {
                var newPaneSize = Math.Max(Math.Min(pane.InternalSize + equalSizeToAllocate, pane.Max), pane.Min);
                var allocated = newPaneSize - pane.InternalSize;
                leftToAllocate -= allocated;
                pane.InternalSize = newPaneSize;
            }
            else if (!unshrinkable.Contains(pane))
            {
                var newPaneSize = Math.Max(Math.Min(pane.InternalSize + equalSizeToAllocate, pane.Max), pane.Min);
                var allocated = newPaneSize - pane.InternalSize;
                leftToAllocate -= allocated;
                pane.InternalSize = newPaneSize;
            }
        }

        if (Math.Abs(leftToAllocate) > 0.1)
        {
            // warn
        }
    }

    private async Task BindEvents()
    {
        await Js.AddHtmlElementEventListener<MouseEventArgs>("document", "mousemove", HandleMousemove, new EventListenerOptions()
        {
            Passive = false
        }, new EventListenerExtras
        {
            Throttle = 16,
            PreventDefault = true
        });

        await Js.AddHtmlElementEventListener<MouseEventArgs>("document", "mouseup", HandleMouseup, new EventListenerOptions()
        {
            Passive = false
        });
    }

    private async Task UnbindEvents()
    {
        try
        {
            // TODO: check if events was deleted
            
            await Js.RemoveHtmlElementEventListener("document", "mousemove");
            await Js.RemoveHtmlElementEventListener("document", "mouseup");
        }
        catch (JSDisconnectedException)
        {
            // ignored
        }
        catch (InvalidOperationException)
        {
            // ignored
        }
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

        var splitterIndex = _activeIndex;
        var secondNextPanesSize = _panes.Skip(_activeIndex + 3).Sum(u => u.InternalSize);

        var sums = (
            prevPanesSize: _panes.Take(_activeIndex).Sum(u => u.InternalSize),
            nextPanesSize: _panes.Skip(_activeIndex + 2).Sum(u => u.InternalSize),
            prevReachedMinPanes: 0d,
            nextReachedMinPanes: 0d);

        var minDrag = PushOtherPanes ? 0 : sums.prevPanesSize;
        var maxDrag = 100 - (PushOtherPanes ? 0 : sums.nextPanesSize);

        var dragPercentage = Math.Max(Math.Min(CalculateDragPercentage(args), maxDrag), minDrag);

        var panesToResize = new int?[] { splitterIndex, splitterIndex + 1 };

        var paneBefore = _panes[panesToResize[0]!.Value];
        var paneAfter = _panes[panesToResize[1]!.Value];

        var paneBeforeMaxReached = paneBefore.Max < 100 && (dragPercentage >= paneBefore.Max + sums.prevPanesSize);
        var paneAfterMaxReached = paneAfter.Max < 100 && (dragPercentage <= 100 - (paneAfter.Max + secondNextPanesSize));

        if (paneBeforeMaxReached || paneAfterMaxReached)
        {
            if (paneBeforeMaxReached)
            {
                paneBefore.InternalSize = paneBefore.Max;
                paneAfter.InternalSize = Math.Max(100 - paneBefore.Max - sums.prevPanesSize - sums.nextPanesSize, 0);
            }
            else
            {
                paneBefore.InternalSize = Math.Max(100 - paneAfter.Max - sums.prevPanesSize - secondNextPanesSize, 0);
                paneAfter.InternalSize = paneAfter.Max;
            }
        }
        else
        {
            if (PushOtherPanes)
            {
                var res = DoPushOtherPanes(sums, dragPercentage);
                if (res is null)
                {
                    return;
                }

                sums = res.Value.Item1;
                panesToResize = res.Value.Item2;

                if (panesToResize[0].HasValue)
                {
                    paneBefore = _panes[panesToResize[0]!.Value];
                }

                if (panesToResize[1].HasValue)
                {
                    paneAfter = _panes[panesToResize[1]!.Value];
                }
            }

            paneBefore.InternalSize = Math.Min(Math.Max(dragPercentage - sums.prevPanesSize - sums.prevReachedMinPanes, paneBefore.Min),
                paneBefore.Max);
            paneAfter.InternalSize = Math.Min(Math.Max(100 - dragPercentage - sums.nextPanesSize - sums.nextReachedMinPanes, paneAfter.Min),
                paneAfter.Max);
        }

        StateHasChanged();

        await Task.CompletedTask;
    }

    private async Task HandleMouseup(MouseEventArgs args)
    {
        _mousedown = false;
        await Task.Delay(100);
        await UnbindEvents();
    }

    private((double prevPanesSize, double nextPanesSize, double prevReachedMinPanes, double nextReachedMinPanes), int?[])? DoPushOtherPanes(
        (double prevPanesSize, double nextPanesSize, double prevReachedMinPanes, double nextReachedMinPanes) sums,
        double dragPercentage)
    {
        var splitterIndex = _activeIndex;
        var panesToResize = new int?[] { splitterIndex, splitterIndex + 1 };
        if (dragPercentage < sums.prevPanesSize + _panes[panesToResize[0]!.Value].Min)
        {
            panesToResize[0] = _panes.Reverse().First(p => p.InternalIndex < splitterIndex && p.InternalSize > p.Min)?.InternalIndex;

            sums.prevReachedMinPanes = 0;

            if (panesToResize[0].HasValue)
            {
                if (panesToResize[0]!.Value < splitterIndex)
                {
                    for (int i = 0; i < _panes.Count; i++)
                    {
                        var pane = _panes[i];

                        if (i > panesToResize[0]!.Value && i <= splitterIndex)
                        {
                            pane.InternalSize = pane.Min;
                            sums.prevReachedMinPanes += pane.Min;
                        }
                    }
                }
            }

            sums.prevPanesSize = panesToResize[0].HasValue ? SumPrevPanesSize(panesToResize[0]!.Value) : 0;

            if (!panesToResize[0].HasValue)
            {
                sums.prevReachedMinPanes = 0;
                _panes[0].InternalSize = _panes[0].Min;
                for (int i = 0; i < _panes.Count; i++)
                {
                    var pane = _panes[i];
                    if (i > 0 && i <= splitterIndex)
                    {
                        pane.InternalSize = pane.Size;
                        sums.prevReachedMinPanes += pane.Min;
                    }
                }

                _panes[panesToResize[1]!.Value].Size = 100 - sums.prevReachedMinPanes - _panes[0].Min - sums.prevPanesSize - sums.nextPanesSize;

                return null;
            }
        }

        Console.Out.WriteLine($"{dragPercentage} {sums.nextPanesSize} {_panes[panesToResize[1].Value].Min}");
        if (dragPercentage > 100 - sums.nextPanesSize - _panes[panesToResize[1]!.Value].Min)
        {
            panesToResize[1] = _panes.FirstOrDefault(p => p.InternalIndex > splitterIndex + 1 && p.InternalSize > p.Min)?.InternalIndex;
            Console.Out.WriteLine("panesToResize[1] = {0}", panesToResize[1]);
            sums.nextReachedMinPanes = 0;

            if (panesToResize[1].HasValue)
            {
                if (panesToResize[1] > splitterIndex + 1)
                {
                    for (int i = 0; i < _panes.Count; i++)
                    {
                        var pane = _panes[i];
                        if (i > splitterIndex && i < panesToResize[1]!.Value)
                        {
                            pane.InternalSize = pane.Min;
                            sums.nextReachedMinPanes += pane.Min;
                        }
                    }
                }
            }

            sums.nextPanesSize = panesToResize[1].HasValue ? SumPrevPanesSize(panesToResize[1]!.Value - 1) : 0;

            if (!panesToResize[1].HasValue)
            {
                sums.nextReachedMinPanes = 0;
                _panes.Last().InternalSize = _panes.Last().Min;

                for (int i = 0; i < _panes.Count; i++)
                {
                    var pane = _panes[i];
                    if (i < _panes.Count - 1 && i >= splitterIndex + 1)
                    {
                        pane.InternalSize = pane.Min;
                        sums.nextReachedMinPanes += pane.Min;
                    }
                }

                _panes[panesToResize[0]!.Value].Size = 100 - sums.prevPanesSize - sums.nextReachedMinPanes - _panes.Last().Min - sums.nextPanesSize;
                return null;
            }
        }

        return (sums, panesToResize);
    }

    private double SumPrevPanesSize(int index)
    {
        return _panes.Take(index).Sum(p => p.InternalSize);
    }

    private double SumNextPanesSize(int index)
    {
        return _panes.Skip(index + 2).Sum(p => p.InternalSize);
    }

    private double CalculateDragPercentage(MouseEventArgs args)
    {
        ArgumentNullException.ThrowIfNull(_containerRect);

        if (Row)
        {
            var y = args.ClientY - _containerRect.Top;
            return y * 100 / _containerRect.Height;
        }

        var x = args.ClientX - _containerRect.Left;
        return x * 100 / _containerRect.Width;
    }
}
