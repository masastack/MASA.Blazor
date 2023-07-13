using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor.Playground.Pages;

public class MTransitionElement : MElement
{
    #region parameters working with transition

    [Parameter] public bool TransitionIf { get; set; }

    [Parameter] public bool TransitionShow { get; set; }

    [Parameter] public string? TransitionKey { get; set; }

    #endregion

    private string[] _dirtyParameters = Array.Empty<string>();

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        _dirtyParameters = parameters.ToDictionary().Keys.ToArray();

        await base.SetParametersAsync(parameters);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        // TODO: other attributes

        if (_dirtyParameters.Contains(nameof(TransitionIf)))
        {
            builder.OpenComponent<MIfTransitionElement>(0);
            builder.AddAttribute(1, "Value", TransitionIf);
            builder.AddAttribute(2, nameof(ChildContent), ChildContent);
            builder.CloseComponent();
        }
        else if (_dirtyParameters.Contains(nameof(TransitionShow)))
        {
            builder.OpenComponent<MShowTransitionElement>(0);
            builder.AddAttribute(1, "Value", TransitionShow);
            builder.AddAttribute(2, nameof(ChildContent), ChildContent);
            builder.CloseComponent();
        }
        else if (_dirtyParameters.Contains(nameof(TransitionKey)))
        {
            builder.OpenComponent<MKeyTransitionElement>(0);
            builder.AddAttribute(1, "Value", TransitionKey);
            builder.AddAttribute(2, nameof(ChildContent), ChildContent);
            builder.CloseComponent();
        }
        else
        {
            base.BuildRenderTree(builder);
        }
    }
}
