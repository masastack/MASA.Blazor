using Masa.Blazor.Components.Window;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MWindow : MItemGroup
{
    public MWindow()
    {
        GroupType = GroupType.Window;
        Mandatory = true;
    }

    [Inject] public MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public bool Continuous { get; set; }

    [Parameter] public string? NextIcon { get; set; }

    [Parameter] public RenderFragment<Action>? NextContent { get; set; }

    [Parameter] public string? PrevIcon { get; set; }

    [Parameter] public RenderFragment<Action>? PrevContent { get; set; }

    [Parameter] public bool Reverse { get; set; }

    [Parameter] public bool ShowArrows { get; set; }

    [Parameter] public bool ShowArrowsOnHover { get; set; }

    [Parameter] public bool Vertical { get; set; }

    [Parameter] [MasaApiParameter(ReleasedIn = "v1.9.0")] public bool Touchless { get; set; }

    private static Block _block = new("m-window");
    private StringNumber _prevInternalValue;
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private ModifierBuilder _containerModifierBuilder = _block.Element("container").CreateModifierBuilder();

    private bool _prevTouchless;
    private int _useTouchId;
    private IJSObjectReference? _module;
    private DotNetObjectReference<MWindow>? _dotNetObjectReference;

    protected bool IsReverse { get; set; }

    protected bool RTL => MasaBlazor.RTL;

    public virtual bool ArrowsVisible => ShowArrowsOnHover || ShowArrows;

    public int TransitionCount { get; set; }

    public StringNumber? TransitionHeight { get; set; }

    public bool IsActive => TransitionCount > 0;

    public int InternalIndex { get; private set; }

    private int _prevInternalIndex = 0;

    private void UpdateInternalIndex()
    {
        InternalIndex = Items.FindIndex(item => item.Value == InternalValue);
        if (_prevInternalIndex == InternalIndex)
        {
            return;
        }

        IsReverse = UpdateReverse(InternalIndex, _prevInternalIndex);
        _prevInternalIndex = InternalIndex;
    }

    public bool HasActiveItems => Items.Any(item => !item.Disabled);

    public bool HasNext => Continuous || InternalIndex < Items.Count - 1;

    public bool HasPrev => Continuous || InternalIndex > 0;

    public bool InternalReverse => RTL ? !Reverse : Reverse;

    public string ComputedTransition
    {
        get
        {
            //TODO:isBooted

            var axis = Vertical ? "y" : "x";
            var reverse = InternalReverse ? !IsReverse : IsReverse;
            var direction = reverse ? "-reverse" : "";

            return $"m-window-{axis}{direction}-transition";
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ActiveClass = "m-window-item--active";
        PrevIcon ??= "$prev";
        NextIcon ??= "$next";
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        
        if (_prevTouchless != Touchless)
        {
            _prevTouchless = Touchless;

            if (Touchless)
            {
                await CleanupTouchAsync();
            }
            else
            { 
                await UseTouchAsync();
            }
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (!Touchless)
            {
                await UseTouchAsync();
            }
        }
    }

    private async Task UseTouchAsync()
    {
        _module = await Js.InvokeAsync<IJSObjectReference>("import", $"./_content/Masa.Blazor/js/{JSManifest.WindowTouchJs}");
        _useTouchId = await _module.InvokeAsync<int>("useTouch", Ref, new TouchValue(
            new Dictionary<string, AddEventListenerOptions>
            {
                { "touchstart", new AddEventListenerOptions { Passive = true, StopPropagation = true } }
            }), _dotNetObjectReference);
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat([
            _modifierBuilder.Add(ShowArrowsOnHover).Build()
        ]);
    }

    private string GetContainerClass() => _containerModifierBuilder.Add(IsActive).Build();
    private string GetContainerStyle() => StyleBuilder.Create().AddHeight(TransitionHeight).Build();

    protected override void OnInternalValuesChanged()
    {
        base.OnInternalValuesChanged();

        UpdateInternalIndex();
    }

    protected override void OnItemsUpdate()
    {
        base.OnItemsUpdate();

        // HasNext property needs the latest items count
        StateHasChanged();
    }

    protected override void RefreshItemsState()
    {
        base.RefreshItemsState();

        UpdateInternalIndex();
    }

    public void RenderState()
    {
        StateHasChanged();
    }

    [JSInvokable]
    public void OnTouchend(string direction)
    {
        if (Touchless)
        {
            return;
        }
        
        if (direction is "left")
        {
            if (InternalReverse)
            {
                Prev();
            }
            else
            {
                Next();
            }
        }
        else if (direction is "right")
        {
            if (InternalReverse)
            {
                Next();
            }
            else
            {
                Prev();
            }
        }
    }

    protected void Next()
    {
        if (!HasActiveItems || !HasNext) return;

        var nextIndex = GetNextIndex(InternalIndex);
        var nextItem = Items[nextIndex];

        _ = ToggleAsync(nextItem.Value);
    }

    private void Prev()
    {
        if (!HasActiveItems || !HasPrev) return;

        var prevIndex = GetPrevIndex(InternalIndex);
        var prevItem = Items[prevIndex];

        _ = ToggleAsync(prevItem.Value);
    }

    private int GetNextIndex(int currentIndex)
    {
        var nextIndex = (currentIndex + 1) % Items.Count;
        var nextItem = Items[nextIndex];

        if (nextItem.Disabled) return GetNextIndex(nextIndex);

        return nextIndex;
    }

    private int GetPrevIndex(int currentIndex)
    {
        var prevIndex = (currentIndex + Items.Count - 1) % Items.Count;
        var prevItem = Items[prevIndex];

        if (prevItem.Disabled) return GetPrevIndex(prevIndex);

        return prevIndex;
    }

    private bool UpdateReverse(int val, int oldVal)
    {
        var itemsLength = Items.Count;
        var lastIndex = itemsLength - 1;

        if (itemsLength <= 2) return val < oldVal;

        if (val == lastIndex && oldVal == 0)
        {
            return true;
        }
        else if (val == 0 && oldVal == lastIndex)
        {
            return false;
        }
        else
        {
            return val < oldVal;
        }
    }

    private async Task CleanupTouchAsync()
    {
        if (_module is null) return;

        await _module.InvokeVoidAsync("cleanupTouch", Ref, _useTouchId);
        await _module.DisposeAsync();
        _module = null;
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (_module is not null)
        {
            _dotNetObjectReference?.Dispose();
            await CleanupTouchAsync().ConfigureAwait(false);
        }

        await base.DisposeAsyncCore();
    }
}