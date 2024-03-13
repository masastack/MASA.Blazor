using BlazorComponent;
using BlazorComponent.Components.Transition;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Playground.Pages;

public class MToggleTransitionElement : MTransitionElementBase<bool>, ITransitionJSCallback
{
    [Inject]
    private TransitionJSModule TransitionJSModule { get; set; } = null!;

    private bool _prevValue;

    protected bool LazyValue { get; set; }

    // Enter from -> Enter 需要Server端应用CSS
    private bool _entering;

    public string? TransitionName => Transition?.Name;

    public bool LeaveAbsolute => Transition?.LeaveAbsolute ?? false;

    protected override string? ComputedClass
    {
        get
        {
            if (TransitionName != null && Value && _entering)
            {
                return $"{TransitionName}-enter {TransitionName}-enter-active";
            }

            return base.ComputedClass;
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _prevValue = Value;
        LazyValue = Value;

        Console.Out.WriteLine($">>>>>>>>>>>>>>>>>>>> OnInitialized Value = {Value}");
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Console.Out.WriteLine($">>>>>>>>>>>>>>>>>>>> OnAfterRenderAsync Value = {Value}");
        }

        if (firstRender && TransitionName != null)
        {
            Console.Out.WriteLine($">>>>>>>>>>>>>>>>>>>> OnAfterRenderAsync Value = {Value}, TransitionName = {TransitionName}");
            await TransitionJSModule.InitializeAsync(this);
        }
        
        // TODO: 还得防抖

        if (_prevValue != Value)
        {
            _prevValue = Value;

            if (TransitionJSModule.Initialized)
            {
                if (Value)
                {
                    LazyValue = true;
                    _entering = true;
                    Console.Out.WriteLine($">>>>>>>>>>>>>>>>>>>> ValueChanged Value = {Value}");
                    // await Task.Delay(32); // LeaveAbsolute 时让 Leave的元素先设置绝对定位
                    NextTick(() => { _ = TransitionJSModule.OnEnterTo(Reference); });
                    StateHasChanged();
                }
                else
                {
                    _ = TransitionJSModule.OnLeave();
                }
            }
        }
        else
        {
            _entering = false;
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    public async Task HandleOnTransitionend()
    {
        if (Value)
        {
            return;
        }

        LazyValue = false;
        StateHasChanged();
    }
}
