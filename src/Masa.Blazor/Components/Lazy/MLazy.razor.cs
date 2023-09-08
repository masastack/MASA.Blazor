namespace Masa.Blazor;

public partial class MLazy
{
    [Inject] private DomEventJsInterop DomEventJsInterop { get; set; } = null!;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public StringNumber? MinHeight { get; set; }

    private Block Block => new("m-lazy");

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        
        await DomEventJsInterop.IntersectionObserver(Ref.GetSelector(), TryAutoFocus, OnResize);

    }
}
