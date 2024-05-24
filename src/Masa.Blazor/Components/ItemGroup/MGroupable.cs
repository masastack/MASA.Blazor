namespace Masa.Blazor.Components.ItemGroup;

public abstract class MGroupable<TGroup> :  MasaComponentBase, IGroupable
    where TGroup : ItemGroupBase
{
    [CascadingParameter]
    public TGroup? ItemGroup { get; set; }

    [Parameter]
    public string? ActiveClass { get; set; }

    [Parameter]
    public virtual bool Disabled { get; set; }

    // TODO: use Value instead of IsActive
    [Parameter]
    public bool IsActive
    {
        get => _isActive ?? false;
        set => _isActive = value;
    }

    [Parameter]
    public StringNumber? Value
    {
        get => _value;
        set
        {
            if (value == null) return;

            _value = value;
        }
    }

    /// <summary>
    /// whether to enable bootable.
    /// </summary>
    private readonly bool _bootable;

    /// <summary>
    /// the <see cref="GroupType"/> of the groupable component.
    /// </summary>
    private readonly GroupType _groupType;

    private bool? _isActive;
    private StringNumber? _value;
    private bool _firstRenderAfterBooting;

    /// <summary>
    /// Initializes a base component <see cref="MGroupable{TGroup}"/> with the <see cref="GroupType"/>.
    /// </summary>
    /// <param name="groupType">the <see cref="GroupType"/> of the groupable component.</param>
    protected MGroupable(GroupType groupType)
    {
        _groupType = groupType;
    }

    /// <summary>
    /// Initializes a base component <see cref="MGroupable{TGroup}"/> with the <see cref="GroupType"/>
    /// and specifies whether to bootable.
    /// </summary>
    /// <param name="groupType">the <see cref="GroupType"/> of the groupable component.</param>
    /// <param name="bootable">determines whether bootable is enabled or not.</param>
    protected MGroupable(GroupType groupType, bool bootable) : this(groupType)
    {
        _bootable = bootable;
    }

    protected string? ComputedActiveClass => ActiveClass ?? ItemGroup?.ActiveClass;

    /// <summary>
    /// Determines whether the component has a routable ancestor component.
    /// </summary>
    protected virtual bool HasRoutableAncestor => ItemGroup is IAncestorRoutable { Routable: true };

    /// <summary>
    /// The routable ancestor.
    /// </summary>
    protected IAncestorRoutable? RoutableAncestor => HasRoutableAncestor ? (IAncestorRoutable)ItemGroup! : null;

    protected bool Matched => ItemGroup != null && (ItemGroup.GroupType == _groupType);

    protected bool ValueMatched => Matched && ItemGroup!.InternalValues.Contains(Value);

    public bool InternalIsActive { get; private set; }

    /// <summary>
    /// Determines whether the component has been booted.
    /// </summary>
    protected bool IsBooted { get; private set; }

    protected virtual bool IsEager { get; } = false;

    protected virtual bool HasTransition { get; } = false;

    protected override async Task OnInitializedAsync()
    {
        if (!Matched) return;

        if (this is IGroupable item)
        {
            ItemGroup!.Register(item);
        }

        await base.OnInitializedAsync();
    }

    protected override async Task OnParametersSetAsync()
    {
        if (_isActive.HasValue) // if setting by [Parameter]IsActive, Matched is not required.
        {
            await SetInternalIsActive(_isActive.Value);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_firstRenderAfterBooting)
        {
            await Task.Delay(16);
            _firstRenderAfterBooting = false;

            InternalIsActive = true;
            StateHasChanged();
        }
    }

    public async Task RefreshState()
    {
        if (!Matched || HasRoutableAncestor) return;

        await SetInternalIsActive(ValueMatched);
        StateHasChanged();
    }

    protected virtual async Task ToggleAsync()
    {
        if (!Matched) return;

        await ItemGroup!.ToggleAsync(Value);
    }

    protected async Task SetInternalIsActive(bool val, bool force = false)
    {
        if (IsEager)
        {
            if (InternalIsActive != val || force)
            {
                InternalIsActive = val;
            }
        }
        else
        {
            if (!IsBooted)
            {
                if (val)
                {
                    IsBooted = true;

                    if (HasTransition)
                    {
                        _firstRenderAfterBooting = true;

                        await Task.Delay(16);
                    }
                    else
                    {
                        InternalIsActive = true;
                    }

                    StateHasChanged();
                }
            }
            else if (InternalIsActive != val || force)
            {
                if (_firstRenderAfterBooting)
                {
                    // waiting for one frame(16ms) to make sure the element has been rendered,
                    // and then set the InternalIsActive to be true to invoke transition. 
                    await Task.Delay(16);
                    _firstRenderAfterBooting = false;
                }

                InternalIsActive = val;
            }
        }
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (Matched && this is IGroupable item)
        {
            ItemGroup!.Unregister(item);
        }

        await base.DisposeAsyncCore();
    }
}