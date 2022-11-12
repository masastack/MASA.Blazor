namespace Masa.Docs.Shared.Examples.lists;

public class Usage : Masa.Docs.Shared.Components.Usage
{
    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MList.Dense), false },
        { nameof(MList.Flat), false },
    };

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<Index>(0);
        builder.CloseComponent();
    };

    public Usage() : base(typeof(MList))
    {
    }
}