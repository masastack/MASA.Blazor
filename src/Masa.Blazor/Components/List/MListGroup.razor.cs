using Microsoft.AspNetCore.Components.Routing;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MListGroup : MasaComponentBase
{
    [CascadingParameter] public MList? List { get; set; }

    [Parameter] public string? ActiveClass { get; set; }

    [Parameter] public bool NoAction { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter]
    [MasaApiParameter("primary")]
    public string? Color { get; set; } = "primary";

    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public List<string>? Group { get; set; }

    [Parameter]
    public bool Value
    {
        get => _value;
        set
        {
            _value = value;
            IsActive = value;
        }
    }

    [Parameter] public EventCallback<bool> ValueChanged { get; set; }

    [Parameter] public string? PrependIcon { get; set; }

    [Parameter] public RenderFragment? PrependIconContent { get; set; }

    [Parameter] public string? AppendIcon { get; set; }

    [Parameter] public RenderFragment? AppendIconContent { get; set; }

    [Parameter] public RenderFragment? ActivatorContent { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool SubGroup { get; set; }

    [Parameter] public bool Eager { get; set; }

    private bool _value;
    private string? _previousAbsolutePath;

    protected bool IsActive { get; set; }

    protected bool IsBooted { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        List?.Register(this);

        NavigationManager.LocationChanged += OnLocationChanged;

        _previousAbsolutePath = NavigationManager.GetAbsolutePath();
    }

    private Block _block = new("m-list-group");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier("active", IsActive)
            .And(Disabled)
            .And(NoAction)
            .And(SubGroup)
            .AddTextColor(Color)
            .GenerateCssClasses();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create().AddTextColor(Color).GenerateCssStyles();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        EnsureBooted();

        if (SubGroup)
        {
            PrependIcon ??= "$subgroup";
        }
        else
        {
            AppendIcon ??= "$expand";
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            var shouldRender = UpdateActiveForRoutable();
            if (shouldRender)
            {
                StateHasChanged();
            }
        }
    }

    private void EnsureBooted()
    {
        if (!IsBooted && (IsActive || Eager))
        {
            IsBooted = true;
        }
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        var absolutePath = NavigationManager.GetAbsolutePath();
        if (_previousAbsolutePath == absolutePath)
        {
            return;
        }

        _previousAbsolutePath = absolutePath;

        var shouldRender = UpdateActiveForRoutable();
        if (shouldRender)
        {
            InvokeStateHasChanged();
        }
    }

    private async Task HandleOnClick(EventArgs args)
    {
        if (Disabled) return;

        if (!IsBooted)
        {
            IsBooted = true;

            // waiting for one frame(16ms) to make sure the element has been rendered,
            await Task.Delay(16);

            StateHasChanged();
        }

        IsActive = !IsActive;
        await UpdateValue(IsActive);
    }

    private bool MatchRoute(string path)
    {
        if (Group is null) return false;

        var relativePath = "/" + NavigationManager.ToBaseRelativePath(path);
        return Group.Any(item => Regex.Match(relativePath, item, RegexOptions.IgnoreCase).Success);
    }

    private bool UpdateActiveForRoutable()
    {
        var isActive = IsActive;

        if (Group != null)
        {
            IsActive = MatchRoute(NavigationManager.Uri);
            EnsureBooted();
        }

        return isActive != IsActive;
    }

    private async Task UpdateValue(bool value)
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(value);
        }
        else
        {
            Value = value;
        }
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        List?.Unregister(this);
        NavigationManager.LocationChanged -= OnLocationChanged;

        await base.DisposeAsyncCore();
    }
}