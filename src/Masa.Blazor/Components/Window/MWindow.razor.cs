using Masa.Blazor.Mixins;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MWindow : MItemGroup
{
    public MWindow() : base(GroupType.Window)
    {
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

    protected bool IsReverse { get; set; }

    protected bool RTL => MasaBlazor.RTL;

    public virtual bool ArrowsVisible => ShowArrowsOnHover || ShowArrows;

    public int TransitionCount { get; set; }

    public StringNumber? TransitionHeight { get; set; }

    public bool IsActive => TransitionCount > 0;

    public int InternalIndex => GetComputedValue<int>();

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

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

#if NET8_0_OR_GREATER
            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
#endif
        ActiveClass = "m-window-item--active";
        PrevIcon ??= "$prev";
        NextIcon ??= "$next";
    }

    private Block _block = new("m-window");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            _block.Modifier(ShowArrowsOnHover)
                .GenerateCssClasses()
        );
    }

    private string GetContainerClass() => _block.Element("container").Modifier(IsActive).Build();
    private string GetContainerStyle() => StyleBuilder.Create().AddHeight(TransitionHeight).Build();

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch(nameof(InternalIndex),
            (newVal, oldVal) => IsReverse = UpdateReverse(newVal, oldVal),
            () => Items.FindIndex(item => item.Value == InternalValue),
            new[] { nameof(InternalValues) },
            false,
            true);
    }

    internal override void Register(IGroupable item)
    {
        base.Register(item);

        StateHasChanged();
    }

    public void RenderState()
    {
        StateHasChanged();
    }

    protected void Next()
    {
        if (!HasActiveItems || !HasNext) return;

        var nextIndex = GetNextIndex(InternalIndex);
        var nextItem = Items[nextIndex];

        _ = ToggleAsync(nextItem.Value);
    }

    protected void Prev()
    {
        if (!HasActiveItems || !HasPrev) return;

        var prevIndex = GetPrevIndex(InternalIndex);
        var prevItem = Items[prevIndex];

        _ = ToggleAsync(prevItem.Value);
    }

    protected int GetNextIndex(int currentIndex)
    {
        var nextIndex = (currentIndex + 1) % Items.Count;
        var nextItem = Items[nextIndex];

        if (nextItem.Disabled) return GetNextIndex(nextIndex);

        return nextIndex;
    }

    protected int GetPrevIndex(int currentIndex)
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
}