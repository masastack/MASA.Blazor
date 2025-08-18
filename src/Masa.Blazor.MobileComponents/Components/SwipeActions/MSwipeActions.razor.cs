using Masa.Blazor.SourceGenerated;

namespace Masa.Blazor;

public partial class MSwipeActions
{
    [Parameter] [MasaApiParameter(true)]
    public bool CloseOnClick { get; set; } = true;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public RenderFragment<SwipeActionContext>? LeftContent { get; set; }

    [Parameter] public RenderFragment<SwipeActionContext>? RightContent { get; set; }

    private static readonly Block Block = new("m-swipe-actions");
    private static readonly Element WrapperElement = Block.Element("wrapper");
    private static readonly Element LeftElement = Block.Element("left");
    private static readonly Element RightElement = Block.Element("right");

    private ElementReference _wrapperEl;
    private IJSObjectReference? _useTouch;

    private SwipeActionContext _actionContext = null!;

    private readonly IDictionary<string, IDictionary<string, object>> _actionDefaults =
        new Dictionary<string, IDictionary<string, object>>
        {
            [nameof(MButton)] = new Dictionary<string, object>
            {
                [nameof(MButton.Depressed)] = true,
                [nameof(MButton.Tile)] = true,
                [nameof(MButton.Height)] = (StringNumber)"100%"
            }
        };

    protected override IEnumerable<string?> BuildComponentClass()
    {
        yield return Block.Name;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            var import = await Js.InvokeAsync<IJSObjectReference>("import",
                $"./_content/Masa.Blazor/js/{JSManifest.SwipeActionsIndexJs}");
            _useTouch = await import.InvokeAsync<IJSObjectReference>("useTouch", _wrapperEl, new
            {
                threshold = 0.3,
                duration = 600
            });
            await _useTouch.InvokeVoidAsync("bindEvents");
            _actionContext = new(Close);
            await import.DisposeAsync();
        }
    }

    private void OnClick()
    {
        if (CloseOnClick)
        {
            Close();
        }
    }

    private void Close() => _useTouch?.TryInvokeVoidAsync("close");

    protected override async ValueTask DisposeAsyncCore()
    {
        await _useTouch.TryInvokeVoidAsync("unbindEvents");
        _useTouch = null;
    }
}