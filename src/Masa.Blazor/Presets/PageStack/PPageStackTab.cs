namespace Masa.Blazor.Presets.PageStack;

public class PPageStackTab : MasaComponentBase
{
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, ChildContent?.Invoke(_context));
    }

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Parameter] [EditorRequired] public RenderFragment<PageStackTabContext>? ChildContent { get; set; }

    [Parameter] public EventCallback<string?> OnNavigate { get; set; }

    [Parameter] [EditorRequired] public string? Href { get; set; }

    [Parameter] public bool Refreshable { get; set; } = true;

    [Parameter] public bool RefreshOnDblClick { get; set; }

    private readonly PageStackTabContext _context = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _context.Attrs["__internal_preventDefault_onclick"] = true;
        _context.Attrs["onclick"] = EventCallback.Factory.Create<MouseEventArgs>(this, HandleOnClick);
        _context.Attrs["ondblclick"] = EventCallback.Factory.Create<MouseEventArgs>(this, HandleOnDblClick);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _context.Attrs["href"] = Href;
    }

    private async Task HandleOnClick()
    {
        if (string.IsNullOrWhiteSpace(Href))
        {
            return;
        }

        if (OnNavigate.HasDelegate)
        {
            await OnNavigate.InvokeAsync(Href);
        }
        else
        {
            NavigationManager.Replace(Href);
        }
    }

    private void HandleOnDblClick()
    {
        if (!RefreshOnDblClick) return;
    }
}

public class PageStackTabContext
{
    public Dictionary<string, object?> Attrs { get; } = new();
}