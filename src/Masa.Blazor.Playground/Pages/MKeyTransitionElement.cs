using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor.Playground.Pages;

public class KeyTransitionState<TValue> where TValue : notnull
{
    public TValue Key { get; set; }

    public bool Value { get; set; }

    public KeyTransitionState(TValue key, bool value)
    {
        Key = key;
        Value = value;
    }
}

public class MKeyTransitionElement<TValue> : MTransitionElementBase<TValue> where TValue : notnull
{
    private TValue? _prevValue;
    private List<KeyTransitionState<TValue>?> _states = new() { default, default };
    private bool need_a_render;

    private List<KeyTransitionState<TValue>?> ComputedStates => _states.Where(s => s is not null).ToList();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _prevValue = Value;
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!EqualityComparer<TValue>.Default.Equals(Value, _prevValue))
        {
            _states[0] = new KeyTransitionState<TValue>(_prevValue, true);
            _states[1] = new KeyTransitionState<TValue>(Value, false);

            _prevValue = Value;

            need_a_render = true;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (need_a_render)
        {
            need_a_render = false;

            NextTick(() =>
            {
                _states[0].Value = false;
                _states[1].Value = true;
                StateHasChanged();
            });

            // await Task.Delay(16);

            StateHasChanged();
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (ComputedStates.Count == 0)
        {
            BuildRenderTree2(builder, new KeyTransitionState<TValue>(Value, true));
        }
        else
        {
            foreach (var state in _states)
            {
                BuildRenderTree2(builder, state);
            }
        }
    }

    private void BuildRenderTree2(RenderTreeBuilder builder, KeyTransitionState<TValue> state)
    {
        Console.Out.WriteLine($"state.Key ={state.Key}, state.Value = {state.Value}");

        builder.OpenComponent<MIfTransitionElement>(0);
        builder.AddAttribute(1, nameof(MIfTransitionElement.Value), state.Value);
        builder.AddAttribute(2, nameof(ChildContent), RenderChildContent(state));
        builder.AddAttribute(3, nameof(Tag), Tag);
        builder.SetKey(state.Key);
        builder.CloseComponent();
    }

    private RenderFragment RenderChildContent(KeyTransitionState<TValue> state)
    {
        return builder =>
        {
            var sequence = 0;
            builder.OpenComponent<Container>(sequence++);
            builder.AddAttribute(sequence++, nameof(Container.Value), EqualityComparer<TValue>.Default.Equals(state.Key, Value));
            builder.AddAttribute(sequence, nameof(ChildContent), ChildContent);
            builder.SetKey(state.Key);
            builder.CloseComponent();
        };
    }
}
